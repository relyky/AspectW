using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;
using NCCUCS.AspectW;

using System.Diagnostics;

namespace DemoApp
{
    public partial class Form2 : Form
    {
        private MyLogger _logger;
        private ServiceSimulator _svc;

        #region AOP Program

        private void MyTrace(string message, params object[] args)
        {
            _logger.Write(string.Format("[{0:HH:mm:ss}] ", DateTime.Now));
            _logger.WriteLine(string.Format(message, args));
        }

        #endregion

        public Form2()
        {
            InitializeComponent();
            //
            _logger = new MyLogger(this.txtMessage);
            _svc = new ServiceSimulator(_logger);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // controls init.
            cboException.SelectedIndex = 0;

            ServiceSimulator.SetFailSeed((int)numErrorSeed.Value);
            ServiceSimulator.SetFailDepth((int)numErrorDepth.Value);
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMessage.Clear();
        }

        private void numErrorSeed_ValueChanged(object sender, EventArgs e)
        {
            ServiceSimulator.SetFailSeed((int)numErrorSeed.Value);
        }

        private void numErrorDepth_ValueChanged(object sender, EventArgs e)
        {
            ServiceSimulator.SetFailDepth((int)numErrorDepth.Value);
        }

        private void cboException_SelectedIndexChanged(object sender, EventArgs e)
        {
            ServiceSimulator.SetFailException(cboException.Text);
        }

        private void btnBasicRetry_Click(object sender, EventArgs e)
        {
            try
            {
                AspectW.Define
                    .WaitCursor(this, btnBasicRetry) 
                    .Retry(3000, 2)
                    .Trace(() => this.MyTrace("BEGIN : Basic Retry"),
                           () => this.MyTrace("END\r\n"),
                           (ex) => this.MyTrace("CATCH : {0}", ex.Message))
                    .Do(() =>
                    {
                        _svc.SendMessage("John", "hello, long time no see.");
                    });
            }
            catch (Exception ex)
            {
                txtMessage.AppendText(string.Format("<<OUTSIDE CATCH>> {0}\r\n\r\n", ex.Message));
            }
        }

        private void btnBasicRetryT_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    // OnEnter
                    this.Cursor = Cursors.WaitCursor; //@ UI control, mouse
                    btnBasicRetryT.Enabled = false;

