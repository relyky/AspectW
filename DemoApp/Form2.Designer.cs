namespace DemoApp
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboException = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numErrorDepth = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numErrorSeed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnBasicRetryT = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnBasicRetry = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.btnParamRetryT = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.btnParamRetry = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.btnMultiExRetry = new System.Windows.Forms.Button();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.btnSkipFault = new System.Windows.Forms.Button();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.btnComposite = new System.Windows.Forms.Button();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnMultiExRetryT = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numErrorDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numErrorSeed)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMessage
            // 
            this.txtMessage.ContextMenuStrip = this.contextMenuStrip1;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Font = new System.Drawing.Font("細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtMessage.Location = new System.Drawing.Point(0, 0);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(4);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessage.Size = new System.Drawing.Size(1075, 285);
            this.txtMessage.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearAllToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 26);
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.clearAllToolStripMenuItem.Text = "Clear All";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboException);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.numErrorDepth);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.numErrorSeed);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1075, 71);
            this.panel1.TabIndex = 0;
            // 
            // cboException
            // 
            this.cboException.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboException.FormattingEnabled = true;
            this.cboException.Items.AddRange(new object[] {
            "(Random)",
            "ApplicationException",
            "IOException",
            "DataException"});
            this.cboException.Location = new System.Drawing.Point(482, 37);
            this.cboException.Name = "cboException";
            this.cboException.Size = new System.Drawing.Size(222, 24);
            this.cboException.TabIndex = 2;
            this.toolTip1.SetToolTip(this.cboException, "指定Fail類別。");
            this.cboException.SelectedIndexChanged += new System.EventHandler(this.cboException_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(411, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "excepton";
            // 
            // numErrorDepth
            // 
            this.numErrorDepth.Location = new System.Drawing.Point(290, 33);
            this.numErrorDepth.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numErrorDepth.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numErrorDepth.Name = "numErrorDepth";
            this.numErrorDepth.Size = new System.Drawing.Size(92, 27);
            this.numErrorDepth.TabIndex = 1;
            this.numErrorDepth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.numErrorDepth, "連續Fail次數，以負數算。");
            this.numErrorDepth.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numErrorDepth.ValueChanged += new System.EventHandler(this.numErrorDepth_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(207, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "error depth";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "FAIL_RISE Parameters";
            // 
            // numErrorSeed
            // 
            this.numErrorSeed.Location = new System.Drawing.Point(88, 33);
            this.numErrorSeed.Name = "numErrorSeed";
            this.numErrorSeed.Size = new System.Drawing.Size(92, 27);
            this.numErrorSeed.TabIndex = 0;
            this.numErrorSeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.numErrorSeed, "即執行N次後出現Fail。");
            this.numErrorSeed.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numErrorSeed.ValueChanged += new System.EventHandler(this.numErrorSeed_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "error seed";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1075, 314);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1067, 284);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Basic Retry";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnBasicRetryT);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnBasicRetry);
            this.splitContainer1.Panel2.Controls.Add(this.textBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1067, 284);
            this.splitContainer1.SplitterDistance = 517;
            this.splitContainer1.TabIndex = 8;
            // 
            // btnBasicRetryT
            // 
            this.btnBasicRetryT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBasicRetryT.Location = new System.Drawing.Point(374, 240);
            this.btnBasicRetryT.Margin = new System.Windows.Forms.Padding(4);
            this.btnBasicRetryT.Name = "btnBasicRetryT";
            this.btnBasicRetryT.Size = new System.Drawing.Size(112, 31);
            this.btnBasicRetryT.TabIndex = 2;
            this.btnBasicRetryT.Text = "Basic Retry T";
            this.btnBasicRetryT.UseVisualStyleBackColor = true;
            this.btnBasicRetryT.Click += new System.EventHandler(this.btnBasicRetryT_Click);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("細明體-ExtB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(517, 284);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // btnBasicRetry
            // 
            this.btnBasicRetry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBasicRetry.Location = new System.Drawing.Point(410, 240);
            this.btnBasicRetry.Margin = new System.Windows.Forms.Padding(4);
            this.btnBasicRetry.Name = "btnBasicRetry";
            this.btnBasicRetry.Size = new System.Drawing.Size(112, 31);
            this.btnBasicRetry.TabIndex = 1;
            this.btnBasicRetry.Text = "Basic Retry";
            this.btnBasicRetry.UseVisualStyleBackColor = true;
            this.btnBasicRetry.Click += new System.EventHandler(this.btnBasicRetry_Click);
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Font = new System.Drawing.Font("細明體-ExtB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox2.Location = new System.Drawing.Point(0, 0);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(546, 284);
            this.textBox2.TabIndex = 0;
            this.textBox2.TabStop = false;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer3);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1067, 284);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Parameterized Retry";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.btnParamRetryT);
            this.splitContainer3.Panel1.Controls.Add(this.textBox3);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.btnParamRetry);
            this.splitContainer3.Panel2.Controls.Add(this.textBox4);
            this.splitContainer3.Size = new System.Drawing.Size(1067, 284);
            this.splitContainer3.SplitterDistance = 520;
            this.splitContainer3.TabIndex = 3;
            // 
            // btnParamRetryT
            // 
            this.btnParamRetryT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnParamRetryT.Location = new System.Drawing.Point(375, 249);
            this.btnParamRetryT.Margin = new System.Windows.Forms.Padding(4);
            this.btnParamRetryT.Name = "btnParamRetryT";
            this.btnParamRetryT.Size = new System.Drawing.Size(112, 31);
            this.btnParamRetryT.TabIndex = 2;
            this.btnParamRetryT.Text = "Param Retry T";
            this.btnParamRetryT.UseVisualStyleBackColor = true;
            this.btnParamRetryT.Click += new System.EventHandler(this.btnParamRetryT_Click);
            // 
            // textBox3
            // 
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox3.Font = new System.Drawing.Font("細明體-ExtB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox3.Location = new System.Drawing.Point(0, 0);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox3.Size = new System.Drawing.Size(520, 284);
            this.textBox3.TabIndex = 0;
            this.textBox3.TabStop = false;
            this.textBox3.Text = resources.GetString("textBox3.Text");
            // 
            // btnParamRetry
            // 
            this.btnParamRetry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnParamRetry.Location = new System.Drawing.Point(406, 249);
            this.btnParamRetry.Margin = new System.Windows.Forms.Padding(4);
            this.btnParamRetry.Name = "btnParamRetry";
            this.btnParamRetry.Size = new System.Drawing.Size(112, 31);
            this.btnParamRetry.TabIndex = 1;
            this.btnParamRetry.Text = "Param Retry";
            this.btnParamRetry.UseVisualStyleBackColor = true;
            this.btnParamRetry.Click += new System.EventHandler(this.btnParamRetry_Click);
            // 
            // textBox4
            // 
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox4.Font = new System.Drawing.Font("細明體-ExtB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox4.Location = new System.Drawing.Point(0, 0);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox4.Size = new System.Drawing.Size(543, 284);
            this.textBox4.TabIndex = 0;
            this.textBox4.TabStop = false;
            this.textBox4.Text = resources.GetString("textBox4.Text");
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer4);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1067, 284);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Recovery From Multiple Exception";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.btnMultiExRetryT);
            this.splitContainer4.Panel1.Controls.Add(this.textBox5);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.btnMultiExRetry);
            this.splitContainer4.Panel2.Controls.Add(this.textBox6);
            this.splitContainer4.Size = new System.Drawing.Size(1067, 284);
            this.splitContainer4.SplitterDistance = 489;
            this.splitContainer4.TabIndex = 2;
            // 
            // textBox5
            // 
            this.textBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox5.Font = new System.Drawing.Font("細明體-ExtB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox5.Location = new System.Drawing.Point(0, 0);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox5.Size = new System.Drawing.Size(489, 284);
            this.textBox5.TabIndex = 0;
            this.textBox5.TabStop = false;
            this.textBox5.Text = resources.GetString("textBox5.Text");
            // 
            // btnMultiExRetry
            // 
            this.btnMultiExRetry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMultiExRetry.Location = new System.Drawing.Point(437, 249);
            this.btnMultiExRetry.Margin = new System.Windows.Forms.Padding(4);
            this.btnMultiExRetry.Name = "btnMultiExRetry";
            this.btnMultiExRetry.Size = new System.Drawing.Size(112, 31);
            this.btnMultiExRetry.TabIndex = 1;
            this.btnMultiExRetry.Text = "Multi-Ex Retry";
            this.btnMultiExRetry.UseVisualStyleBackColor = true;
            this.btnMultiExRetry.Click += new System.EventHandler(this.btnMultiExRetry_Click);
            // 
            // textBox6
            // 
            this.textBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox6.Font = new System.Drawing.Font("細明體-ExtB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox6.Location = new System.Drawing.Point(0, 0);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox6.Size = new System.Drawing.Size(574, 284);
            this.textBox6.TabIndex = 0;
            this.textBox6.TabStop = false;
            this.textBox6.Text = resources.GetString("textBox6.Text");
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.splitContainer5);
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1067, 284);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Skip Some Fault In Loop";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.textBox7);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.btnSkipFault);
            this.splitContainer5.Panel2.Controls.Add(this.textBox8);
            this.splitContainer5.Size = new System.Drawing.Size(1067, 284);
            this.splitContainer5.SplitterDistance = 524;
            this.splitContainer5.TabIndex = 0;
            // 
            // textBox7
            // 
            this.textBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox7.Font = new System.Drawing.Font("細明體-ExtB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox7.Location = new System.Drawing.Point(0, 0);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox7.Size = new System.Drawing.Size(524, 284);
            this.textBox7.TabIndex = 0;
            this.textBox7.TabStop = false;
            this.textBox7.Text = resources.GetString("textBox7.Text");
            // 
            // btnSkipFault
            // 
            this.btnSkipFault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSkipFault.Location = new System.Drawing.Point(400, 249);
            this.btnSkipFault.Margin = new System.Windows.Forms.Padding(4);
            this.btnSkipFault.Name = "btnSkipFault";
            this.btnSkipFault.Size = new System.Drawing.Size(112, 31);
            this.btnSkipFault.TabIndex = 1;
            this.btnSkipFault.Text = "Skip Fault";
            this.btnSkipFault.UseVisualStyleBackColor = true;
            this.btnSkipFault.Click += new System.EventHandler(this.btnSkipFault_Click);
            // 
            // textBox8
            // 
            this.textBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox8.Font = new System.Drawing.Font("細明體-ExtB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox8.Location = new System.Drawing.Point(0, 0);
            this.textBox8.Multiline = true;
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox8.Size = new System.Drawing.Size(539, 284);
            this.textBox8.TabIndex = 0;
            this.textBox8.TabStop = false;
            this.textBox8.Text = resources.GetString("textBox8.Text");
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.splitContainer6);
            this.tabPage5.Location = new System.Drawing.Point(4, 26);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(1067, 284);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Composite Case";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.textBox9);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.btnComposite);
            this.splitContainer6.Panel2.Controls.Add(this.textBox10);
            this.splitContainer6.Size = new System.Drawing.Size(1067, 284);
            this.splitContainer6.SplitterDistance = 519;
            this.splitContainer6.TabIndex = 0;
            // 
            // textBox9
            // 
            this.textBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox9.Font = new System.Drawing.Font("細明體-ExtB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox9.Location = new System.Drawing.Point(0, 0);
            this.textBox9.Multiline = true;
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox9.Size = new System.Drawing.Size(519, 284);
            this.textBox9.TabIndex = 0;
            this.textBox9.TabStop = false;
            this.textBox9.Text = resources.GetString("textBox9.Text");
            // 
            // btnComposite
            // 
            this.btnComposite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComposite.Location = new System.Drawing.Point(410, 249);
            this.btnComposite.Margin = new System.Windows.Forms.Padding(4);
            this.btnComposite.Name = "btnComposite";
            this.btnComposite.Size = new System.Drawing.Size(112, 31);
            this.btnComposite.TabIndex = 1;
            this.btnComposite.Text = "Composite";
            this.btnComposite.UseVisualStyleBackColor = true;
            this.btnComposite.Click += new System.EventHandler(this.btnComposite_Click);
            // 
            // textBox10
            // 
            this.textBox10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox10.Font = new System.Drawing.Font("細明體-ExtB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox10.Location = new System.Drawing.Point(0, 0);
            this.textBox10.Multiline = true;
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox10.Size = new System.Drawing.Size(544, 284);
            this.textBox10.TabIndex = 0;
            this.textBox10.TabStop = false;
            this.textBox10.Text = resources.GetString("textBox10.Text");
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 71);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtMessage);
            this.splitContainer2.Size = new System.Drawing.Size(1075, 603);
            this.splitContainer2.SplitterDistance = 314;
            this.splitContainer2.TabIndex = 8;
            // 
            // btnMultiExRetryT
            // 
            this.btnMultiExRetryT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMultiExRetryT.Location = new System.Drawing.Point(332, 249);
            this.btnMultiExRetryT.Margin = new System.Windows.Forms.Padding(4);
            this.btnMultiExRetryT.Name = "btnMultiExRetryT";
            this.btnMultiExRetryT.Size = new System.Drawing.Size(127, 31);
            this.btnMultiExRetryT.TabIndex = 2;
            this.btnMultiExRetryT.Text = "Multi-Ex Retry T";
            this.btnMultiExRetryT.UseVisualStyleBackColor = true;
            this.btnMultiExRetryT.Click += new System.EventHandler(this.btnMultiExRetryT_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 674);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2 - 以情境來測試";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numErrorDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numErrorSeed)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel1.PerformLayout();
            this.splitContainer6.Panel2.ResumeLayout(false);
            this.splitContainer6.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnBasicRetry;
        private System.Windows.Forms.NumericUpDown numErrorSeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numErrorDepth;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button btnParamRetry;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnMultiExRetry;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboException;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Button btnSkipFault;
        private System.Windows.Forms.Button btnComposite;
        private System.Windows.Forms.Button btnBasicRetryT;
        private System.Windows.Forms.Button btnParamRetryT;
        private System.Windows.Forms.Button btnMultiExRetryT;
    }
}