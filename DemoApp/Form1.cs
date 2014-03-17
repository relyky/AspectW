using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NCCUCS.AspectW;

namespace DemoApp
{
    public partial class Form1 : Form, IShowProgress
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Aspect Program

        //delegate void BeforeProceedDelegate(int a, int b);
        //delegate void AfterProceedDelegate();

        private void BeforeProceed2(decimal a)
        {
            txtMessage.AppendText(string.Format("ON : BeforeProceed() => {0}\r\n", a));
        }

        private void BeforeProceed(int a, int b)
        {
            txtMessage.AppendText(string.Format("ON : BeforeProceed() => {0}\r\n", a + b));
        }

        private void AfterProceed()
        {
            txtMessage.AppendText(string.Format("ON : AfterProceed() => {0}\r\n", "after"));
        }

        private void PrefixTrace()
        {
            txtMessage.AppendText("ON : PrefixMyTrace()...\r\n");
        }

        private void InsertLog(string log)
        {
            txtMessage.AppendText(log + "\r\n");
        }

        private void InsertLog(string logMessage, TimeSpan howlong)
        {
            txtMessage.AppendText(string.Format(logMessage, howlong));
        }

        private void TraceException(Exception ex)
        {
            txtMessage.AppendText(string.Format("EXCEPTION : {0} \r\n", ex.Message));
        }

        private void PostfixTrace()
        {
            txtMessage.AppendText("ON : PostfixTrace()...\r\n");
        }

        private bool ReConfirmToProceed()
        {
            //## 再確認是否真要執行。
            string message = "再確認要執行嗎？";
            DialogResult dlo = MessageBox.Show(message, "再確認一次", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dlo == DialogResult.Yes)
                return true;
            // otherwise
            return false;
        }

        private bool ReConfirmToProceed(string message)
        {
            //## 再確認是否真要執行。
            //string message = "再確認要執行嗎？";
            DialogResult dlo = MessageBox.Show(message, "再確認一次", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dlo == DialogResult.Yes)
                return true;
            // otherwise
            return false;
        }

        private bool ValidateInt(int i, int min, int max, string message)
        {
            bool condition = min <= i && i <= max;
            if (!condition)
                MessageBox.Show(message);

            return condition;
        }

        #endregion of Aspect Program

        #region realize interface IShowProgress

        float _progressMax = 0.0f;
        float _progress = 0.0f;

        public void Init(float progressMax)
        {
            _progressMax = progressMax;
            _progress = 0.0f;
        }

        public void Step()
        {
            _progress++;
        }

        public void ShowProgress()
        {
            txtMessage.AppendText((_progress / _progressMax).ToString("P0") + "\r\n"); // show progress
        }

        public void Finish()
        {
            txtMessage.AppendText("完成\r\n"); // show progress
            // just do nothing...
        }

