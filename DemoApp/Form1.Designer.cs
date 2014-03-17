namespace DemoApp
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTrace = new System.Windows.Forms.Button();
            this.btnRetryOnce = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnIgnore = new System.Windows.Forms.Button();
            this.btnRestore2 = new System.Windows.Forms.Button();
            this.btnWhenTrue = new System.Windows.Forms.Button();
            this.btnWaitCursor = new System.Windows.Forms.Button();
            this.btnShowProgress = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUntil2 = new System.Windows.Forms.Button();
            this.btnReconfirmIntent = new System.Windows.Forms.Button();
            this.btnUntil = new System.Windows.Forms.Button();
            this.btnRetryParam = new System.Windows.Forms.Button();
            this.btnRestore7 = new System.Windows.Forms.Button();
            this.btnRestore6 = new System.Windows.Forms.Button();
            this.btnRestore5 = new System.Windows.Forms.Button();
            this.btnRestore4 = new System.Windows.Forms.Button();
            this.btnRestore3 = new System.Windows.Forms.Button();
            this.btnExceptionHandler = new System.Windows.Forms.Button();
            this.btnREF = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnMultiException = new System.Windows.Forms.Button();
            this.btnParaRetry = new System.Windows.Forms.Button();
            this.btnTraceException = new System.Windows.Forms.Button();
            this.btnAdTest = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMessage
            // 
            this.txtMessage.ContextMenuStrip = this.contextMenuStrip1;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Location = new System.Drawing.Point(0, 240);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(4);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(908, 317);
            this.txtMessage.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearAllToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearAllToolStripMenuItem.Text = "Clear All";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // btnTrace
            // 
            this.btnTrace.Location = new System.Drawing.Point(741, 8);
            this.btnTrace.Margin = new System.Windows.Forms.Padding(4);
            this.btnTrace.Name = "btnTrace";
            this.btnTrace.Size = new System.Drawing.Size(154, 31);
            this.btnTrace.TabIndex = 1;
            this.btnTrace.Text = "Trace";
            this.btnTrace.UseVisualStyleBackColor = true;
            this.btnTrace.Click += new System.EventHandler(this.btnTrace_Click);
            // 
            // btnRetryOnce
            // 
            this.btnRetryOnce.Location = new System.Drawing.Point(741, 44);
            this.btnRetryOnce.Margin = new System.Windows.Forms.Padding(4);
            this.btnRetryOnce.Name = "btnRetryOnce";
            this.btnRetryOnce.Size = new System.Drawing.Size(154, 31);
            this.btnRetryOnce.TabIndex = 2;
            this.btnRetryOnce.Text = "RetryOnce";
            this.btnRetryOnce.UseVisualStyleBackColor = true;
            this.btnRetryOnce.Click += new System.EventHandler(this.btnRetryOnce_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(18, 16);
            this.btnRestore.Margin = new System.Windows.Forms.Padding(4);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(172, 31);
            this.btnRestore.TabIndex = 3;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnIgnore
            // 
            this.btnIgnore.Location = new System.Drawing.Point(421, 8);
            this.btnIgnore.Margin = new System.Windows.Forms.Padding(4);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Size = new System.Drawing.Size(152, 31);
            this.btnIgnore.TabIndex = 4;
            this.btnIgnore.Text = "Ignore";
            this.btnIgnore.UseVisualStyleBackColor = true;
            this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
            // 
            // btnRestore2
            // 
            this.btnRestore2.Location = new System.Drawing.Point(18, 49);
            this.btnRestore2.Margin = new System.Windows.Forms.Padding(4);
            this.btnRestore2.Name = "btnRestore2";
            this.btnRestore2.Size = new System.Drawing.Size(172, 31);
            this.btnRestore2.TabIndex = 5;
            this.btnRestore2.Text = "Restore2 - primitive";
            this.btnRestore2.UseVisualStyleBackColor = true;
            this.btnRestore2.Click += new System.EventHandler(this.btnRestore2_Click);
            // 
            // btnWhenTrue
            // 
            this.btnWhenTrue.Location = new System.Drawing.Point(421, 44);
            this.btnWhenTrue.Margin = new System.Windows.Forms.Padding(4);
            this.btnWhenTrue.Name = "btnWhenTrue";
            this.btnWhenTrue.Size = new System.Drawing.Size(152, 31);
            this.btnWhenTrue.TabIndex = 6;
            this.btnWhenTrue.Text = "WhenTrue";
            this.btnWhenTrue.UseVisualStyleBackColor = true;
            this.btnWhenTrue.Click += new System.EventHandler(this.btnWhenTrue_Click);
            // 
            // btnWaitCursor
            // 
            this.btnWaitCursor.Location = new System.Drawing.Point(741, 119);
            this.btnWaitCursor.Margin = new System.Windows.Forms.Padding(4);
            this.btnWaitCursor.Name = "btnWaitCursor";
            this.btnWaitCursor.Size = new System.Drawing.Size(154, 31);
            this.btnWaitCursor.TabIndex = 7;
            this.btnWaitCursor.Text = "WaitCursor";
            this.btnWaitCursor.UseVisualStyleBackColor = true;
            this.btnWaitCursor.Click += new System.EventHandler(this.btnWaitCursor_Click);
            // 
            // btnShowProgress
            // 
            this.btnShowProgress.Location = new System.Drawing.Point(581, 83);
            this.btnShowProgress.Margin = new System.Windows.Forms.Padding(4);
            this.btnShowProgress.Name = "btnShowProgress";
            this.btnShowProgress.Size = new System.Drawing.Size(152, 31);
            this.btnShowProgress.TabIndex = 8;
            this.btnShowProgress.Text = "Show Progress";
            this.btnShowProgress.UseVisualStyleBackColor = true;
            this.btnShowProgress.Click += new System.EventHandler(this.btnShowProgress_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUntil2);
            this.panel1.Controls.Add(this.btnReconfirmIntent);
            this.panel1.Controls.Add(this.btnUntil);
            this.panel1.Controls.Add(this.btnRetryParam);
            this.panel1.Controls.Add(this.btnRestore7);
            this.panel1.Controls.Add(this.btnRestore6);
            this.panel1.Controls.Add(this.btnRestore5);
            this.panel1.Controls.Add(this.btnRestore4);
            this.panel1.Controls.Add(this.btnRestore3);
            this.panel1.Controls.Add(this.btnExceptionHandler);
            this.panel1.Controls.Add(this.btnREF);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnTraceException);
            this.panel1.Controls.Add(this.btnWaitCursor);
            this.panel1.Controls.Add(this.btnTrace);
            this.panel1.Controls.Add(this.btnShowProgress);
            this.panel1.Controls.Add(this.btnRetryOnce);
            this.panel1.Controls.Add(this.btnRestore2);
            this.panel1.Controls.Add(this.btnWhenTrue);
            this.panel1.Controls.Add(this.btnRestore);
            this.panel1.Controls.Add(this.btnIgnore);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(908, 240);
            this.panel1.TabIndex = 9;
            // 
            // btnUntil2
            // 
            this.btnUntil2.Location = new System.Drawing.Point(261, 80);
            this.btnUntil2.Margin = new System.Windows.Forms.Padding(4);
            this.btnUntil2.Name = "btnUntil2";
            this.btnUntil2.Size = new System.Drawing.Size(152, 31);
            this.btnUntil2.TabIndex = 22;
            this.btnUntil2.Text = "Until - 3 seconds";
            this.btnUntil2.UseVisualStyleBackColor = true;
            this.btnUntil2.Click += new System.EventHandler(this.btnUntil2_Click);
            // 
            // btnReconfirmIntent
            // 
            this.btnReconfirmIntent.Location = new System.Drawing.Point(261, 8);
            this.btnReconfirmIntent.Margin = new System.Windows.Forms.Padding(4);
            this.btnReconfirmIntent.Name = "btnReconfirmIntent";
            this.btnReconfirmIntent.Size = new System.Drawing.Size(152, 31);
            this.btnReconfirmIntent.TabIndex = 21;
            this.btnReconfirmIntent.Text = "Reconfirm Intent";
            this.btnReconfirmIntent.UseVisualStyleBackColor = true;
            this.btnReconfirmIntent.Click += new System.EventHandler(this.btnReconfirmIntent_Click);
            // 
            // btnUntil
            // 
            this.btnUntil.Location = new System.Drawing.Point(261, 44);
            this.btnUntil.Margin = new System.Windows.Forms.Padding(4);
            this.btnUntil.Name = "btnUntil";
            this.btnUntil.Size = new System.Drawing.Size(152, 31);
            this.btnUntil.TabIndex = 20;
            this.btnUntil.Text = "Until - I am happy";
            this.btnUntil.UseVisualStyleBackColor = true;
            this.btnUntil.Click += new System.EventHandler(this.btnUntil_Click);
            // 
            // btnRetryParam
            // 
            this.btnRetryParam.Location = new System.Drawing.Point(741, 83);
            this.btnRetryParam.Margin = new System.Windows.Forms.Padding(4);
            this.btnRetryParam.Name = "btnRetryParam";
            this.btnRetryParam.Size = new System.Drawing.Size(154, 31);
            this.btnRetryParam.TabIndex = 19;
            this.btnRetryParam.Text = "RetryParam";
            this.btnRetryParam.UseVisualStyleBackColor = true;
            this.btnRetryParam.Click += new System.EventHandler(this.btnRetryParam_Click);
            // 
            // btnRestore7
            // 
            this.btnRestore7.Location = new System.Drawing.Point(378, 119);
            this.btnRestore7.Margin = new System.Windows.Forms.Padding(4);
            this.btnRestore7.Name = "btnRestore7";
            this.btnRestore7.Size = new System.Drawing.Size(172, 31);
            this.btnRestore7.TabIndex = 18;
            this.btnRestore7.Text = "Restore7 - 2D array";
            this.btnRestore7.UseVisualStyleBackColor = true;
            this.btnRestore7.Click += new System.EventHandler(this.btnRestore7_Click);
            // 
            // btnRestore6
            // 
            this.btnRestore6.Location = new System.Drawing.Point(558, 119);
            this.btnRestore6.Margin = new System.Windows.Forms.Padding(4);
            this.btnRestore6.Name = "btnRestore6";
            this.btnRestore6.Size = new System.Drawing.Size(172, 31);
            this.btnRestore6.TabIndex = 17;
            this.btnRestore6.Text = "Restore6 - list";
            this.btnRestore6.UseVisualStyleBackColor = true;
            this.btnRestore6.Click += new System.EventHandler(this.btnRestore6_Click);
            // 
            // btnRestore5
            // 
            this.btnRestore5.Location = new System.Drawing.Point(18, 119);
            this.btnRestore5.Margin = new System.Windows.Forms.Padding(4);
            this.btnRestore5.Name = "btnRestore5";
            this.btnRestore5.Size = new System.Drawing.Size(172, 31);
            this.btnRestore5.TabIndex = 16;
            this.btnRestore5.Text = "Restore5 - int[]";
            this.btnRestore5.UseVisualStyleBackColor = true;
            this.btnRestore5.Click += new System.EventHandler(this.btnRestore5_Click);
            // 
            // btnRestore4
            // 
            this.btnRestore4.Location = new System.Drawing.Point(198, 119);
            this.btnRestore4.Margin = new System.Windows.Forms.Padding(4);
            this.btnRestore4.Name = "btnRestore4";
            this.btnRestore4.Size = new System.Drawing.Size(172, 31);
            this.btnRestore4.TabIndex = 15;
            this.btnRestore4.Text = "Restore4 - array";
            this.btnRestore4.UseVisualStyleBackColor = true;
            this.btnRestore4.Click += new System.EventHandler(this.btnRestore4_Click);
            // 
            // btnRestore3
            // 
            this.btnRestore3.Location = new System.Drawing.Point(18, 83);
            this.btnRestore3.Margin = new System.Windows.Forms.Padding(4);
            this.btnRestore3.Name = "btnRestore3";
            this.btnRestore3.Size = new System.Drawing.Size(172, 31);
            this.btnRestore3.TabIndex = 14;
            this.btnRestore3.Text = "Restore3 - class";
            this.btnRestore3.UseVisualStyleBackColor = true;
            this.btnRestore3.Click += new System.EventHandler(this.btnRestore3_Click);
            // 
            // btnExceptionHandler
            // 
            this.btnExceptionHandler.Location = new System.Drawing.Point(581, 44);
            this.btnExceptionHandler.Margin = new System.Windows.Forms.Padding(4);
            this.btnExceptionHandler.Name = "btnExceptionHandler";
            this.btnExceptionHandler.Size = new System.Drawing.Size(152, 31);
            this.btnExceptionHandler.TabIndex = 13;
            this.btnExceptionHandler.Text = "Exception Handler";
            this.btnExceptionHandler.UseVisualStyleBackColor = true;
            this.btnExceptionHandler.Click += new System.EventHandler(this.btnExceptionHandler_Click);
            // 
            // btnREF
            // 
            this.btnREF.Location = new System.Drawing.Point(421, 83);
            this.btnREF.Margin = new System.Windows.Forms.Padding(4);
            this.btnREF.Name = "btnREF";
            this.btnREF.Size = new System.Drawing.Size(152, 31);
            this.btnREF.TabIndex = 12;
            this.btnREF.Text = "REF<T>";
            this.btnREF.UseVisualStyleBackColor = true;
            this.btnREF.Click += new System.EventHandler(this.btnREF_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAdTest);
            this.groupBox1.Controls.Add(this.btnMultiException);
            this.groupBox1.Controls.Add(this.btnParaRetry);
            this.groupBox1.Location = new System.Drawing.Point(18, 158);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(872, 73);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "應用篇";
            // 
            // btnMultiException
            // 
            this.btnMultiException.Location = new System.Drawing.Point(9, 28);
            this.btnMultiException.Margin = new System.Windows.Forms.Padding(4);
            this.btnMultiException.Name = "btnMultiException";
            this.btnMultiException.Size = new System.Drawing.Size(170, 31);
            this.btnMultiException.TabIndex = 11;
            this.btnMultiException.Text = "Multi-Exception";
            this.btnMultiException.UseVisualStyleBackColor = true;
            this.btnMultiException.Click += new System.EventHandler(this.btnMultiException_Click);
            // 
            // btnParaRetry
            // 
            this.btnParaRetry.Location = new System.Drawing.Point(188, 28);
            this.btnParaRetry.Margin = new System.Windows.Forms.Padding(4);
            this.btnParaRetry.Name = "btnParaRetry";
            this.btnParaRetry.Size = new System.Drawing.Size(170, 31);
            this.btnParaRetry.TabIndex = 10;
            this.btnParaRetry.Text = "Parameterized Retry";
            this.btnParaRetry.UseVisualStyleBackColor = true;
            this.btnParaRetry.Click += new System.EventHandler(this.btnParaRetry_Click);
            // 
            // btnTraceException
            // 
            this.btnTraceException.Location = new System.Drawing.Point(581, 8);
            this.btnTraceException.Margin = new System.Windows.Forms.Padding(4);
            this.btnTraceException.Name = "btnTraceException";
            this.btnTraceException.Size = new System.Drawing.Size(152, 31);
            this.btnTraceException.TabIndex = 9;
            this.btnTraceException.Text = "Trace Exception";
            this.btnTraceException.UseVisualStyleBackColor = true;
            this.btnTraceException.Click += new System.EventHandler(this.btnTraceException_Click);
            // 
            // btnAdTest
            // 
            this.btnAdTest.Location = new System.Drawing.Point(366, 28);
            this.btnAdTest.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdTest.Name = "btnAdTest";
            this.btnAdTest.Size = new System.Drawing.Size(170, 31);
            this.btnAdTest.TabIndex = 12;
            this.btnAdTest.Text = "Ad Test";
            this.btnAdTest.UseVisualStyleBackColor = true;
            this.btnAdTest.Click += new System.EventHandler(this.btnAdTest_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 557);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1 - 功能測試";
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnTrace;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
        private System.Windows.Forms.Button btnRetryOnce;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnIgnore;
        private System.Windows.Forms.Button btnRestore2;
        private System.Windows.Forms.Button btnWhenTrue;
        private System.Windows.Forms.Button btnWaitCursor;
        private System.Windows.Forms.Button btnShowProgress;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTraceException;
        private System.Windows.Forms.Button btnParaRetry;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnREF;
        private System.Windows.Forms.Button btnExceptionHandler;
        private System.Windows.Forms.Button btnMultiException;
        private System.Windows.Forms.Button btnRestore3;
        private System.Windows.Forms.Button btnRestore4;
        private System.Windows.Forms.Button btnRestore5;
        private System.Windows.Forms.Button btnRestore6;
        private System.Windows.Forms.Button btnRestore7;
        private System.Windows.Forms.Button btnRetryParam;
        private System.Windows.Forms.Button btnUntil;
        private System.Windows.Forms.Button btnReconfirmIntent;
        private System.Windows.Forms.Button btnUntil2;
        private System.Windows.Forms.Button btnAdTest;
    }
}