                    int retryCount = 2;
                    for (; ; ) //@ for retry mechanism.
                    {
                        try
                        {
                            // before
                            this.MyTrace("BEGIN : Basic Retry");
                            // Biz-Logic
                            _svc.SendMessage("John", "hello, long time no see.");
                            // after
                            this.MyTrace("END\r\n");
                            // success & leave
                            break;
                        }
                        catch (Exception ex)
                        {
                            // exception tracing
                            this.MyTrace("CATCH : {0}", ex.Message);
                            //@ for retry mechanism.
                            if (retryCount-- > 0)
                            {
                                Thread.Sleep(3000);
                                continue; // retry
                            }
                            // fail & leave
                            throw; 
                        }
                    }
                }
                finally
                {
                    // OnLeave
                    this.Cursor = Cursors.Default; //@ UI control, mouse
                    btnBasicRetryT.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                txtMessage.AppendText(string.Format("<<OUTSIDE CATCH>> {0}\r\n\r\n", ex.Message));
            }
        }

        private void btnParamRetry_Click(object sender, EventArgs e)
        {
            try
            {
                string protocol = "HTTPS";
                AspectW.Define
                    .WaitCursor(this, btnParamRetry)
                    .RetryParam(3000, // parameterized retry
                        ()=> protocol="HTTP",   // 1st retry with new parameter
                        ()=> protocol="SMS")  // 2nd retry with another new parameter
                    .Trace(() => this.MyTrace("BEGIN : Parameterized Retry"),
                           () => this.MyTrace("END\r\n"),
                           (ex) => this.MyTrace("CATCH : {0}", ex.Message))
                    .Do(() =>
                    {
                        _svc.SendMessage("John", "see you again.", protocol);
                    });
            }
            catch (Exception ex)
            {
                txtMessage.AppendText(string.Format("<<OUTSIDE CATCH>> {0}\r\n\r\n", ex.Message));
            }
        }

        private void btnParamRetryT_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    // OnEnter
                    this.Cursor = Cursors.WaitCursor; //@ UI control, mouse
                    btnParamRetryT.Enabled = false;

                    // parameters for doing retry
                    string[] retryParamAry = new string[] { "HTTPS", "HTTP", "SMS" };
                    int retryParamIndex = 0;
                    string protocol = retryParamAry[retryParamIndex++];

                    for (; ; ) //@ for retry mechanism.
                    {
                        try
                        {
                            // before
                            this.MyTrace("BEGIN : Parameterized Retry");
                            // Biz-Logic
                            _svc.SendMessage("John", "see you again.", protocol);
                            // after
                            this.MyTrace("END\r\n");
                            // success & leave
                            break;
                        }
                        catch (Exception ex)
                        {
                            // exception tracing
                            this.MyTrace("CATCH : {0}", ex.Message);
                            //@ for retry mechanism.
                            if (retryParamIndex < retryParamAry.Length)
                            {
                                Thread.Sleep(3000);
                                protocol = retryParamAry[retryParamIndex++];
                                continue; // retry
                            }
                            // fail & leave
                            throw; 
                        }
                    }
                }
                finally
                {
                    // OnLeave
                    this.Cursor = Cursors.Default; //@ UI control, mouse
                    btnParamRetryT.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                txtMessage.AppendText(string.Format("<<OUTSIDE CATCH>> {0}\r\n\r\n", ex.Message));
            }
        }

        ///<remarks>
        ///try
        ///{
        ///    ReadData();
        ///}
        ///catch(NetworkConnectivityException e)
        ///{
        ///    Thread.Sleep(3000);
        ///    retry 2;
        ///}
        ///fail 
        ///{
        ///    Console.Writeline("network failure!");
        ///}
        ///catch(StaleDataException e)
        ///{
        ///    RefreshData();
        ///    retry 10;
        ///}
        ///</remarks>
        private void btnMultiExRetry_Click(object sender, EventArgs e)
        {
            try
            {
                AspectW.Define
                    .WaitCursor(this, btnMultiExRetry)
                    .Retry<DataException>(0, 10, (ex) => _svc.RefreshData())
                    .Retry<IOException>(3000, 2, null, (ex) => txtMessage.AppendText("network failure!\r\n"))
                    .Trace(() => this.MyTrace("BEGIN : Recovery From Multiple Exception"),
                           () => this.MyTrace("END\r\n"),
                           (ex) => this.MyTrace("CATCH<{0}>", ex.GetType().Name))
                    .Do(() =>
                    {
                        _svc.ReadData();
                    });
            }
            catch (Exception ex)
            {
                txtMessage.AppendText(string.Format("<<OUTSIDE CATCH>> {0}\r\n\r\n", ex.Message));
            }
        }

        private void btnMultiExRetryT_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    // OnEnter
                    this.Cursor = Cursors.WaitCursor; //@ UI control, mouse
                    btnMultiExRetryT.Enabled = false;

                    int retryCount1 = 2;
                    int retryCount2 = 10;
                    for (; ; ) //@ for retry mechanism.
                    {
                        try
                        {
                            // before
                            this.MyTrace("BEGIN : Recovery From Multiple Exception");
                            // Biz-Logic
                            _svc.ReadData();
                            // after
                            this.MyTrace("END\r\n");
                            // success & leave
                            break;
                        }
                        catch (IOException ex)
                        {
                            // exception tracing
                            this.MyTrace("CATCH : {0}", ex.Message);

                            //@ for retry mechanism.
                            if (retryCount1-- > 0)
                            {
                                Thread.Sleep(3000);
                                continue; // retry
                            }
                            else
                            {
                                // Handle Fail!
                                txtMessage.AppendText("network failure!\r\n");
                                throw; // fail & leave
                            }
                        }
                        catch (DataException ex)
                        {
                            // exception tracing
                            this.MyTrace("CATCH : {0}", ex.Message);

                            //@ for retry mechanism.
                            if(retryCount2-- > 0)
                            {
                                // recovery action
                                _svc.RefreshData();                  
                                continue; // retry
                            }
                            else // fail!
                            {
                                throw; // fail & leave
                            }
                        }
                    }
                }
                finally
                {
                    // OnLeave
                    this.Cursor = Cursors.Default; //@ UI control, mouse
                    btnMultiExRetryT.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                txtMessage.AppendText(string.Format("<<OUTSIDE CATCH>> {0}\r\n\r\n", ex.Message));
            }
        }

        private void btnSkipFault_Click(object sender, EventArgs e)
        {
            ///// This example captures the problem of getting status updates from
            ///// one’s friends on Facebook.
            ///// In this variant, we allow individual people’s status to be
            ///// stale. We simply choose to skip these outdated status messages
            ///// when creating a set of status updates:
            //
            //TimestampingMap<Person, Set<Person>> friends;
            //TimestampingMap<Person, Message> statusUpdates;
            //struct Message {string msg; DateTime time};
            //
            //List<Message> getStatusUpdate(Person person, TimeSpan staleness)
            //{
            //  List<Message> result = new List<Message>();
            //  Set<Person> f = friends.Get(person, staleness);
            //  foreach (Person p in f)
            //  {
            //    AspectW.Define
            //      .Ignore<StalenessException>()
            //      .Do(()=>
            //        result.Add(statusUpdates.Get(p, staleness))
            //      );
            //  }
            //  return result;
            //}

            /////////////////////////////////////////////////////////
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    btnSkipFault.Enabled = false;
            //    this.MyTrace("BEGIN : GetStatusUpdate");
            //
            //    List<AMessage> result = _svc.GetStatusUpdate("Alan");
            //
            //    this.MyTrace("SUCCESS");
            //    foreach (AMessage m in result)
            //        txtMessage.AppendText(string.Format("{0} : {1} \r\n", m.sender.Name, m.message));
            //
            //    this.MyTrace("END\r\n");
            //}
            //catch (Exception ex)
            //{
            //    txtMessage.AppendText(string.Format("<<OUTSIDE CATCH>> {0}\r\n\r\n", ex.Message));
            //}
            //finally
            //{
            //    btnSkipFault.Enabled = true;
            //    this.Cursor = Cursors.Default;
            //}

            ///////////////////////////////////////////////////////
            try
            {
                AspectW.Define
                    .WaitCursor(this, btnSkipFault)
                    .Trace(() => this.MyTrace("BEGIN : GetStatusUpdate"),
                           () => this.MyTrace("END\r\n"))
                    .Do(() =>
                    {
                        List<AMessage> result = _svc.GetStatusUpdate("Alan");

                        txtMessage.AppendText("SHOW RESULT : \r\n");
                        foreach (AMessage m in result)
                            txtMessage.AppendText("{0} : {1} \r\n", m.sender.Name, m.message);
                    });
            }
            catch (Exception ex)
            {
                txtMessage.AppendText(string.Format("<<OUTSIDE CATCH>> {0}\r\n\r\n", ex.Message));
            }
        }

        /// <summary>
        /// helper function : for btnComposite_Click
        /// </summary>
        private bool IsValid(string address)
        {
            // return (address == "Taipei"); // 假設：住在台北的朋友

            bool isValid = (address == "Taipei"); // 假設：住在台北的朋友

            if (!isValid)
                this.MyTrace(string.Format("NOT_VALID : address = \"{0}\"", address));

            return isValid;
        }

        private void btnComposite_Click(object sender, EventArgs e)
        {
            /////////////////////////////////////////////////////////
            //void SendEventInvite(
            //  Person me,
            //  AddressFilter<string,bool> isValid,
            //  string message,
            //  TimeStamp staleness)
            //{
            //  foreach(Person friend in friends.Get(me, staleness))
            //  {
            //    AspectW.Define
            //      .OnLeave(()=> Console.WriteLine("Done"))
            //      .Retry<StaleDataException>(3000, 5, 
            //          ()=> friend.Refresh("address")), 
            //          ()=> Console.WriteLine("5 retries failed"))
            //      .Do(()=>
            //      {
            //        string address = friend.Get("address", staleness);
            //        string protocol = "HTTPS";
            //        AspectW.Define
            //          .When(isValid(friend))
            //          .RetryParam<AvailablityException>(3000, ()=> protocol="HTTP" )
            //          .Do(()=> 
            //            sendMessage(friend, message, protocol)
            //          );
            //      }
            //  }
            //}

            try
            {
                AspectW.Define
                    .WaitCursor(this, btnComposite)
                    .Trace(() => this.MyTrace("BEGIN : SendEventInvite"),
                           () => this.MyTrace("END\r\n"))
                    .Do(() =>
                    {
                        Person me = _svc.GetPerson("Alan");
                        _svc.SendEventInvite(me, IsValid, "Please join my party.");
                    });
            }
            catch (Exception ex)
            {
                txtMessage.AppendText(string.Format("<<OUTSIDE CATCH>> {0}\r\n\r\n", ex.Message));
            }
        }


    }

    #region Service Simulator

    /// <summary>
    /// Implementing a Derived Class of TextWriter.
    /// Here's a minimal TextWriter descendant:
    /// </summary>
    /// <see cref="http://stackoverflow.com/questions/17712607/implementing-a-derived-class-of-textwriter"/>
    internal class MyLogger : TextWriter
    {
        TextBox _msgBox;

        public MyLogger(TextBox msgBox)
        {
            _msgBox = msgBox;
        }

        public override void Write(char value)
        {
            _msgBox.AppendText(value.ToString());
        }

        public override Encoding Encoding
        {
            get { return Encoding.Default; }
        }
    }

    internal class ServiceSimulator
    {
        private TextWriter _logger;

        #region FAIL_RISE

        /// <summary>
        /// 執行 N次後就會 Fail。
        /// </summary>
        private static int _FailSeed = int.MaxValue;

        /// <summary>
        /// 當"fail rise"後，要連續 Fail的次數。-1表示１次；-2表示２次，以此類推。
        /// </summary>
        private static int _FailDepth = -1;

        /// <summary>
        /// 當降到負數時，表示該"fail rise"了。
        /// </summary>
        private static int _FailCountDown = _FailSeed;

        public static void SetFailSeed(int failSeed)
        {
            _FailCountDown = _FailSeed = failSeed;
        }

        public static void SetFailDepth(int failDepth)
        {
            _FailDepth = failDepth;
        }

        /// <summary>
        /// 模擬失敗機率
        /// </summary>
        internal static bool FAIL_RISE
        {
            get
            {
                if (--_FailCountDown < 0) // 每查看一次就降１級。當降到負數時，表示該"fail rise"了。
                {
                    if(_FailCountDown <= _FailDepth) // 已達深度？
                        _FailCountDown = _FailSeed;
                    return true; // fail rise
                }
                return false;
            }
        }

        #endregion

        #region FAIL_EXCEPTION

        private static EnumFailException _FailException = EnumFailException.Random; // random

        private enum EnumFailException : int
        {
            Random = -1,
            ApplicationException = 0,
            IOException = 1,
            DataException = 2
        }

        private static readonly Random _rand = new Random();

        /// <summary>
        /// 設定吐出的 FAIL
        /// </summary>
        public static void SetFailException(string exception)
        {
            switch (exception)
            {
                case "ApplicationException":
                    _FailException = EnumFailException.ApplicationException;
                    break;
                case "IOException":
                    _FailException = EnumFailException.IOException;
                    break;
                case "DataException":
                    _FailException = EnumFailException.DataException;
                    break;
                default:
                    _FailException = EnumFailException.Random;
                    break;
            }
        }

        /// <summary>
        /// 依設定吐出 FAIL
        /// </summary>
        internal static Exception NewFailException(string message)
        {
            int idx = _FailException == EnumFailException.Random 
                    ? _rand.Next(3) 
                    : (int)_FailException;

            switch (idx)
            {
                case 0:
                    return new ApplicationException(message);
                case 1:
                    return new IOException(message);
                case 2:
                    return new DataException(message);
                default:
                    return new Exception(message);
            }
        }

        #endregion

        #region Test Data
        //TimestampingMap<Person, Set<Person>> friends;
        internal static List<Person> friends = null;
        //TimestampingMap<Person, Message> statusUpdates;
        internal static List<AMessage> statusUpdates = null;  

        private void InitTestData()
        {
            // had initialized.
            if (friends != null) return;

            #region init. friends

            // 定義所有人
            Person Alen   = new Person("Alan", "Taipei");
            Person Ben    = new Person("Ben", "Taipei");
            Person Colin = new Person("Colin", "Taipei");
            Person Daniel = new Person("Daniel", "New York");
            Person Edwin = new Person("Edwin", "New York");
            //
            Person Fiona = new Person("Fiona", "Taipei");
            Person Grace = new Person("Grace", "Taipei");
            Person Hebe  = new Person("Hebe", "Taipei");
            Person Ivy = new Person("Ivy", "New York");
            Person Jean = new Person("Jean", "New York");
            //
            Person Ken   = new Person("Ken", "Taipei");
            Person Lee   = new Person("Lee", "Taipei");
            Person Mark  = new Person("Mark", "Taipei");
            Person Neil = new Person("Neil", "New York");
            Person Oscar = new Person("Oscar", "New York");
            //
            Person Peggy    = new Person("Peggy", "Taipei");
            Person Quentian = new Person("Quentian", "Taipei");
            Person Rose     = new Person("Rose", "Taipei");
            Person Sandy = new Person("Sandy", "New York");
            Person Tina = new Person("Tina", "New York");
            //
            Person Upton   = new Person("Upton", "Taipei");
            Person Van     = new Person("Van", "Taipei");
            Person William = new Person("William", "Taipei");
            Person Xavier  = new Person("Xavier", "Taipei");
            Person York = new Person("York", "New York");
            Person Ziv = new Person("Ziv", "New York");

            // 互加好友
            Alen.AddFriend(Ben, Colin, Daniel, Edwin);
            Fiona.AddFriend(Grace, Hebe, Ivy, Jean);
            Ken.AddFriend(Lee, Mark, Neil, Oscar);
            Peggy.AddFriend(Quentian, Rose, Sandy, Tina);
            Upton.AddFriend(Van, William, Xavier, York, Ziv);
                
            // 存入數據庫
            friends = new List<Person>();
            friends.AddRange(new Person[] { 
                Alen, Ben, Colin, Daniel, Edwin, 
                Fiona, Grace, Hebe, Ivy, Jean, 
                Ken, Lee, Mark, Neil, Oscar, 
                Peggy, Quentian, Rose, Sandy, Tina, 
                Upton, Van, William, Xavier, York, Ziv 
            });

            #endregion of init. friends

            #region 留下一些訊息
            statusUpdates = new List<AMessage>();
            statusUpdates.Add(new AMessage(Alen, "Hi, I am Alen."));
            statusUpdates.Add(new AMessage(Alen, "Today is my day."));
            statusUpdates.Add(new AMessage(Ben, "Hi, I am Ben."));
            statusUpdates.Add(new AMessage(Ben, "Today is my day."));
            statusUpdates.Add(new AMessage(Colin, "Hi, I am Colin."));
            statusUpdates.Add(new AMessage(Colin, "Today is my day."));
            statusUpdates.Add(new AMessage(Daniel, "Hi, I am Daniel."));
            statusUpdates.Add(new AMessage(Daniel, "Today is my day."));
            statusUpdates.Add(new AMessage(Edwin, "Hi, I am Edwin."));
            statusUpdates.Add(new AMessage(Edwin, "Today is my day."));
            statusUpdates.Add(new AMessage(Fiona, "Hi, I am Fiona."));
            statusUpdates.Add(new AMessage(Fiona, "Today is my day."));
            statusUpdates.Add(new AMessage(Grace, "Hi, I am Grace."));
            statusUpdates.Add(new AMessage(Grace, "Today is my day."));
            statusUpdates.Add(new AMessage(Hebe, "Hi, I am Hebe."));
            statusUpdates.Add(new AMessage(Hebe, "Today is my day."));
            statusUpdates.Add(new AMessage(Ivy, "Hi, I am Ivy."));
            statusUpdates.Add(new AMessage(Ivy, "Today is my day."));
            statusUpdates.Add(new AMessage(Jean, "Hi, I am Jean."));
            statusUpdates.Add(new AMessage(Jean, "Today is my day."));
            #endregion 

        }

        #endregion

        public ServiceSimulator(TextWriter logger)
        {
            // init.
            _logger = logger;

            InitTestData();
        }

        public void SendMessage(string target, string message)
        {
            SendMessage(target, message, "HTTPS");
        }

        public void SendMessage(string target, string message, string protocol)
        {
            //## 模擬失敗可能性。
            if (FAIL_RISE)
                throw NewFailException(string.Format("[{0}] SendMessage FAIL!", protocol));

            // success
            _logger.Write(string.Format("[{0:HH:mm:ss}] ", DateTime.Now));
            _logger.WriteLine(string.Format("[{0}] SendMessage(\"{1}\", \"{2}\")", protocol, target, message));
        }

        public void SendMessage(Person target, string message, string protocol)
        {
            SendMessage(target.Name, message, protocol);
        }

        public void ReadData()
        {
            //## 模擬失敗可能性。
            if (FAIL_RISE)
                throw NewFailException("ReadData FAIL!");

            // success
            _logger.Write(string.Format("[{0:HH:mm:ss}] ", DateTime.Now));
            _logger.WriteLine("ReadData()");
        }

        public void RefreshData()
        {
            // 先假設一定成功

            // success
            _logger.Write(string.Format("[{0:HH:mm:ss}] ", DateTime.Now));
            _logger.WriteLine("RefreshData()");
        }

        public void RefreshAddress(Person psn)
        {
            // 先假設一定成功

            // success
            _logger.Write(string.Format("[{0:HH:mm:ss}] ", DateTime.Now));
            _logger.WriteLine(string.Format("RefreshAddress({0})", psn.Name));
        }

        public List<AMessage> GetStatusUpdate(string name)
        {
            List<AMessage> result = new List<AMessage>();
            Person[] f = friends.Get(name);
            foreach (Person p in f)
            {
                AspectW.Define
                    .Ignore<IOException>()
                    .TraceException<IOException>((ex)=> _logger.WriteLine("IGNORE : " + ex.Message))
                    .Do(()=> 
                        result.Add(statusUpdates.Get(p))
                        );
            }
            return result;
        }

        public Person GetPerson(string name)
        {
            return friends.GetPerson(name);
        }

        public void SendEventInvite(Person me, Func<string, bool> isValid, string message)
        {
            foreach(Person friend in friends.Get(me))
            {
                AspectW.Define
                    .OnLeave(() => _logger.WriteLine("Done"))
                    .Retry<DataException>(3000, 5,
                        (ex)=> RefreshAddress(friend), // recovery action
                        (ex)=> _logger.WriteLine("5 retries failed")) // fail action
                    .TraceException((ex) => _logger.WriteLine("CATCH@L1<{0}> : {1}", ex.GetType().Name, ex.Message))
                    .Do(()=>
                    {
                        string address = friend.GetAddress();
                        string protocol = "HTTPS";
                        AspectW.Define
                            .WhenTrue(isValid(address))
                            .RetryParam<ApplicationException>(3000, ()=> protocol="HTTP" )
                            .TraceException((ex) => _logger.WriteLine("CATCH@L2<{0}> : {1}", ex.GetType().Name, ex.Message))
                            .Do(()=> 
                                SendMessage(friend, message, protocol)
                            );
                    });
            }
        }
    }

    #region Test Data Helper