        #endregion

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMessage.Clear();
        }

        private void btnTrace_Click(object sender, EventArgs e)
        {
            AspectW.Define
                //.BeforeProceed(new BeforeProceedDelegate(this.BeforeProceed), 5, 6)
                //.AfterProceed(new AfterProceedDelegate(this.AfterProceed))
                .TraceBefore(this.PrefixTrace)
                .TraceAfter(this.PostfixTrace)
                .TraceException(this.TraceException)
                .Trace(PrefixTrace, PostfixTrace)
                .Do(() =>
                {
                    txtMessage.AppendText("ON : button1_Click()...\r\n");
                });
        }

        private void btnRetryOnce_Click(object sender, EventArgs e)
        {
            bool failFlag = true;
            MyClass o1 = new MyClass(1, 12.34, "我是中文字");

            AspectW.Define
                .RetryOnce(1000)
                .Restore(new RestoreWhenFail(o1, (v)=>o1=(MyClass)v))
                .Do(() => 
                    {
                        txtMessage.AppendText(string.Format(
                            "ON : {0:HH:mm:ss} : btnRetryOnce_Click()...\r\n", DateTime.Now));

                        if(failFlag)
                        {
                            failFlag = false;
                            txtMessage.AppendText(string.Format("ON : {0:HH:mm:ss} : FAIL!\r\n", DateTime.Now));
                            throw new ApplicationException("FAIL!");
                        }

                        txtMessage.AppendText(string.Format(
                            "ON : {0:HH:mm:ss} : Success\r\n", DateTime.Now));
                    });
        }

        private void btnRetryParam_Click(object sender, EventArgs e)
        {
            try
            {
                ///<remark>
                ///string protocol = "HTTPS";
                ///try(protocol) {
                ///    SendMessage(friend, message, protocol);
                ///} catch (AvailablityException ex) {
                ///    Thread.Sleep(1000);
                ///    retry(protocol="HTTP");
                ///}
                ///<remark>

                string protocol = "HTTPS";
                AspectW.Define
                    .RetryParam(3000, ()=>protocol="MMS", ()=>protocol="HTTP")
                    .Do(() =>
                    {
                        // SendMessage(friend, message, protocol);
                        txtMessage.AppendText(string.Format(
                            "ON : {0:HH:mm:ss} : SendMessage(friend, message, '{1}')\r\n", DateTime.Now, protocol));

                        if (protocol != "HTTP")
                        {
                            // make fail!
                            throw new ApplicationException("FAIL!");
                        }

                        txtMessage.AppendText("SUCCESS\r\n");
                    });

            }
            catch (Exception ex)
            {
                txtMessage.AppendText("## Outside : " + ex.Message + Environment.NewLine);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            int failFlag = -3;
            int a = 9;
            string b = "我是字串。";

            txtMessage.AppendText(string.Format("a = {0}\r\n", a));
            txtMessage.AppendText(string.Format("b = {0}\r\n", b));

            AspectW.Define
                .Ignore()
                .Retry(1000,2)
                //.Restore(a, (v) => a = (int)v, b, (v) => b = (string)v) // 當 multi-retry 時，會有多次冗餘的 clone。clone只需一次即可。
                .Restore(new RestoreWhenFail(a, (v) => a = (int)v), 
                         new RestoreWhenFail(b, (v) => b = (string)v))
                .Trace(this.PrefixTrace, this.PostfixTrace, this.TraceException)
                .Do(()=>
                    {
                        a = 11;
                        b = "我改變了。";

                        if (failFlag++ < 0)
                        {
                            throw new ApplicationException("FAIL!");
                        }
                    });

            txtMessage.AppendText(string.Format("a = {0}\r\n", a));
            txtMessage.AppendText(string.Format("b = {0}\r\n", b));
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            txtMessage.AppendText("BEGIN\r\n");

            AspectW.Define
                .Ignore()
                .Do(() =>
                    {
                        throw new ApplicationException("FAIL!");
                    });

            txtMessage.AppendText("END\r\n");

        }

        private void btnRestore2_Click(object sender, EventArgs e)
        {
            bool failFlag = true;
            int a = 9;
            string b = "ABC我是中文字";
            double d = 5678.1234;

            txtMessage.AppendText(string.Format("TRACE : a = {0}\r\n", a));
            txtMessage.AppendText(string.Format("TRACE : b = {0}\r\n", b));
            txtMessage.AppendText(string.Format("TRACE : d = {0}\r\n", d));

            try
            {
                AspectW.Define
                    .Trace(PrefixTrace, PostfixTrace)
                    .Restore(new RestoreWhenFail(a, (v) => a = (int)v)
                            ,new RestoreWhenFail(b, (v) => b = (string)v)
                            ,new RestoreWhenFail(d, (v) => d =(double)v))
                    .Do(() =>
                    {
                        txtMessage.AppendText("ON : btnRestore2_Click()...\r\n");

                        a = 11;
                        b = "天天天";
                        d = 1234.5678;

                        if (failFlag)
                        {
                            failFlag = false;
                            throw new ApplicationException("FAIL!");
                        }
                    });
            }
            catch (Exception ex)
            {
                txtMessage.AppendText(string.Format("OUT-BOUNDS : {0}\r\n", ex.Message));
            }

            txtMessage.AppendText(string.Format("TRACE : a = {0}\r\n", a));
            txtMessage.AppendText(string.Format("TRACE : b = {0}\r\n", b));
            txtMessage.AppendText(string.Format("TRACE : d = {0}\r\n", d));
        }
        
        private void btnRestore3_Click(object sender, EventArgs e)
        {
            bool failFlag = true;
            MyClass o1 = new MyClass(9, 5678.1234, "ABC我是中文字");

            // dump
            txtMessage.AppendText(o1.ToString() + "\r\n");

            try
            {
                AspectW.Define
                    .Trace(PrefixTrace, PostfixTrace)
                    .Restore(new RestoreWhenFail(o1, (v) => o1 = (MyClass)v))
                    .Do(() =>
                    {
                        txtMessage.AppendText("ON : btnRestore3_Click()...\r\n");

                        o1.a = 999;
                        o1.d = 1234.5678;
                        o1.s = "我改變了";

                        if (failFlag)
                        {
                            failFlag = false;
                            throw new ApplicationException("FAIL!");
                        }
                    });
            }
            catch (Exception ex)
            {
                txtMessage.AppendText(string.Format("OUT-BOUNDS : {0}\r\n", ex.Message));
            }

            // dump
            txtMessage.AppendText(o1.ToString() + "\r\n");

        }

        private void btnRestore4_Click(object sender, EventArgs e)
        {
            bool failFlag = true;

            MyClass[] ary1 = new MyClass[5] { 
                new MyClass(1, 12.34, "一二三四"), 
                new MyClass(2, 23.45, "二三四五"), 
                new MyClass(3, 34.56, "三四五六"), 
                new MyClass(4, 45.67, "四五六七"), 
                new MyClass(5, 56.78, "五六七八") 
            };

            // dump
            DumpArray(ary1);

            try
            {
                AspectW.Define
                    .Trace(PrefixTrace, PostfixTrace)
                    .Restore(new RestoreArrayWhenFail(ary1, (v) => ary1 = (MyClass[])v))
                    .Do(() =>
                    {
                        txtMessage.AppendText("ON : btnRestore3_Click()...\r\n");

                        ary1[0].a = 999;
                        ary1[0].d = 1234.5678;
                        ary1[0].s = "我改變了";

                        if (failFlag)
                        {
                            failFlag = false;
                            throw new ApplicationException("FAIL!");
                        }
                    });
            }
            catch (Exception ex)
            {
                txtMessage.AppendText(string.Format("OUT-BOUNDS : {0}\r\n", ex.Message));
            }

            // dump
            DumpArray(ary1);
        }
        
        private void btnRestore5_Click(object sender, EventArgs e)
        {
            bool failFlag = true;
            int[] ary1 = new int[5] { 1, 2, 3, 4, 5 };

            // dump
            DumpArray(ary1);

            try
            {
                AspectW.Define
                    .Trace(PrefixTrace, PostfixTrace)
                    .Restore(new RestoreWhenFail(ary1, (v) => ary1 = (int[])v))
                    .Do(() =>
                    {
                        txtMessage.AppendText("ON : btnRestore3_Click()...\r\n");

                        ary1[0] = 999;

                        if (failFlag)
                        {
                            failFlag = false;
                            throw new ApplicationException("FAIL!");
                        }
                    });
            }
            catch (Exception ex)
            {
                txtMessage.AppendText(string.Format("OUT-BOUNDS : {0}\r\n", ex.Message));
            }

            // dump
            DumpArray(ary1);
        }

        private void btnRestore6_Click(object sender, EventArgs e)
        {
            bool failFlag = true;

            List<MyClass> lst1 = new List<MyClass>();
            lst1.Add(new MyClass(1, 12.34, "一二三四"));
            lst1.Add(new MyClass(2, 23.45, "二三四五")); 
            lst1.Add(new MyClass(3, 34.56, "三四五六")); 
            lst1.Add(new MyClass(4, 45.67, "四五六七"));
            lst1.Add(new MyClass(5, 56.78, "五六七八"));

            // dump
            DumpList(lst1);

            try
            {
                AspectW.Define
                    .Trace(PrefixTrace, PostfixTrace)
                    .Restore(new RestoreWhenFail(lst1, (v) => lst1 = (List<MyClass>)v))
                    .Do(() =>
                    {
                        txtMessage.AppendText("ON : btnRestore3_Click()...\r\n");

                        lst1[0].a = 999;
                        lst1[0].d = 1234.5678;
                        lst1[0].s = "我改變了";

                        if (failFlag)
                        {
                            failFlag = false;
                            throw new ApplicationException("FAIL!");
                        }
                    });
            }
            catch (Exception ex)
            {
                txtMessage.AppendText(string.Format("OUT-BOUNDS : {0}\r\n", ex.Message));
            }

            // dump
            DumpList(lst1);
        }

        private void btnRestore7_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("未實作 btnRestore7_Click()");

            bool failFlag = true;

            MyClass[][] ary1 = new MyClass[][] {
                 new MyClass[] { 
                    new MyClass(1, 12.34, "一二三四"), 
                    new MyClass(2, 23.45, "二三四五"), 
                    new MyClass(3, 34.56, "三四五六") 
                },
                new MyClass[] { 
                    new MyClass(4, 45.67, "四五六七"), 
                    new MyClass(5, 56.78, "五六七八") 
                }
            };

            // dump
            DumpArray(ary1);

            try
            {
                AspectW.Define
                    .Trace(PrefixTrace, PostfixTrace)
                    .Restore(new RestoreArrayWhenFail(ary1, (v) => ary1 = (MyClass[][])v))
                    .Do(() =>
                    {
                        txtMessage.AppendText("ON : btnRestore3_Click()...\r\n");

                        ary1[0][0].a = 999;
                        ary1[0][0].d = 1234.5678;
                        ary1[0][0].s = "我改變了";

                        if (failFlag)
                        {
                            failFlag = false;
                            throw new ApplicationException("FAIL!");
                        }
                    });
            }
            catch (Exception ex)
            {
                txtMessage.AppendText(string.Format("OUT-BOUNDS : {0}\r\n", ex.Message));
            }

            // dump
            DumpArray(ary1);
        }

        private void btnWhenTrue_Click(object sender, EventArgs e)
        {
            // simulate arguments/parameters
            int a = 99; 
            int b = 10;

            AspectW.Define
                .WhenTrue(ReConfirmToProceed("再確認要執行嗎？")) // 
                .WhenTrue(a == 99, b == 10) // verify parameters
                .WhenTrue(ValidateInt(a, 90, 100, "參數[a]的範圍不合格！")) // advance verify parameter
                .Do(() =>
                {
                    txtMessage.AppendText("ON : btnWhenTrue_Click\r\n");
                    txtMessage.AppendText(string.Format("\t a : {0}, b : {1} \r\n", a, b));
                });

            //===============================================

            //AspectW.Define
            //    .WhenTrue(ReConfirmToProceed("再確認要執行嗎？")) // true or false
            //    .Do(() =>
            //        {
            //            txtMessage.AppendText("ON : btnWhenTrue_Click\r\n");
            //        });

            //===============================================

            //int a = 99; // simulate arguments/parameters
            //int b = 10;
            //AspectW.Define
            //    .WhenTrue(a == 99, b == 10) // verify parameters
            //    .Do(()=>
            //    {
            //        txtMessage.AppendText("ON : btnWhenTrue_Click\r\n");
            //    });
        }

        private void btnUntil_Click(object sender, EventArgs e)
        {
            // simulate arguments/parameters
            AspectW.Define
                .Until(() =>
                    {
                        return ReConfirmToProceed("我可以執行了嗎？");
                    })
                .Do(() =>
                {
                    txtMessage.AppendText("ON : btnUntil_Click\r\n");
                    txtMessage.AppendText("終於可以打包下班了。\r\n");
                });
        }

        private void btnUntil2_Click(object sender, EventArgs e)
        {
            // simulate arguments/parameters
            AspectW.Define
                .WaitCursor(this, btnUntil2)
                .Until(() => IsDataReady())
                .Do(() =>
                {
                    txtMessage.AppendText("ON : btnUntil2_Click\r\n");
                    txtMessage.AppendText("終於可以打包下班了。\r\n");
                });
        }

        #region is data ready?

        private DateTime beginPrepareDataTime = DateTime.Now;

        private bool IsDataReady()
        {
            // 資料準備時間不到３秒，不準離開。
            if ((DateTime.Now - beginPrepareDataTime).Seconds <= 3)
            {
                // 工作中
                txtMessage.AppendText("等待資料中…\r\n"); // tracing
                return false;
            }
            // else
            beginPrepareDataTime = DateTime.Now; // 
            return true;
        }

        #endregion

        private void DumpList(IList lst)
        {
            txtMessage.AppendText("BEGIN dump array \r\n");
            foreach (var e in lst.AsQueryable())
                txtMessage.AppendText(e.ToString() + "\r\n");
            txtMessage.AppendText("END \r\n");
        }

        private void btnWaitCursor_Click(object sender, EventArgs e)
        {
            AspectW.Define
                .WaitCursor(this)
                .Do(() =>
                    {
                        txtMessage.AppendText(string.Format("BEGIN : {0:HH:mm:ss} : btnWaitCursor_Click()\r\n", DateTime.Now));

                        // biz logic.
                        Thread.Sleep(3000); // wait 3s.

                        txtMessage.AppendText(string.Format("END : {0:HH:mm:ss}\r\n", DateTime.Now));
                    });
        }

        private void btnShowProgress_Click(object sender, EventArgs e)
        {
            try
            {
                string[] datas = new string[] { "AA", "BB", "CC", "DD" };

                AspectW.Define
                    .WaitCursor(this)
                    .Ignore()
                    .TraceException(this.TraceException)
                    .DoStepByStep<string>(datas, new ShowProgressForm(), (c) =>
                    //.DoStepByStep<string>(datas, this, (c)=>
                    {
                        //# cursor biz ligic...
                        //txtMessage.AppendText(string.Format("c = {0}, progress = {1}, max = {2}\r\n", c, this.progress, this.progressMax));
                        if (c == "BB") throw new ApplicationException("FAIL!");
                        Thread.Sleep(1000);
                    });
            }
            catch (Exception ex)
            {
                txtMessage.AppendText(string.Format("<<OUTSIDE CATCH>> {0}\r\n\r\n", ex.Message));
            }

            //var entities = datas.AsEnumerable();
            ////---------------
            //float max = (float)entities.Count();
            //float progress = 0f;
            //txtMessage.AppendText((progress / max).ToString("P0") + "\r\n");
            //foreach (var c in entities)
            //{
            //    txtMessage.AppendText(string.Format("c -> {0},{1},{2}\r\n", c, progress, max));

            //    txtMessage.AppendText((++progress / max).ToString("P0") + "\r\n");
            //}
            //txtMessage.AppendText((progress / max).ToString("P0") + "\r\n");

        }

        private void btnTraceException_Click(object sender, EventArgs e)
        {
            // 注意：有順序關系
            AspectW.Define
                .Ignore()
                .TraceException(this.TraceException)
                .Do(() =>
                {
                    txtMessage.AppendText("ON : btnTraceException_Click\r\n");
                    throw new ApplicationException("FAIL!");
                });
        }

        private void btnParaRetry_Click(object sender, EventArgs e)
        {
            try
            {
                ///<remark>
                ///string protocol = "HTTPS";
                ///try(protocol) {
                ///    SendMessage(friend, message, protocol);
                ///} catch (AvailablityException ex) {
                ///    Thread.Sleep(1000);
                ///    retry(protocol="HTTP");
                ///}
                ///<remark>

                string protocol = "HTTPS";
                AspectW.Define
                    .RetryOnce(1000)
                    .Restore(new RestoreWhenFail("HTTP", (v) => protocol = (string)v)) // Parameterized Retry
                    .Do(() =>
                    {
                        // SendMessage(friend, message, protocol);
                        txtMessage.AppendText(string.Format(
                            "ON : {0:HH:mm:ss} : SendMessage(friend, message, '{1}')\r\n", DateTime.Now, protocol));

                        if (protocol == "HTTPS")
                        {
                            // make fail!
                            throw new ApplicationException("FAIL!");
                        }

                        txtMessage.AppendText("SUCCESS\r\n");
                    });

            }
            catch(Exception ex)
            {
                txtMessage.AppendText("## Outside : " + ex.Message + Environment.NewLine);
            }

        }

        private void btnREF_Click(object sender, EventArgs e)
        {
            int a = 33;
            Ref<int> ra = new Ref<int>(() => a, (v) => a = v);

            txtMessage.AppendText(string.Format("a = {0}\r\n", a));
            txtMessage.AppendText(string.Format("ra = {0}\r\n", ra.Value));
            
            ra.Value = 22;
 
            txtMessage.AppendText(string.Format("a = {0}\r\n", a));
            txtMessage.AppendText(string.Format("ra = {0}\r\n", ra.Value));
        }

        private void btnExceptionHandler_Click(object sender, EventArgs e)
        {
            try
            {
                // Enter-Leave
                // Try-Catch-Wait-Retry
                // Restore
                // Fail
                //Ignore

                AspectW.Define
                    .OnEnterLeave(null, null)
                    .Ignore()
                    .HandleFail((ex) => { return HandleFailMethod.Retry; })
                    .Retry(1000, 3)
                    .Restore(new RestoreWhenFail(null, null))
                    .Do(() =>
                    {

                    });

                AspectW.Define
                    .HandleFail(this.MyFailHandler)
                    .Do(() =>
                    {
                        txtMessage.AppendText("ON : btnExceptionHandler_Click\r\n");
                        throw new ApplicationException("FAIL!");
                    });
            }
            catch (Exception ex)
            {
                txtMessage.AppendText("## Outside : " + ex.Message + Environment.NewLine);
            }

        }

        private void btnMultiException_Click(object sender, EventArgs e)
        {
            try
            {
                int failFlag = 3;
                
                AspectW.Define
                    //.HandleException(this.MyExceptionHandler)
                    .Retry<AggregateException>(1000, 1, this.MyRecoveryAction)
                    .Retry<ApplicationException>(1000, 1, (ex) => MyRefreshData())
                    .Retry<IOException>(1000, 1, this.MyRecoveryAction, this.MyVoidFailHandler)
                    //.Retry<IOException>(1000, 1, this.MyRecoveryAction, (ex) => { return ExceptionProceedMethod.ThrowOut; })
                    .Do(() =>
                    {
                        txtMessage.AppendText("ON : btnMultiException_Click\r\n");

                        if (failFlag == 3)
                        {
                            failFlag = 2;
                            throw new IOException("FAIL!");
                        }
                        else if (failFlag == 2)
                        {
                            failFlag = 1;
                            throw new ApplicationException("FAIL!");
                        }
                        else if (failFlag == 1)
                        {
                            failFlag = 0;
                            throw new AggregateException("Agg FAIL!");
                        }

                        //throw new Exception("FAIL!!!");

                        txtMessage.AppendText("SUCCESS\r\n");
                    });

            }
            catch (Exception ex)
            {
                txtMessage.AppendText("## Outside : " + ex.Message + Environment.NewLine);
            }
        }

        //private FailProceedMethod MyFailHandler(Exception ex)
        //{
        //    txtMessage.AppendText("ON : MyFailHandler\r\n");
        //    ////## method 1 
        //    //return ExceptionProceedMethod.Ignore;
        //
        //    //## method 2 
        //    return FailProceedMethod.ThrowOut;
        //
        //    ////## method 3 : wait a short time and then retry.
        //    //Thread.Sleep(1000);
        //    //return ExceptionProceedMethod.Retry;
        //}

        private void MyRecoveryAction(Exception ex)
        {
            txtMessage.AppendText(string.Format(
                "ON : MyAppExceptionHandler[{0}]\r\n", ex.GetType().Name));

            txtMessage.AppendText(string.Format(
                "ON : {0:HH:mm:ss} : RefreshData(friend)\r\n", DateTime.Now));
        }

        private void MyRefreshData()
        {
            txtMessage.AppendText(string.Format(
                "ON : {0:HH:mm:ss} : MyRefreshData()\r\n", DateTime.Now));
        }

        private HandleFailMethod MyFailHandler(Exception ex)
        {
            txtMessage.AppendText(string.Format(
                "ON : {0:HH:mm:ss} : MyFailHandler()\r\n", DateTime.Now));

            return HandleFailMethod.ThrowOut;
        }

        private void MyVoidFailHandler(Exception ex)
        {
            txtMessage.AppendText(string.Format(
                "ON : {0:HH:mm:ss} : MyFailHandler()\r\n", DateTime.Now));
        }

        private void DumpArray(Array ary)
        {
            txtMessage.AppendText("BEGIN dump array \r\n");
            foreach (var e in ary)
            {
                txtMessage.AppendText(e.ToString() + "\r\n");
                if (e is Array)
                {
                    txtMessage.AppendText("\t");
                    DumpArray((Array)e);
                }
            }
            txtMessage.AppendText("END \r\n");
        }

        private void btnReconfirmIntent_Click(object sender, EventArgs e)
        {
            AspectW.Define
                .ReconfirmIntent()
                .Do(() =>
                {
                    txtMessage.AppendText("ON : btnReconfirmIntent_Click\r\n");
                });
        }

        private void btnAdTest_Click(object sender, EventArgs e)
        {
            AspectW.Define
                .TraceBefore(()=>this.InsertLog("Inserting customer the easy way"))
                .TraceHowLong(() => this.InsertLog("Starting customer insert"), 
                            (ts) => this.InsertLog("Inserted customer in {0} seconds", ts))
                .RetryOnce(1000)
                .Do(() =>
                    {
                        //CustomerData data = new CustomerData();
                        
                    });

            //Try/catch/retry/fail/finally
            //Enter-Try-Catch-Restore-Wait-Recovery-Retry-Fail-Ignore-Leave
            //Enter-Leave => OnEnterLeave
            //Try-Catch-Wait-Recovery-Retry => Retry, RetryParam
            //Restore => Restore
            //Fail => HandleFail
            //Ignore

            BarClass bar = new BarClass();
            AspectW.Define
                .OnEnterLeave(FooEnterAction, FooLeaveAction)
                .Ignore()
                .HandleFail(FooFailHandler)
                //.Retry(3000, 3, (ex) => FooRecoveryAction())
                .Retry(3000, 3)
                .Restore(new RestoreWhenFail(bar, (v)=>bar=(BarClass)v))
                .Do(() =>
                {
                    bar.DoSomeChange();
                });

            BarClass bar1 = new BarClass();
            BarClass bar2 = new BarClass();
            AspectW.Define
                .Retry<InvalidDataException>(3000, 3)
                .Retry<ApplicationException>(3000, 3)
                .Restore(new RestoreWhenFail(bar1, (v) => bar1 = (BarClass)v)
                        ,new RestoreWhenFail(bar2, (v) => bar2 = (BarClass)v))
                .Do(() =>
                {
                    bar1.DoSomeChange();
                    bar2.DoSomeChange();
                });

            BarClass bar3 = new BarClass();
            RestoreWhenFail res3 = new RestoreWhenFail(bar3, (v) => bar3 = (BarClass)v);
            AspectW.Define
                .Retry(3000, 3, (ex) => { FooFireCancelMissile(bar3); res3.Restore(); })
                .Do(() =>
                {
                    bar3.DoSomeChange();
                    FooFireUpdateMissile(bar3);
                });

            BarClass bar4 = new BarClass();
            string protocol = "HTTPS";
            AspectW.Define
                .Ignore()
                .RetryParam(3000, () => protocol = "HTTP")
                .Do(() =>
                {
                    FooFireUpdateMissile(bar4, protocol);
                });

        }

        class BarClass
        {
            public void DoSomeChange() { }
        }
        private void FooRecoveryAction() { }
        private void FooEnterAction() { }
        private void FooLeaveAction() { }
        private HandleFailMethod FooFailHandler(Exception ex) { return HandleFailMethod.ThrowOut; }

        private void FooFireUpdateMissile(BarClass bar) { }
        private void FooFireUpdateMissile(BarClass bar, string protocol) { }
        private void FooFireCancelMissile(BarClass bar) { }

        /// <summary>
        /// 無意義，只是用來寫文件的
        /// </summary>
        public void InsertCustomerTheEasyWay(string firstName, string lastName, int age
            , Dictionary<string, string> attributes)
        {
            AspectW.Define
                .TraceBefore(() => this.InsertLog("Inserting customer the easy way"))
                .TraceHowLong(() => this.InsertLog("Starting customer insert"),
                             (ts) => this.InsertLog("Inserted customer in {0} seconds", ts))
                .RetryOnce(1000)
                .Do(() =>
                {
                    CustomerData data = new CustomerData();
                    data.Insert(firstName, lastName, attributes);
                });
        }

        class CustomerData
        {
            public void Insert(string firstName, string lastName, Dictionary<string, string> attributes)
            {
            }
        }

        //private void btn1MustBeNonNull_Click(object sender, EventArgs e)
        //{
        //    // simulate arguments/parameters
        //    int a = 99;
        //    int b = 10;
        //    string s = string.Empty;
        //    MyClass mc = null;
        //
        //    AspectW.Define
        //        .Ignore()
        //        .TraceException((ex)=>txtMessage.AppendText(ex.Message + "\r\n"))
        //        .Validate(a == null || a.Equals(default(int)), "badafadfadf")
        //        .MustBeNonNull(s, mc)
        //        .MustBeNonDefault<int>(a,b)
        //        .Do(() =>
        //        {
        //            txtMessage.AppendText("ON : btn1MustBeNonNull_Click\r\n");
        //            txtMessage.AppendText(string.Format("\t a : {0}, b : {1}, s : {2}\r\n", a, b, s));
        //        });
        //}

        //private void btnMustBeNonDefault_Click(object sender, EventArgs e)
        //{
        //    // simulate arguments/parameters
        //    int a = 99;
        //    int b = default(int);
        //
        //    AspectW.Define
        //        .Ignore()
        //        .TraceException((ex) => txtMessage.AppendText(ex.Message + "\r\n"))
        //        //.MustBeNonDefault<int>(a,b)
        //        .Do(() =>
        //        {
        //            txtMessage.AppendText("ON : btnMustBeNonDefault_Click\r\n");
        //            txtMessage.AppendText(string.Format("\t a : {0}, b : {1} \r\n", a, b));
        //        });
        //}

        //private void btnMustBeNonDefault2_Click(object sender, EventArgs e)
        //{
        //    // simulate arguments/parameters
        //    int a = 99;
        //    int b = default(int);
        //    string s = default(string);
        //    MyClassA ma = new MyClassA();
        //
        //    AspectW.Define
        //        .Ignore()
        //        .TraceException((ex) => txtMessage.AppendText(ex.Message + "\r\n"))
        //        .MustBeNonDefault(ma)
        //        //.MustBeNonDefault(a, b, s)
        //        .Do(() =>
        //        {
        //            txtMessage.AppendText("ON : btnMustBeNonDefault2_Click\r\n");
        //            txtMessage.AppendText(string.Format("\t a : {0}, b : {1}, s : {2}\r\n", a, b, s));
        //        });
        //}

    }

    internal sealed class Ref<T>
    {
        private Func<T> getter;
        private Action<T> setter;

        public Ref(Func<T> getter, Action<T> setter)
        {
            this.setter = setter;
            this.getter = getter;
        }

        public T Value
        {
            get
            {
                return getter();
            }
            set
            {
                setter(value);
            }
        }
    }

    [Serializable]
    internal class MyClass : ICloneable
    {
        public int a;
        public double d;
        public string s;

        private readonly Guid _oid;
        public Guid OID {
            get { return _oid; }
        }

        public MyClass(int _a,double _d,string _s)
        {
            _oid = Guid.NewGuid();
            a = _a;
            d = _d;
            s = _s;
        }

        public override string ToString()
        {
            return string.Format("OID = {0}, a = {1}, d = {2}, s = {3} ", _oid.ToString().ToUpper(), a, d, s);
        }

        #region realize interface "ICloneable".
        public object Clone()
        {
            return new MyClass(a, d, s);
        }
        #endregion
    }

}
