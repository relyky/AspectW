using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using NCCUCS.AspectW;

namespace DemoApp
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMessage.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyClass o1 = new MyClass(111, 1234.5678, "我是中文字。");

            // backup;
            MyClass c1 = (MyClass)o1.Clone();

            // dump o1.
            txtMessage.AppendText(o1.ToString() + "\r\n"); 

            // change content.
            o1.a = 222;
            o1.d = 2345.6789;
            o1.s = "我改變了";

            // restore
            o1 = c1;

            // dump o1.
            txtMessage.AppendText(o1.ToString() + "\r\n"); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyClass[] ary1 = new MyClass[5] { 
                new MyClass(1, 12.34, "一二三四"), 
                new MyClass(2, 23.45, "二三四五"), 
                new MyClass(3, 34.56, "三四五六"), 
                new MyClass(4, 45.67, "四五六七"), 
                new MyClass(5, 56.78, "五六七八") 
            };

            MyClass2 o1 = new MyClass2(111, 1234.5678, "我是中文字。", new string[] { "中１１１１", "中２２２２", "中３３３３" }, ary1);

            // backup;
            //MyClass2 c1 = (MyClass2)o1.Clone();
            MyClass2 c1 = o1.Clone2();
            //MyClass2 c1 = o1.DeepClone();

            // dump o1.
            txtMessage.AppendText(o1.ToString() + "\r\n"); 

            // do change.
            o1.a = 222;
            o1.d = 2345.6789;
            o1.s = "我改變了";
            o1.ss[1] = "我不再是我了";

            ary1[0].a = 999;
            ary1[0].d = 1234.5678;
            ary1[0].s = "我改變了";

            // restore
            o1 = c1;

            // dump o1.
            txtMessage.AppendText(o1.ToString() + "\r\n"); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int a;
            string s;
            MyClassA o;

            for (int i = 1; i <= 5; i++)
            {
                // set value
                a = 90 + i;
                s = "我是字串" + i.ToString();
                o = new MyClassA();
                o.s = "ＡＢＣＤ" + i.ToString();

                // dump
                txtMessage.AppendText(a + Environment.NewLine);
                txtMessage.AppendText(s + Environment.NewLine);
                o.Dump(txtMessage);

                AspectW.Define
                    .Ignore()
                    .Restore(new RestoreWhenFail(a, (v) => a = (int)v),
                                new RestoreWhenFail(s, (v) => s = (string)v),
                                new RestoreWhenFail(o, (v)=> o = (MyClassA)v))
                    .TraceException((ex) => txtMessage.AppendText("<<FAIL!>>" + Environment.NewLine + Environment.NewLine))
                    .Do(()=>
                    {
                        // change 
                        a = -77;
                        s = "我改變了";
                        o.a = 111;
                        o.d = 5678.1234;
                        o.m = 123456789.123456789123456789M;
                        o.s = "我改變了12345";
                        // dump
                        txtMessage.AppendText(a + Environment.NewLine);
                        txtMessage.AppendText(s + Environment.NewLine);
                        o.Dump(txtMessage);
                        // mail flal!
                        throw new ApplicationException("make FAIL!");
                    });

                // dump
                txtMessage.AppendText(a + Environment.NewLine);
                txtMessage.AppendText(s + Environment.NewLine);
                o.Dump(txtMessage);
                txtMessage.AppendText("=============================" + Environment.NewLine);
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int round = 1; round <= 3; round++)
            {
                // show round.
                txtMessage.AppendText(string.Format("## ROUND {0} ==================", round) + Environment.NewLine);

                int a = 99;
                MyClassA o = new MyClassA();
                // dump
                txtMessage.AppendText(a + Environment.NewLine);
                o.Dump(txtMessage);
                txtMessage.AppendText("-----------------------------" + Environment.NewLine);

                AspectW.Define
                    .Ignore()
                    .Restore(new RestoreWhenFail(a, (v) => a = (int)v),
                                new RestoreWhenFail(o, (v) => o = (MyClassA)v))
                    .TraceException((ex) => txtMessage.AppendText(ex.Message + Environment.NewLine + Environment.NewLine))
                    .Do(() =>
                    {
                        a = 77;
                        o.s = "我改變了，在Ｌ１。";
                        // dump
                        txtMessage.AppendText(a + Environment.NewLine);
                        o.Dump(txtMessage);
                        txtMessage.AppendText("-----------------------------" + Environment.NewLine);

                        //## mail flal at level 1 point A.
                        if (round == 1)
                            throw new ApplicationException("<<FAIL AT LEVEL1A>>");

                        AspectW.Define
                            .Ignore()
                            .Restore(new RestoreWhenFail(a, (v) => a = (int)v),
                                        new RestoreWhenFail(o, (v) => o = (MyClassA)v))
                            .TraceException((ex) => txtMessage.AppendText(ex.Message + Environment.NewLine + Environment.NewLine))
                            .Do(() =>
                            {
                                a = 55;
                                o.s = "我改變了，在Ｌ２。";
                                // dump
                                txtMessage.AppendText(a + Environment.NewLine);
                                o.Dump(txtMessage);
                                txtMessage.AppendText("-----------------------------" + Environment.NewLine);

                                //## mail flal at level 2.
                                if (round == 2)
                                    throw new ApplicationException("<<FAIL AT LEVEL2>>");
                            });

                        //## mail flal at level 1 point B.
                        if (round == 3)
                            throw new ApplicationException("<<FAIL AT LEVEL1B>>");
                    });

                // dump
                txtMessage.AppendText("final:" + Environment.NewLine);
                txtMessage.AppendText(a + Environment.NewLine);
                o.Dump(txtMessage);
                txtMessage.AppendText("=============================" + Environment.NewLine);

            }       
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int a = 99;
            MyClassA o = new MyClassA();
            // dump
            txtMessage.AppendText(a + Environment.NewLine);
            o.Dump(txtMessage);
            txtMessage.AppendText("-----------------------------" + Environment.NewLine);

            AspectW.Define
                .Ignore()
                .Restore(new RestoreWhenFail(a, (v) => a = (int)v),
                            new RestoreWhenFail(o, (v) => o = (MyClassA)v))
                .TraceException((ex) => txtMessage.AppendText(ex.Message + Environment.NewLine + Environment.NewLine))
                .Do(() =>
                {
                    a = 77;
                    o.s = "我改變了，在Ｂ１。";
                    // dump
                    txtMessage.AppendText(a + Environment.NewLine);
                    o.Dump(txtMessage);
                    txtMessage.AppendText("-----------------------------" + Environment.NewLine);

                    //## mail flal at level 1 point A.
                    throw new ApplicationException("<<FAIL AT BLOCK 1>>");
                });

            // dump
            txtMessage.AppendText(a + Environment.NewLine);
            o.Dump(txtMessage);
            txtMessage.AppendText("-----------------------------" + Environment.NewLine);
            txtMessage.AppendText("-----------------------------" + Environment.NewLine);

            AspectW.Define
                .Ignore()
                .Restore(new RestoreWhenFail(a, (v) => a = (int)v),
                            new RestoreWhenFail(o, (v) => o = (MyClassA)v))
                .TraceException((ex) => txtMessage.AppendText(ex.Message + Environment.NewLine + Environment.NewLine))
                .Do(() =>
                {
                    a = 55;
                    o.s = "我改變了，在Ｂ２。";
                    // dump
                    txtMessage.AppendText(a + Environment.NewLine);
                    o.Dump(txtMessage);
                    txtMessage.AppendText("-----------------------------" + Environment.NewLine);

                    //## mail flal at level 2.
                    throw new ApplicationException("<<FAIL AT BLOCK 2>>");
                });

            // dump
            txtMessage.AppendText("final:" + Environment.NewLine);
            txtMessage.AppendText(a + Environment.NewLine);
            o.Dump(txtMessage);
            txtMessage.AppendText("=============================" + Environment.NewLine);

        }
       
    }

    [Serializable]
    internal class MyClass2 : ICloneable
    {
        public int a;
        public double d;
        public string s;
        public string[] ss;
        public MyClass[] ary1;

        private readonly Guid _oid;
        public Guid OID {
            get { return _oid; }
        }

        public MyClass2(int _a, double _d, string _s, string[] _ss, MyClass[] _ary1)
        {
            _oid = Guid.NewGuid();
            a = _a;
            d = _d;
            s = _s;
            ss = _ss;
            ary1 = _ary1;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("OID = {0}, a = {1}, d = {2}, s = {3} \r\n", _oid.ToString().ToUpper(), a, d, s);
            for(int i = 0; i < ss.Length; i++)
                sb.AppendFormat("\tss[{0}] = {1} \r\n", i, ss[i] );
            foreach (var c in ary1)
                sb.AppendLine(c.ToString());

            return sb.ToString();
        }

        #region realize interface "ICloneable".
        public object Clone()
        {
            // deep clone.
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;
                return formatter.Deserialize(ms);
            }
        }
        #endregion

        /// <remarks>
        /// 不可靠的clone方式。
        /// </remarks>
        public MyClass2 Clone2()
        {
            MyClass2 c = new MyClass2(this.a, this.d, this.s, this.ss, this.ary1);
            c.ss = (string[])this.ss.Clone();
            c.ary1 = (MyClass[])this.ary1.Clone();

            return c;
        }
    }
}