//TimestampingMap<Person, Set<Person>> friends;
//TimestampingMap<Person, Message> statusUpdates;
//struct Message {string msg; DateTime time};

    internal class AMessage //: IComparable
    { 
        public Person sender;
        public string message;
        public DateTime timeStamp;

        public AMessage(Person _person, string _message)
        {
            sender = _person;
            message = _message;
            timeStamp = DateTime.Now;
        }
    }

    internal class Person
    {
        string name;
        string address;
        ArrayList friends = new ArrayList();

        public string Name
        {
            get { return name; }
        }

        public string GetAddress()
        {
            return address;
        }

        public Person(string _name, string _address)
        {
            name = _name;
            address = _address;
        }

        public void AddFriend(params Person[] psn)
        {
            friends.AddRange(psn);
        }

        public Person[] GetFriends()
        {
            return (Person[])this.friends.ToArray(typeof(Person));
        }
    }

    internal static class ServiceSimulatorExtensions
    {
        internal static AMessage Get(this List<AMessage> status, Person psn)
        {
            //## 模擬失敗可能性。
            if (ServiceSimulator.FAIL_RISE)
                throw ServiceSimulator.NewFailException("statusUpdates.GetMessage FAIL!");

            return status.Where(c => c.sender == psn).FirstOrDefault();
        }

        internal static Person GetPerson(this List<Person> friends, string name)
        {
            ////## 模擬失敗可能性。
            //if (ServiceSimulator.FAIL_RISE)
            //    throw ServiceSimulator.NewFailException("friends.GetFriends FAIL!");
           
            return friends.Where(c => c.Name == name).FirstOrDefault();
        }

        internal static Person[] Get(this List<Person> friends, string name)
        {
            ////## 模擬失敗可能性。
            //if (ServiceSimulator.FAIL_RISE)
            //    throw ServiceSimulator.NewFailException("friends.GetFriends FAIL!");

            Person thePerson = friends.Where(c => c.Name == name).FirstOrDefault();
            if (thePerson == null)
                return null;
            // else
            return thePerson.GetFriends();
        }

        internal static Person[] Get(this List<Person> friends, Person me)
        {
            ////## 模擬失敗可能性。
            //if (ServiceSimulator.FAIL_RISE)
            //    throw ServiceSimulator.NewFailException("friends.GetFriends FAIL!");

            Person thePerson = friends.Where(c => c.Name == me.Name).FirstOrDefault();
            if (thePerson == null)
                return null;
            // else
            return thePerson.GetFriends();
        }
    }

    #endregion

    #endregion

    public static class ExtensionMethods
    {
        /// <remarks>
        /// Make TextBoxBase.AppendText() method can append fromated string.
        /// </remarks>
        internal static void AppendText(this TextBoxBase txt, string format, params object[] args)
        {
            txt.AppendText(string.Format(format, args));
        }       
    } 
}

//#region //## 模擬失敗可能性。
//if (FAIL_RISE)
//{
//    Random r = new Random();
//    switch (r.Next(3))
//    {
//        case 0:
//            throw new ApplicationException(string.Format("[{0}] SendMessage FAIL!", protocol));
//        case 1:
//            throw new IOException(string.Format("[{0}] SendMessage FAIL!", protocol));
//        case 2:
//            throw new DataException(string.Format("[{0}] SendMessage FAIL!", protocol));
//    }
//
//    //throw new StalenessException;
//    //throw new AvailablityException;
//    //throw new NetworkConnectivityException
//    //throw new StaleDataException --- java
//}
//#endregion 