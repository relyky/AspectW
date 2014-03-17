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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //## testing target : primitive type 
            // init.
            int a = 999;
            double d = 1234.5678;
            decimal m = 987654321987654321.987654321M;
            string s = "這是字串。";

            // dump
            txtMessage.AppendText("INITIAL : Dump primitive type values\r\n");
            txtMessage.AppendText("a = " + a.ToString() + "\r\n");
            txtMessage.AppendText("d = " + d.ToString() + "\r\n");
            txtMessage.AppendText("m = " + m.ToString() + "\r\n");
            txtMessage.AppendText("s = " + s.ToString() + "\r\n\r\n");

            AspectW.Define
                .Ignore()
                .Restore(new RestoreWhenFail(a, (v) => a = (int)v),
                         new RestoreWhenFail(d, (v) => d = (double)v),
                         new RestoreWhenFail(m, (v) => m = (decimal)v),
                         new RestoreWhenFail(s, (v) => s = (string)v))
                .TraceException((ex) => txtMessage.AppendText("EXCEPTION : " + ex.Message + "\r\n\r\n"))
                .Do(()=>
                {
                    // change value
                    a = 111;
                    d = 5678.1234;
                    m = 123456789.123456789123456789M;
                    s = "我改變了。";

                    // dump
                    txtMessage.AppendText("CHANGE : Dump primitive type values\r\n");
                    txtMessage.AppendText("a = " + a.ToString() + "\r\n");
                    txtMessage.AppendText("d = " + d.ToString() + "\r\n");
                    txtMessage.AppendText("m = " + m.ToString() + "\r\n");
                    txtMessage.AppendText("s = " + s.ToString() + "\r\n\r\n");

                    // make fail!
                    throw new ApplicationException("Make fail to trigger restore!");
                });

            // dump
            txtMessage.AppendText("RESTORE : Dump primitive type values\r\n");
            txtMessage.AppendText("a = " + a.ToString() + "\r\n");
            txtMessage.AppendText("d = " + d.ToString() + "\r\n");
            txtMessage.AppendText("m = " + m.ToString() + "\r\n");
            txtMessage.AppendText("s = " + s.ToString() + "\r\n\r\n");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //## testing target : object
            // init.
            MyClassA o = new MyClassA();

            // dump
            txtMessage.AppendText("\r\nINITIAL ");
            o.Dump(txtMessage);

            AspectW.Define
                .Ignore()
                .Restore(new RestoreWhenFail(o, (v) => o = (MyClassA)v))
                .TraceException((ex) => txtMessage.AppendText("\r\nEXCEPTION : " + ex.Message + "\r\n"))
                .Do(() =>
                {
                    // change value
                    o.a = 111;
                    o.d = 5678.1234;
                    o.m = 123456789.123456789123456789M;
                    o.s = "我改變了。";

                    // dump
                    txtMessage.AppendText("\r\nCHANGE ");
                    o.Dump(txtMessage);

                    // make fail!
                    throw new ApplicationException("Make fail to trigger restore!");
                });

            // dump
            txtMessage.AppendText("\r\nRESTORE ");
            o.Dump(txtMessage);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //## testing target : primitive type array
            // init.
            string [] strs = new string[] {"this is string 1.", "this is string 2.", "this is string 3."};

            // dump
            txtMessage.AppendText("INITIAL : \r\n");
            foreach(string s in strs)
                txtMessage.AppendText("s = " + s + "\r\n");
            txtMessage.AppendText("\r\n");

            AspectW.Define
                .Ignore()
                .Restore(new RestoreWhenFail(strs, (v) => strs = (string[])v)) // 
                .TraceException((ex) => txtMessage.AppendText("EXCEPTION : " + ex.Message + "\r\n\r\n"))
                .Do(() =>
                {
                    // change value
                    strs[0] = "我改變了。";
                    strs[1] = "我改變了。";
                    strs[2] = "我改變了。";

                    // dump
                    txtMessage.AppendText("CHANGE : \r\n");
                    foreach (string s in strs)
                        txtMessage.AppendText("s = " + s + "\r\n");
                    txtMessage.AppendText("\r\n");

                    // make fail!
                    throw new ApplicationException("Make fail to trigger restore!");
                });

            // dump
            txtMessage.AppendText("RESTORE : \r\n");
            foreach (string s in strs)
                txtMessage.AppendText("s = " + s + "\r\n");
            txtMessage.AppendText("\r\n");


        }

        private void button4_Click(object sender, EventArgs e)
        {
            //## testing target : object array
            // init.
            MyClassA[] oary = new MyClassA[] {new MyClassA(), new MyClassA(), new MyClassA() };

            // dump
            txtMessage.AppendText("====== INITIAL ====== \r\n");
            foreach (MyClassA o in oary)
                o.Dump(txtMessage);
            txtMessage.AppendText("\r\n");

            AspectW.Define
            .Ignore()
            .Restore(new RestoreWhenFail(oary, (v)=> oary = (MyClassA[])v)) // MyClassA 必須支援[Serializable]。
            .TraceException((ex) => txtMessage.AppendText("EXCEPTION : " + ex.Message + "\r\n\r\n"))
            .Do(() =>
            {
                // change value
                oary[0].a = 111;
                oary[0].d = 5678.1234;
                oary[0].m = 123456789.123456789123456789M;
                oary[0].s = "我改變了。";

                // change value
                oary[1].a = 111;
                oary[1].d = 5678.1234;
                oary[1].m = 123456789.123456789123456789M;
                oary[1].s = "我改變了。";

                // change value
                oary[2].a = 111;
                oary[2].d = 5678.1234;
                oary[2].m = 123456789.123456789123456789M;
                oary[2].s = "我改變了。";

                // dump
                txtMessage.AppendText("====== CHANGE ====== \r\n");
                foreach (MyClassA o in oary)
                    o.Dump(txtMessage);
                txtMessage.AppendText("\r\n");

                // make fail!
                throw new ApplicationException("Make fail to trigger restore!");
            });

            // dump
            txtMessage.AppendText("====== RESTORE ====== \r\n");
            foreach (MyClassA o in oary)
                o.Dump(txtMessage);
            txtMessage.AppendText("\r\n");
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMessage.Clear();
        }

    }

    [Serializable]
    class MyClassA 
    {
        public readonly Guid _guid = Guid.NewGuid();
        public int a = 999;
        public double d = 1234.5678;
        public decimal m = 987654321987654321.987654321M;
        public string s = "這是字串。";

        public MyClassA()
        {
            _guid = Guid.NewGuid();
        }

        public void ChangeToValue1()
        {
            // change value
            this.a = 111;
            this.d = 5678.1234;
            this.m = 123456789.123456789123456789M;
            this.s = "我改變了。";
        }

        public void Dump(TextBox txtMessage)
        {
            txtMessage.AppendText("DUMP<MyClassA>: \r\n");
            txtMessage.AppendText("  guid : " + _guid.ToString().ToUpper() + "\r\n");
            txtMessage.AppendText("  a = " + a.ToString() + "\r\n");
            txtMessage.AppendText("  d = " + d.ToString() + "\r\n");
            txtMessage.AppendText("  m = " + m.ToString() + "\r\n");
            txtMessage.AppendText("  s = " + s.ToString() + "\r\n");
        }

    }

}
