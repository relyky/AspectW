using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DemoApp
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            //Application.Run(new Form5());
            //Application.Run(new Form4());
            //Application.Run(new Form3()); // test clone methods
            //Application.Run(new Form2());
            //Application.Run(new Form1());
        }
    }
}
