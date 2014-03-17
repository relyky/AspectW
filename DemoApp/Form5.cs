using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NCCUCS.AspectW;

namespace DemoApp
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // show code
            webBrowser1.DocumentText = "<HTML></HTML>"; // 一定要有這行。怪!!!
            webBrowser1.Document.OpenNew(true);
            webBrowser1.Document.Write(Properties.Resources.TextFile1);
        }

        #region AOP Program

        private void MyTrace(string message, params object[] args)
        {
            //txtMessage.AppendText(string.Format("[{0:HH:mm:ss}] ", DateTime.Now));
            txtMessage.AppendText(string.Format(message, args) + Environment.NewLine);
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            // show code
            //webBrowser1.DocumentText = "<HTML></HTML>"; // 一定要有這行。怪!!!
            webBrowser1.Document.OpenNew(true);
            webBrowser1.Document.Write(Properties.Resources.TextFile1);

            // declare variable
            int a;
            string s;
            MyClassA o;

            for (int i = 1; i <= 3; i++)
            {
                // set value
                a = 90 + i;
                s = "我是字串" + i.ToString();
                o = new MyClassA();
                o.s = "ABCD" + i.ToString();
                // dump
                this.MyTrace("SET VALUE:");
                this.MyTrace("a = {0}", a);
                this.MyTrace("s = {0}", s);
                o.Dump(txtMessage);
                this.MyTrace("-----------------------------");

                AspectW.Define
                    .Ignore()
                    .Restore(new RestoreWhenFail(a, (v) => a = (int)v),
                             new RestoreWhenFail(s, (v) => s = (string)v),
                             new RestoreWhenFail(o, (v) => o = (MyClassA)v))
                    .TraceException((ex) => this.MyTrace(ex.Message))
                    .Do(() =>
                    {
                        // change 
                        a = -77;
                        s = "我改變了";
                        o.a = 111;
                        o.d = 5678.1234;
                        o.m = 123456789.123456789123456789M;
                        o.s = "我改變了ABCD";
                        // dump
                        this.MyTrace("DO:");
                        this.MyTrace("a = {0}", a);
                        this.MyTrace("s = {0}", s);
                        o.Dump(txtMessage);
                        this.MyTrace("-----------------------------");

                        //## Make Fail.
                        if(i == 1 && chkMakeFail1.Checked) 
                            throw new ApplicationException("\r\n<<FAIL c1>>\r\n");
                        else if (i == 2 && chkMakeFail2.Checked)
                            throw new ApplicationException("\r\n<<FAIL c2>>\r\n");
                        else if (i == 3 && chkMakeFail3.Checked)
                            throw new ApplicationException("\r\n<<FAIL c3>>\r\n");
                    });

                // dump
                this.MyTrace("FINAL:");
                this.MyTrace("a = {0}", a);
                this.MyTrace("s = {0}", s);
                o.Dump(txtMessage);
                this.MyTrace("=============================\r\n");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // show code
            //webBrowser1.DocumentText = "<HTML></HTML>"; // 一定要有這行。怪!!!
            webBrowser1.Document.OpenNew(true);
            webBrowser1.Document.Write(Properties.Resources.TextFile2);

            // declare variable
            int a = 99;
            MyClassA o = new MyClassA();
            // dump
            this.MyTrace("INIT:");
            this.MyTrace("a = {0}", a);
            o.Dump(txtMessage);
            this.MyTrace("-----------------------------");

            AspectW.Define
                .Ignore()
                .Restore(new RestoreWhenFail(a, (v) => a = (int)v),
                         new RestoreWhenFail(o, (v) => o = (MyClassA)v))
                .TraceException((ex) => this.MyTrace(ex.Message))
                .Do(() =>
                {
                    // change value
                    a = 77;
                    o.s = "我改變了，在Ｌ１。";
                    // dump
                    this.MyTrace("DO at L1:");
                    this.MyTrace("a = {0}", a);
                    o.Dump(txtMessage);
                    this.MyTrace("-----------------------------");

                    //## Make Fail Point 1.
                    if (chkMakeFail1.Checked)
                        throw new ApplicationException("\r\n<<FAIL AT LEVEL１Ａ>>\r\n");

                    AspectW.Define
                        .Ignore()
                        .Restore(new RestoreWhenFail(a, (v)=> a = (int)v),
                                 new RestoreWhenFail(o, (v)=> o = (MyClassA)v))
                        .TraceException((ex) => this.MyTrace(ex.Message))
                        .Do(() =>
                        {
                            // change value
                            a = 55;
                            o.s = "我改變了，在Ｌ２。";
                            // dump
                            this.MyTrace("DO at L2:");
                            this.MyTrace("a = {0}", a);
                            o.Dump(txtMessage);
                            this.MyTrace("-----------------------------");

                            //## Make Fail Point 2.
                            if (chkMakeFail2.Checked)
                                throw new ApplicationException("\r\n<<FAIL AT LEVEL２>>\r\n");
                        });

                    //## Make Fail Point 3.
                    if (chkMakeFail3.Checked)
                        throw new ApplicationException("\r\n<<FAIL AT LEVEL１Ｂ>>\r\n");
                });

            // dump
            this.MyTrace("FINAL:");
            this.MyTrace("a = {0}", a);
            o.Dump(txtMessage);
            this.MyTrace("=============================\r\n");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // show code
            //webBrowser1.DocumentText = "<HTML></HTML>"; // 一定要有這行。怪!!!
            webBrowser1.Document.OpenNew(true);
            webBrowser1.Document.Write(Properties.Resources.TextFile3);

            // declare variable
            int a = 99;
            MyClassA o = new MyClassA();
            // dump
            this.MyTrace("INIT:");
            this.MyTrace("a = {0}", a);
            o.Dump(txtMessage);
            this.MyTrace("-----------------------------");

            AspectW.Define
                .Ignore()
                .Restore(new RestoreWhenFail(a, (v) => a = (int)v),
                         new RestoreWhenFail(o, (v) => o = (MyClassA)v))
                .TraceException((ex) => this.MyTrace(ex.Message))
                .Do(() =>
                {
                    // change value
                    a = 77;
                    o.s = "我改變了，在Ｂ１。";
                    // dump
                    this.MyTrace("DO BLOCK 1:");
                    this.MyTrace("a = {0}", a);
                    o.Dump(txtMessage);
                    this.MyTrace("-----------------------------");

                    //## Make Fail Point 1.
                    if(chkMakeFail1.Checked)
                        throw new ApplicationException("\r\n<<FAIL AT BLOCK 1>>\r\n");
                });

            // dump
            this.MyTrace("MIDDLE:");
            this.MyTrace("a = {0}", a);
            o.Dump(txtMessage);
            this.MyTrace("*****************************");

            AspectW.Define
                .Ignore()
                .Restore(new RestoreWhenFail(a, (v) => a = (int)v),
                         new RestoreWhenFail(o, (v) => o = (MyClassA)v))
                .TraceException((ex) => this.MyTrace(ex.Message))
                .Do(() =>
                {
                    // change value
                    a = 55;
                    o.s = "我改變了，在Ｂ２。";
                    // dump
                    this.MyTrace("DO BLOCK 2:");
                    this.MyTrace("a = {0}", a);
                    o.Dump(txtMessage);
                    this.MyTrace("-----------------------------");

                    //## Make Fail Point 2.
                    if (chkMakeFail2.Checked)
                        throw new ApplicationException("\r\n<<FAIL AT BLOCK 2>>\r\n");
                });

            // dump
            this.MyTrace("FINAL:");
            this.MyTrace("a = {0}", a);
            o.Dump(txtMessage);
            this.MyTrace("=============================\r\n");
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMessage.Clear();
        }
    }
}
