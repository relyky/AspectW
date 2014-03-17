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
    public partial class ShowProgressForm : Form, IShowProgress
    {
        public ShowProgressForm()
        {
            InitializeComponent();
        }

        private void ShowProgressForm_Load(object sender, EventArgs e)
        {

        }

        #region realize interface IShowProgress

        float _progressMax = 0.0f;
        //public float progressMax
        //{
        //    get { return _progressMax; }
        //}

        float _progress = 0.0f;
        //public float progress
        //{
        //    get { return _progress; }
        //}

        public void Init(float progressMax)
        {
            _progressMax = progressMax;
            _progress = 0.0f;
            //
            this.Show();
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
            txtMessage.AppendText("完成。\r\n");
            this.Close();
        }

        #endregion


    }
}
