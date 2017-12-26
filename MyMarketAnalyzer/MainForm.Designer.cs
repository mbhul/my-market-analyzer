namespace MyMarketAnalyzer
{
    partial class MainForm
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
                if (backgroundWorkerStat.IsBusy == true)
                    backgroundWorkerStat.CancelAsync();
                if (backgroundWorkerProgress.IsBusy == true)
                    backgroundWorkerProgress.CancelAsync();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabHome = new System.Windows.Forms.TabPage();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.loadProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.watchlist1 = new MyMarketAnalyzer.Watchlist();
            this.ticker1 = new MyMarketAnalyzer.Ticker();
            this.tabStats = new System.Windows.Forms.TabPage();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.dataMenuPanel = new System.Windows.Forms.Panel();
            this.cbLiveDataInterval = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dataMenuArrow = new System.Windows.Forms.PictureBox();
            this.tblStatTableMain = new MyMarketAnalyzer.StatTable();
            this.lblUpdate = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblPoweredBy = new System.Windows.Forms.Label();
            this.progressStats = new System.Windows.Forms.ProgressBar();
            this.toolStripStat = new System.Windows.Forms.ToolStrip();
            this.btnDataSrc = new System.Windows.Forms.ToolStripDropDownButton();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.unloadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.unloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnVisualsMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.showChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heatMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.random10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.imgRegionFlag = new System.Windows.Forms.ToolStripButton();
            this.cbRegionSelect = new System.Windows.Forms.ToolStripComboBox();
            this.btnExcelDowloader = new System.Windows.Forms.ToolStripButton();
            this.tsRegionImg = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.cbMarketSelect = new System.Windows.Forms.ToolStripComboBox();
            this.groupBoxStatFilter = new System.Windows.Forms.GroupBox();
            this.lblStatResultNum = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStatTo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStatFrom = new System.Windows.Forms.TextBox();
            this.comboBoxStatFilter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblLiveDataStatus1 = new System.Windows.Forms.Label();
            this.lblHistDataStatus1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tabAnalysis = new System.Windows.Forms.TabPage();
            this.analysisSplitContainer = new System.Windows.Forms.SplitContainer();
            this.btnRunAnalysis = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.sellRulesBox = new System.Windows.Forms.GroupBox();
            this.tblPanelSellRule = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSellRuleExpandCollapse = new System.Windows.Forms.Button();
            this.analysisSell_RTxtBox = new System.Windows.Forms.RichTextBox();
            this.buyRulesBox = new System.Windows.Forms.GroupBox();
            this.tblPanelBuyRule = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnBuyRuleExpandCollapse = new System.Windows.Forms.Button();
            this.analysisBuy_RTxtBox = new System.Windows.Forms.RichTextBox();
            this.analysisToolbox1 = new MyMarketAnalyzer.AnalysisToolbox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblLiveDataStatus2 = new System.Windows.Forms.Label();
            this.lblHistDataStatus2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.analysisAmtHelpBtn = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.analysisAmtTxt = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.analysisSelectBox = new System.Windows.Forms.ComboBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.panelAnalysis1 = new System.Windows.Forms.Panel();
            this.lblChartDate = new System.Windows.Forms.Label();
            this.btnChartNext = new System.Windows.Forms.Button();
            this.btnChartPrev = new System.Windows.Forms.Button();
            this.chartAnalysis = new MyMarketAnalyzer.CustomChart();
            this.analysisSummaryPage1 = new MyMarketAnalyzer.AnalysisSummaryPage();
            this.toolStripAnalysis = new System.Windows.Forms.ToolStrip();
            this.tsHistSourceDir2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.btnLoadPatternForm = new System.Windows.Forms.ToolStripButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsHistSourceDir1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbAnalysisIndicatorY = new System.Windows.Forms.ComboBox();
            this.cbAnalysisType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAnalysisShowChart = new System.Windows.Forms.Button();
            this.cbAnalysisIndicatorX = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dlgStatFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorkerStat = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerProgress = new System.ComponentModel.BackgroundWorker();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.backgroundWorkerAnalysisProgress = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerAnalysis = new System.ComponentModel.BackgroundWorker();
            this.tabControl1.SuspendLayout();
            this.tabHome.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabStats.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.dataMenuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMenuArrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStripStat.SuspendLayout();
            this.groupBoxStatFilter.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabAnalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.analysisSplitContainer)).BeginInit();
            this.analysisSplitContainer.Panel1.SuspendLayout();
            this.analysisSplitContainer.Panel2.SuspendLayout();
            this.analysisSplitContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.sellRulesBox.SuspendLayout();
            this.tblPanelSellRule.SuspendLayout();
            this.panel4.SuspendLayout();
            this.buyRulesBox.SuspendLayout();
            this.tblPanelBuyRule.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.analysisAmtHelpBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panelAnalysis1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAnalysis)).BeginInit();
            this.toolStripAnalysis.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabHome);
            this.tabControl1.Controls.Add(this.tabStats);
            this.tabControl1.Controls.Add(this.tabAnalysis);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.tabControl1.Location = new System.Drawing.Point(1, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(981, 486);
            this.tabControl1.TabIndex = 0;
            // 
            // tabHome
            // 
            this.tabHome.Controls.Add(this.toolStrip2);
            this.tabHome.Controls.Add(this.splitContainer1);
            this.tabHome.Location = new System.Drawing.Point(4, 22);
            this.tabHome.Name = "tabHome";
            this.tabHome.Padding = new System.Windows.Forms.Padding(3);
            this.tabHome.Size = new System.Drawing.Size(973, 460);
            this.tabHome.TabIndex = 0;
            this.tabHome.Text = "Home";
            this.tabHome.UseVisualStyleBackColor = true;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator3,
            this.toolStripButton2});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(967, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadProfileToolStripMenuItem,
            this.saveProfileToolStripMenuItem});
            this.toolStripButton1.Image = global::MyMarketAnalyzer.Properties.Resources.folder;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(54, 22);
            this.toolStripButton1.Text = "File";
            // 
            // loadProfileToolStripMenuItem
            // 
            this.loadProfileToolStripMenuItem.Name = "loadProfileToolStripMenuItem";
            this.loadProfileToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.loadProfileToolStripMenuItem.Text = "Load Profile";
            this.loadProfileToolStripMenuItem.Click += new System.EventHandler(this.loadProfileToolStripMenuItem_Click);
            // 
            // saveProfileToolStripMenuItem
            // 
            this.saveProfileToolStripMenuItem.Name = "saveProfileToolStripMenuItem";
            this.saveProfileToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.saveProfileToolStripMenuItem.Text = "Save Profile";
            this.saveProfileToolStripMenuItem.Click += new System.EventHandler(this.saveProfileToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::MyMarketAnalyzer.Properties.Resources.option;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(69, 22);
            this.toolStripButton2.Text = "Options";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 31);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(967, 426);
            this.splitContainer1.SplitterDistance = 470;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.watchlist1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ticker1, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(470, 426);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(458, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "My Watch List";
            // 
            // watchlist1
            // 
            this.watchlist1.AutoScroll = true;
            this.watchlist1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.watchlist1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.watchlist1.Location = new System.Drawing.Point(6, 33);
            this.watchlist1.Name = "watchlist1";
            this.watchlist1.Size = new System.Drawing.Size(458, 364);
            this.watchlist1.TabIndex = 2;
            // 
            // ticker1
            // 
            this.ticker1.BackColor = System.Drawing.Color.LightGray;
            this.ticker1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ticker1.Location = new System.Drawing.Point(6, 406);
            this.ticker1.Name = "ticker1";
            this.ticker1.Size = new System.Drawing.Size(458, 14);
            this.ticker1.TabIndex = 3;
            // 
            // tabStats
            // 
            this.tabStats.Controls.Add(this.toolStripContainer1);
            this.tabStats.Controls.Add(this.lblUpdate);
            this.tabStats.Controls.Add(this.pictureBox1);
            this.tabStats.Controls.Add(this.lblPoweredBy);
            this.tabStats.Controls.Add(this.progressStats);
            this.tabStats.Controls.Add(this.toolStripStat);
            this.tabStats.Controls.Add(this.groupBoxStatFilter);
            this.tabStats.Controls.Add(this.comboBoxStatFilter);
            this.tabStats.Controls.Add(this.label2);
            this.tabStats.Controls.Add(this.panel2);
            this.tabStats.Location = new System.Drawing.Point(4, 22);
            this.tabStats.Name = "tabStats";
            this.tabStats.Padding = new System.Windows.Forms.Padding(3);
            this.tabStats.Size = new System.Drawing.Size(973, 460);
            this.tabStats.TabIndex = 1;
            this.tabStats.Text = "Data Manager";
            this.tabStats.UseVisualStyleBackColor = true;
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.dataMenuPanel);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tblStatTableMain);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(967, 373);
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(3, 59);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(967, 373);
            this.toolStripContainer1.TabIndex = 12;
            this.toolStripContainer1.Text = "toolStripContainer1";
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            // 
            // dataMenuPanel
            // 
            this.dataMenuPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataMenuPanel.Controls.Add(this.cbLiveDataInterval);
            this.dataMenuPanel.Controls.Add(this.label11);
            this.dataMenuPanel.Controls.Add(this.dataMenuArrow);
            this.dataMenuPanel.Location = new System.Drawing.Point(848, 0);
            this.dataMenuPanel.Name = "dataMenuPanel";
            this.dataMenuPanel.Size = new System.Drawing.Size(120, 373);
            this.dataMenuPanel.TabIndex = 5;
            // 
            // cbLiveDataInterval
            // 
            this.cbLiveDataInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLiveDataInterval.FormattingEnabled = true;
            this.cbLiveDataInterval.Location = new System.Drawing.Point(6, 46);
            this.cbLiveDataInterval.Name = "cbLiveDataInterval";
            this.cbLiveDataInterval.Size = new System.Drawing.Size(104, 21);
            this.cbLiveDataInterval.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Live Session Refresh";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataMenuArrow
            // 
            this.dataMenuArrow.Image = global::MyMarketAnalyzer.Properties.Resources.arrow_icon_normal;
            this.dataMenuArrow.Location = new System.Drawing.Point(0, 0);
            this.dataMenuArrow.Name = "dataMenuArrow";
            this.dataMenuArrow.Size = new System.Drawing.Size(28, 20);
            this.dataMenuArrow.TabIndex = 1;
            this.dataMenuArrow.TabStop = false;
            this.dataMenuArrow.Click += new System.EventHandler(this.dataMenuArrow_Click);
            this.dataMenuArrow.MouseEnter += new System.EventHandler(this.dataMenuArrow_MouseEnter);
            this.dataMenuArrow.MouseLeave += new System.EventHandler(this.dataMenuArrow_MouseLeave);
            // 
            // tblStatTableMain
            // 
            this.tblStatTableMain.AutoScroll = true;
            this.tblStatTableMain.AutoSize = true;
            this.tblStatTableMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblStatTableMain.Location = new System.Drawing.Point(0, 0);
            this.tblStatTableMain.Name = "tblStatTableMain";
            this.tblStatTableMain.Size = new System.Drawing.Size(967, 373);
            this.tblStatTableMain.TabIndex = 4;
            this.tblStatTableMain.TableType = MyMarketAnalyzer.StatTableType.HIST_STATS;
            // 
            // lblUpdate
            // 
            this.lblUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUpdate.AutoSize = true;
            this.lblUpdate.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblUpdate.Location = new System.Drawing.Point(5, 439);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(59, 13);
            this.lblUpdate.TabIndex = 11;
            this.lblUpdate.Text = "Updating...";
            this.lblUpdate.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::MyMarketAnalyzer.Properties.Resources.icom_icon;
            this.pictureBox1.Location = new System.Drawing.Point(864, 434);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(106, 23);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // lblPoweredBy
            // 
            this.lblPoweredBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPoweredBy.AutoSize = true;
            this.lblPoweredBy.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblPoweredBy.Location = new System.Drawing.Point(795, 439);
            this.lblPoweredBy.Name = "lblPoweredBy";
            this.lblPoweredBy.Size = new System.Drawing.Size(67, 13);
            this.lblPoweredBy.TabIndex = 8;
            this.lblPoweredBy.Text = "Powered By:";
            // 
            // progressStats
            // 
            this.progressStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressStats.Location = new System.Drawing.Point(3, 434);
            this.progressStats.Name = "progressStats";
            this.progressStats.Size = new System.Drawing.Size(967, 23);
            this.progressStats.TabIndex = 10;
            // 
            // toolStripStat
            // 
            this.toolStripStat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDataSrc,
            this.toolStripSeparator1,
            this.btnVisualsMenu,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.imgRegionFlag,
            this.cbRegionSelect,
            this.btnExcelDowloader,
            this.tsRegionImg,
            this.toolStripLabel4,
            this.cbMarketSelect});
            this.toolStripStat.Location = new System.Drawing.Point(3, 3);
            this.toolStripStat.Name = "toolStripStat";
            this.toolStripStat.Size = new System.Drawing.Size(967, 25);
            this.toolStripStat.TabIndex = 1;
            this.toolStripStat.Text = "toolStrip1";
            // 
            // btnDataSrc
            // 
            this.btnDataSrc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDataSrc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.unloadToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.btnDataSrc.Image = ((System.Drawing.Image)(resources.GetObject("btnDataSrc.Image")));
            this.btnDataSrc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDataSrc.Name = "btnDataSrc";
            this.btnDataSrc.Size = new System.Drawing.Size(88, 22);
            this.btnDataSrc.Text = "Data Sources";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem1,
            this.unloadToolStripMenuItem1});
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.loadToolStripMenuItem.Text = "Historical Data";
            // 
            // loadToolStripMenuItem1
            // 
            this.loadToolStripMenuItem1.Name = "loadToolStripMenuItem1";
            this.loadToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.loadToolStripMenuItem1.Text = "Load";
            this.loadToolStripMenuItem1.Click += new System.EventHandler(this.tsBtnLoadHistorical_Click);
            // 
            // unloadToolStripMenuItem1
            // 
            this.unloadToolStripMenuItem1.Name = "unloadToolStripMenuItem1";
            this.unloadToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.unloadToolStripMenuItem1.Text = "Unload";
            this.unloadToolStripMenuItem1.Click += new System.EventHandler(this.unloadToolStripMenuItem1_Click);
            // 
            // unloadToolStripMenuItem
            // 
            this.unloadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.unloadToolStripMenuItem.Name = "unloadToolStripMenuItem";
            this.unloadToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.unloadToolStripMenuItem.Text = "Live Data Session";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F2)));
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnVisualsMenu
            // 
            this.btnVisualsMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnVisualsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showChartToolStripMenuItem,
            this.heatMapToolStripMenuItem,
            this.random10ToolStripMenuItem});
            this.btnVisualsMenu.Image = ((System.Drawing.Image)(resources.GetObject("btnVisualsMenu.Image")));
            this.btnVisualsMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVisualsMenu.Name = "btnVisualsMenu";
            this.btnVisualsMenu.Size = new System.Drawing.Size(86, 22);
            this.btnVisualsMenu.Text = "Visualization";
            // 
            // showChartToolStripMenuItem
            // 
            this.showChartToolStripMenuItem.Name = "showChartToolStripMenuItem";
            this.showChartToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.showChartToolStripMenuItem.Text = "Show Chart";
            this.showChartToolStripMenuItem.ToolTipText = "Display Chart for Selected Data";
            this.showChartToolStripMenuItem.Click += new System.EventHandler(this.btnShowChart_OnClick);
            // 
            // heatMapToolStripMenuItem
            // 
            this.heatMapToolStripMenuItem.Name = "heatMapToolStripMenuItem";
            this.heatMapToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.heatMapToolStripMenuItem.Text = "Heat Map";
            this.heatMapToolStripMenuItem.ToolTipText = "Display the Heat Map of the currently loaded data";
            this.heatMapToolStripMenuItem.Click += new System.EventHandler(this.heatMapToolStripMenuItem_Click);
            // 
            // random10ToolStripMenuItem
            // 
            this.random10ToolStripMenuItem.Name = "random10ToolStripMenuItem";
            this.random10ToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.random10ToolStripMenuItem.Text = "Random 10";
            this.random10ToolStripMenuItem.Click += new System.EventHandler(this.random10ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel3.Text = "Region:";
            // 
            // imgRegionFlag
            // 
            this.imgRegionFlag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.imgRegionFlag.Image = ((System.Drawing.Image)(resources.GetObject("imgRegionFlag.Image")));
            this.imgRegionFlag.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.imgRegionFlag.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.imgRegionFlag.Name = "imgRegionFlag";
            this.imgRegionFlag.Size = new System.Drawing.Size(23, 22);
            this.imgRegionFlag.Text = "toolStripButton1";
            // 
            // cbRegionSelect
            // 
            this.cbRegionSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRegionSelect.Name = "cbRegionSelect";
            this.cbRegionSelect.Size = new System.Drawing.Size(121, 25);
            this.cbRegionSelect.SelectedIndexChanged += new System.EventHandler(this.cbRegionSelect_SelectedIndexChanged);
            // 
            // btnExcelDowloader
            // 
            this.btnExcelDowloader.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnExcelDowloader.BackColor = System.Drawing.Color.Transparent;
            this.btnExcelDowloader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExcelDowloader.Image = ((System.Drawing.Image)(resources.GetObject("btnExcelDowloader.Image")));
            this.btnExcelDowloader.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExcelDowloader.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcelDowloader.Name = "btnExcelDowloader";
            this.btnExcelDowloader.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnExcelDowloader.Size = new System.Drawing.Size(167, 22);
            this.btnExcelDowloader.Text = "Download Historical Data";
            this.btnExcelDowloader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcelDowloader.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.btnExcelDowloader.Click += new System.EventHandler(this.btnExcelDowloader_Click);
            // 
            // tsRegionImg
            // 
            this.tsRegionImg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsRegionImg.Name = "tsRegionImg";
            this.tsRegionImg.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(86, 22);
            this.toolStripLabel4.Text = "Market / Index:";
            // 
            // cbMarketSelect
            // 
            this.cbMarketSelect.AutoToolTip = true;
            this.cbMarketSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMarketSelect.DropDownWidth = 200;
            this.cbMarketSelect.Name = "cbMarketSelect";
            this.cbMarketSelect.Size = new System.Drawing.Size(150, 25);
            this.cbMarketSelect.SelectedIndexChanged += new System.EventHandler(this.cbMarket_IndexChanged);
            // 
            // groupBoxStatFilter
            // 
            this.groupBoxStatFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxStatFilter.Controls.Add(this.lblStatResultNum);
            this.groupBoxStatFilter.Controls.Add(this.label4);
            this.groupBoxStatFilter.Controls.Add(this.txtStatTo);
            this.groupBoxStatFilter.Controls.Add(this.label3);
            this.groupBoxStatFilter.Controls.Add(this.txtStatFrom);
            this.groupBoxStatFilter.Location = new System.Drawing.Point(695, 23);
            this.groupBoxStatFilter.Name = "groupBoxStatFilter";
            this.groupBoxStatFilter.Size = new System.Drawing.Size(275, 34);
            this.groupBoxStatFilter.TabIndex = 6;
            this.groupBoxStatFilter.TabStop = false;
            this.groupBoxStatFilter.Visible = false;
            // 
            // lblStatResultNum
            // 
            this.lblStatResultNum.AutoSize = true;
            this.lblStatResultNum.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStatResultNum.Location = new System.Drawing.Point(272, 16);
            this.lblStatResultNum.Name = "lblStatResultNum";
            this.lblStatResultNum.Size = new System.Drawing.Size(0, 13);
            this.lblStatResultNum.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(163, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "To:";
            // 
            // txtStatTo
            // 
            this.txtStatTo.Location = new System.Drawing.Point(192, 10);
            this.txtStatTo.Name = "txtStatTo";
            this.txtStatTo.Size = new System.Drawing.Size(68, 20);
            this.txtStatTo.TabIndex = 2;
            this.txtStatTo.TextChanged += new System.EventHandler(this.btnRefreshStatTable_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Values From:";
            // 
            // txtStatFrom
            // 
            this.txtStatFrom.Location = new System.Drawing.Point(80, 10);
            this.txtStatFrom.Name = "txtStatFrom";
            this.txtStatFrom.Size = new System.Drawing.Size(68, 20);
            this.txtStatFrom.TabIndex = 0;
            this.txtStatFrom.TextChanged += new System.EventHandler(this.btnRefreshStatTable_Click);
            // 
            // comboBoxStatFilter
            // 
            this.comboBoxStatFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatFilter.FormattingEnabled = true;
            this.comboBoxStatFilter.Location = new System.Drawing.Point(534, 33);
            this.comboBoxStatFilter.Name = "comboBoxStatFilter";
            this.comboBoxStatFilter.Size = new System.Drawing.Size(155, 21);
            this.comboBoxStatFilter.TabIndex = 5;
            this.comboBoxStatFilter.SelectionChangeCommitted += new System.EventHandler(this.statFilter_SelectionChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(466, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Filter On:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblLiveDataStatus1);
            this.panel2.Controls.Add(this.lblHistDataStatus1);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Location = new System.Drawing.Point(2, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(461, 30);
            this.panel2.TabIndex = 7;
            // 
            // lblLiveDataStatus1
            // 
            this.lblLiveDataStatus1.AutoSize = true;
            this.lblLiveDataStatus1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLiveDataStatus1.ForeColor = System.Drawing.Color.Red;
            this.lblLiveDataStatus1.Location = new System.Drawing.Point(365, 7);
            this.lblLiveDataStatus1.Name = "lblLiveDataStatus1";
            this.lblLiveDataStatus1.Size = new System.Drawing.Size(62, 15);
            this.lblLiveDataStatus1.TabIndex = 5;
            this.lblLiveDataStatus1.Text = "CLOSED";
            // 
            // lblHistDataStatus1
            // 
            this.lblHistDataStatus1.AutoSize = true;
            this.lblHistDataStatus1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHistDataStatus1.ForeColor = System.Drawing.Color.Red;
            this.lblHistDataStatus1.Location = new System.Drawing.Point(99, 7);
            this.lblHistDataStatus1.Name = "lblHistDataStatus1";
            this.lblHistDataStatus1.Size = new System.Drawing.Size(97, 15);
            this.lblHistDataStatus1.TabIndex = 4;
            this.lblHistDataStatus1.Text = "UNAVAILABLE";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(251, 7);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(108, 15);
            this.label13.TabIndex = 3;
            this.label13.Text = "Live Data Session:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(3, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(90, 15);
            this.label14.TabIndex = 2;
            this.label14.Text = "Historical Data:";
            // 
            // tabAnalysis
            // 
            this.tabAnalysis.Controls.Add(this.analysisSplitContainer);
            this.tabAnalysis.Controls.Add(this.toolStripAnalysis);
            this.tabAnalysis.Location = new System.Drawing.Point(4, 22);
            this.tabAnalysis.Name = "tabAnalysis";
            this.tabAnalysis.Padding = new System.Windows.Forms.Padding(3);
            this.tabAnalysis.Size = new System.Drawing.Size(973, 460);
            this.tabAnalysis.TabIndex = 2;
            this.tabAnalysis.Text = "Analysis";
            this.tabAnalysis.UseVisualStyleBackColor = true;
            this.tabAnalysis.Layout += new System.Windows.Forms.LayoutEventHandler(this.tabAnalysis_Layout);
            // 
            // analysisSplitContainer
            // 
            this.analysisSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.analysisSplitContainer.Location = new System.Drawing.Point(3, 30);
            this.analysisSplitContainer.Name = "analysisSplitContainer";
            // 
            // analysisSplitContainer.Panel1
            // 
            this.analysisSplitContainer.Panel1.Controls.Add(this.btnRunAnalysis);
            this.analysisSplitContainer.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.analysisSplitContainer.Panel1.Controls.Add(this.panel1);
            this.analysisSplitContainer.Panel1.Controls.Add(this.groupBox1);
            // 
            // analysisSplitContainer.Panel2
            // 
            this.analysisSplitContainer.Panel2.Controls.Add(this.splitContainer3);
            this.analysisSplitContainer.Panel2MinSize = 400;
            this.analysisSplitContainer.Size = new System.Drawing.Size(967, 430);
            this.analysisSplitContainer.SplitterDistance = 510;
            this.analysisSplitContainer.SplitterWidth = 6;
            this.analysisSplitContainer.TabIndex = 1;
            // 
            // btnRunAnalysis
            // 
            this.btnRunAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunAnalysis.Location = new System.Drawing.Point(0, 401);
            this.btnRunAnalysis.Name = "btnRunAnalysis";
            this.btnRunAnalysis.Size = new System.Drawing.Size(510, 29);
            this.btnRunAnalysis.TabIndex = 4;
            this.btnRunAnalysis.Text = "Run Analysis";
            this.btnRunAnalysis.UseVisualStyleBackColor = true;
            this.btnRunAnalysis.Click += new System.EventHandler(this.btnRunAnalysis_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.sellRulesBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buyRulesBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.analysisToolbox1, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 77);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(510, 320);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // sellRulesBox
            // 
            this.sellRulesBox.Controls.Add(this.tblPanelSellRule);
            this.sellRulesBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sellRulesBox.Location = new System.Drawing.Point(3, 67);
            this.sellRulesBox.Name = "sellRulesBox";
            this.sellRulesBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.sellRulesBox.Size = new System.Drawing.Size(504, 58);
            this.sellRulesBox.TabIndex = 11;
            this.sellRulesBox.TabStop = false;
            this.sellRulesBox.Text = "Sell Rule";
            // 
            // tblPanelSellRule
            // 
            this.tblPanelSellRule.ColumnCount = 1;
            this.tblPanelSellRule.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPanelSellRule.Controls.Add(this.panel4, 0, 0);
            this.tblPanelSellRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPanelSellRule.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tblPanelSellRule.Location = new System.Drawing.Point(3, 16);
            this.tblPanelSellRule.Name = "tblPanelSellRule";
            this.tblPanelSellRule.RowCount = 2;
            this.tblPanelSellRule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblPanelSellRule.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblPanelSellRule.Size = new System.Drawing.Size(498, 39);
            this.tblPanelSellRule.TabIndex = 10;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnSellRuleExpandCollapse);
            this.panel4.Controls.Add(this.analysisSell_RTxtBox);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(492, 26);
            this.panel4.TabIndex = 0;
            // 
            // btnSellRuleExpandCollapse
            // 
            this.btnSellRuleExpandCollapse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSellRuleExpandCollapse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSellRuleExpandCollapse.FlatAppearance.BorderSize = 0;
            this.btnSellRuleExpandCollapse.ForeColor = System.Drawing.Color.Black;
            this.btnSellRuleExpandCollapse.Image = global::MyMarketAnalyzer.Properties.Resources.expand_icon;
            this.btnSellRuleExpandCollapse.Location = new System.Drawing.Point(462, 4);
            this.btnSellRuleExpandCollapse.Margin = new System.Windows.Forms.Padding(0);
            this.btnSellRuleExpandCollapse.Name = "btnSellRuleExpandCollapse";
            this.btnSellRuleExpandCollapse.Size = new System.Drawing.Size(30, 22);
            this.btnSellRuleExpandCollapse.TabIndex = 4;
            this.btnSellRuleExpandCollapse.UseVisualStyleBackColor = true;
            this.btnSellRuleExpandCollapse.Click += new System.EventHandler(this.btnSellRuleExpandCollapse_Click);
            // 
            // analysisSell_RTxtBox
            // 
            this.analysisSell_RTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.analysisSell_RTxtBox.Location = new System.Drawing.Point(0, 4);
            this.analysisSell_RTxtBox.Name = "analysisSell_RTxtBox";
            this.analysisSell_RTxtBox.Size = new System.Drawing.Size(465, 22);
            this.analysisSell_RTxtBox.TabIndex = 3;
            this.analysisSell_RTxtBox.Text = "";
            this.analysisSell_RTxtBox.Enter += new System.EventHandler(this.sellRTBox_OnFocus);
            // 
            // buyRulesBox
            // 
            this.buyRulesBox.Controls.Add(this.tblPanelBuyRule);
            this.buyRulesBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buyRulesBox.Location = new System.Drawing.Point(3, 3);
            this.buyRulesBox.Name = "buyRulesBox";
            this.buyRulesBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buyRulesBox.Size = new System.Drawing.Size(504, 58);
            this.buyRulesBox.TabIndex = 10;
            this.buyRulesBox.TabStop = false;
            this.buyRulesBox.Text = "Buy Rule";
            // 
            // tblPanelBuyRule
            // 
            this.tblPanelBuyRule.BackColor = System.Drawing.Color.Transparent;
            this.tblPanelBuyRule.ColumnCount = 1;
            this.tblPanelBuyRule.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPanelBuyRule.Controls.Add(this.panel3, 0, 0);
            this.tblPanelBuyRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPanelBuyRule.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tblPanelBuyRule.Location = new System.Drawing.Point(3, 16);
            this.tblPanelBuyRule.Name = "tblPanelBuyRule";
            this.tblPanelBuyRule.RowCount = 2;
            this.tblPanelBuyRule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblPanelBuyRule.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblPanelBuyRule.Size = new System.Drawing.Size(498, 39);
            this.tblPanelBuyRule.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnBuyRuleExpandCollapse);
            this.panel3.Controls.Add(this.analysisBuy_RTxtBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(492, 26);
            this.panel3.TabIndex = 0;
            // 
            // btnBuyRuleExpandCollapse
            // 
            this.btnBuyRuleExpandCollapse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuyRuleExpandCollapse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBuyRuleExpandCollapse.FlatAppearance.BorderSize = 0;
            this.btnBuyRuleExpandCollapse.ForeColor = System.Drawing.Color.Black;
            this.btnBuyRuleExpandCollapse.Image = global::MyMarketAnalyzer.Properties.Resources.expand_icon;
            this.btnBuyRuleExpandCollapse.Location = new System.Drawing.Point(462, 4);
            this.btnBuyRuleExpandCollapse.Margin = new System.Windows.Forms.Padding(0);
            this.btnBuyRuleExpandCollapse.Name = "btnBuyRuleExpandCollapse";
            this.btnBuyRuleExpandCollapse.Size = new System.Drawing.Size(30, 22);
            this.btnBuyRuleExpandCollapse.TabIndex = 12;
            this.btnBuyRuleExpandCollapse.UseVisualStyleBackColor = true;
            this.btnBuyRuleExpandCollapse.Click += new System.EventHandler(this.btnBuyRuleExpandCollapse_Click);
            // 
            // analysisBuy_RTxtBox
            // 
            this.analysisBuy_RTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.analysisBuy_RTxtBox.Location = new System.Drawing.Point(0, 5);
            this.analysisBuy_RTxtBox.Name = "analysisBuy_RTxtBox";
            this.analysisBuy_RTxtBox.Size = new System.Drawing.Size(465, 22);
            this.analysisBuy_RTxtBox.TabIndex = 11;
            this.analysisBuy_RTxtBox.Text = "";
            this.analysisBuy_RTxtBox.Enter += new System.EventHandler(this.buyRTBox_OnFocus);
            // 
            // analysisToolbox1
            // 
            this.analysisToolbox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.analysisToolbox1.Location = new System.Drawing.Point(3, 131);
            this.analysisToolbox1.Name = "analysisToolbox1";
            this.analysisToolbox1.Size = new System.Drawing.Size(504, 186);
            this.analysisToolbox1.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblLiveDataStatus2);
            this.panel1.Controls.Add(this.lblHistDataStatus2);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(507, 30);
            this.panel1.TabIndex = 3;
            // 
            // lblLiveDataStatus2
            // 
            this.lblLiveDataStatus2.AutoSize = true;
            this.lblLiveDataStatus2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLiveDataStatus2.ForeColor = System.Drawing.Color.Red;
            this.lblLiveDataStatus2.Location = new System.Drawing.Point(365, 7);
            this.lblLiveDataStatus2.Name = "lblLiveDataStatus2";
            this.lblLiveDataStatus2.Size = new System.Drawing.Size(62, 15);
            this.lblLiveDataStatus2.TabIndex = 5;
            this.lblLiveDataStatus2.Text = "CLOSED";
            // 
            // lblHistDataStatus2
            // 
            this.lblHistDataStatus2.AutoSize = true;
            this.lblHistDataStatus2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHistDataStatus2.ForeColor = System.Drawing.Color.Red;
            this.lblHistDataStatus2.Location = new System.Drawing.Point(99, 7);
            this.lblHistDataStatus2.Name = "lblHistDataStatus2";
            this.lblHistDataStatus2.Size = new System.Drawing.Size(97, 15);
            this.lblHistDataStatus2.TabIndex = 4;
            this.lblHistDataStatus2.Text = "UNAVAILABLE";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(251, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 15);
            this.label9.TabIndex = 3;
            this.label9.Text = "Live Data Session:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "Historical Data:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.analysisAmtHelpBtn);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.analysisAmtTxt);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.analysisSelectBox);
            this.groupBox1.Location = new System.Drawing.Point(4, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(503, 41);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Security";
            // 
            // analysisAmtHelpBtn
            // 
            this.analysisAmtHelpBtn.BackColor = System.Drawing.Color.Transparent;
            this.analysisAmtHelpBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.analysisAmtHelpBtn.Image = global::MyMarketAnalyzer.Properties.Resources.help_blue;
            this.analysisAmtHelpBtn.Location = new System.Drawing.Point(183, 12);
            this.analysisAmtHelpBtn.Margin = new System.Windows.Forms.Padding(0);
            this.analysisAmtHelpBtn.Name = "analysisAmtHelpBtn";
            this.analysisAmtHelpBtn.Size = new System.Drawing.Size(24, 24);
            this.analysisAmtHelpBtn.TabIndex = 4;
            this.analysisAmtHelpBtn.TabStop = false;
            this.analysisAmtHelpBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.analysisAmtHelp_MouseDown);
            this.analysisAmtHelpBtn.MouseEnter += new System.EventHandler(this.analysisAmtHelp_MouseEnter);
            this.analysisAmtHelpBtn.MouseLeave += new System.EventHandler(this.analysisAmtHelp_MouseLeave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Principal Amount:";
            // 
            // analysisAmtTxt
            // 
            this.analysisAmtTxt.Location = new System.Drawing.Point(98, 14);
            this.analysisAmtTxt.Name = "analysisAmtTxt";
            this.analysisAmtTxt.Size = new System.Drawing.Size(84, 20);
            this.analysisAmtTxt.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(210, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Name / Symbol:";
            // 
            // analysisSelectBox
            // 
            this.analysisSelectBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.analysisSelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.analysisSelectBox.FormattingEnabled = true;
            this.analysisSelectBox.Location = new System.Drawing.Point(299, 13);
            this.analysisSelectBox.Name = "analysisSelectBox";
            this.analysisSelectBox.Size = new System.Drawing.Size(198, 21);
            this.analysisSelectBox.TabIndex = 0;
            this.analysisSelectBox.SelectedIndexChanged += new System.EventHandler(this.analysisSelectBox_SelectedIndexChanged);
            // 
            // splitContainer3
            // 
            this.splitContainer3.BackColor = System.Drawing.Color.AliceBlue;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.panelAnalysis1);
            this.splitContainer3.Panel1.Controls.Add(this.chartAnalysis);
            this.splitContainer3.Panel1MinSize = 100;
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.analysisSummaryPage1);
            this.splitContainer3.Panel2MinSize = 100;
            this.splitContainer3.Size = new System.Drawing.Size(451, 430);
            this.splitContainer3.SplitterDistance = 248;
            this.splitContainer3.TabIndex = 5;
            this.splitContainer3.Paint += new System.Windows.Forms.PaintEventHandler(this.analysis_nestedSplitPanelRightOnPaint);
            // 
            // panelAnalysis1
            // 
            this.panelAnalysis1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAnalysis1.BackColor = System.Drawing.Color.White;
            this.panelAnalysis1.Controls.Add(this.lblChartDate);
            this.panelAnalysis1.Controls.Add(this.btnChartNext);
            this.panelAnalysis1.Controls.Add(this.btnChartPrev);
            this.panelAnalysis1.Location = new System.Drawing.Point(0, 2);
            this.panelAnalysis1.Name = "panelAnalysis1";
            this.panelAnalysis1.Size = new System.Drawing.Size(449, 29);
            this.panelAnalysis1.TabIndex = 4;
            this.panelAnalysis1.Visible = false;
            // 
            // lblChartDate
            // 
            this.lblChartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChartDate.AutoSize = true;
            this.lblChartDate.Location = new System.Drawing.Point(213, 9);
            this.lblChartDate.Name = "lblChartDate";
            this.lblChartDate.Size = new System.Drawing.Size(79, 13);
            this.lblChartDate.TabIndex = 2;
            this.lblChartDate.Text = "MM/DD/YYYY";
            this.lblChartDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnChartNext
            // 
            this.btnChartNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChartNext.Location = new System.Drawing.Point(424, 4);
            this.btnChartNext.Name = "btnChartNext";
            this.btnChartNext.Size = new System.Drawing.Size(25, 23);
            this.btnChartNext.TabIndex = 1;
            this.btnChartNext.Text = ">";
            this.btnChartNext.UseVisualStyleBackColor = true;
            this.btnChartNext.Click += new System.EventHandler(this.btnChartNext_Click);
            // 
            // btnChartPrev
            // 
            this.btnChartPrev.Location = new System.Drawing.Point(0, 4);
            this.btnChartPrev.Name = "btnChartPrev";
            this.btnChartPrev.Size = new System.Drawing.Size(25, 23);
            this.btnChartPrev.TabIndex = 0;
            this.btnChartPrev.Text = "<";
            this.btnChartPrev.UseVisualStyleBackColor = true;
            this.btnChartPrev.Click += new System.EventHandler(this.btnChartPrev_Click);
            // 
            // chartAnalysis
            // 
            this.chartAnalysis.BackColor = System.Drawing.Color.Silver;
            this.chartAnalysis.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalRight;
            chartArea1.BackColor = System.Drawing.Color.Silver;
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalRight;
            chartArea1.Name = "ChartArea1";
            this.chartAnalysis.ChartAreas.Add(chartArea1);
            this.chartAnalysis.CurrentSeriesIndex = 0;
            this.chartAnalysis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartAnalysis.Location = new System.Drawing.Point(0, 0);
            this.chartAnalysis.Name = "chartAnalysis";
            this.chartAnalysis.Size = new System.Drawing.Size(451, 248);
            this.chartAnalysis.TabIndex = 0;
            this.chartAnalysis.Text = "chart1";
            // 
            // analysisSummaryPage1
            // 
            this.analysisSummaryPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analysisSummaryPage1.Location = new System.Drawing.Point(0, 0);
            this.analysisSummaryPage1.Name = "analysisSummaryPage1";
            this.analysisSummaryPage1.Size = new System.Drawing.Size(451, 178);
            this.analysisSummaryPage1.TabIndex = 0;
            // 
            // toolStripAnalysis
            // 
            this.toolStripAnalysis.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsHistSourceDir2,
            this.toolStripLabel2,
            this.btnLoadPatternForm});
            this.toolStripAnalysis.Location = new System.Drawing.Point(3, 3);
            this.toolStripAnalysis.Name = "toolStripAnalysis";
            this.toolStripAnalysis.Size = new System.Drawing.Size(967, 25);
            this.toolStripAnalysis.TabIndex = 0;
            this.toolStripAnalysis.Text = "toolStrip1";
            // 
            // tsHistSourceDir2
            // 
            this.tsHistSourceDir2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsHistSourceDir2.Name = "tsHistSourceDir2";
            this.tsHistSourceDir2.ReadOnly = true;
            this.tsHistSourceDir2.Size = new System.Drawing.Size(433, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(89, 22);
            this.toolStripLabel2.Text = "Current Source:";
            // 
            // btnLoadPatternForm
            // 
            this.btnLoadPatternForm.Image = global::MyMarketAnalyzer.Properties.Resources.ptn_icon;
            this.btnLoadPatternForm.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnLoadPatternForm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadPatternForm.Name = "btnLoadPatternForm";
            this.btnLoadPatternForm.Size = new System.Drawing.Size(167, 22);
            this.btnLoadPatternForm.Text = "Algorithm Design Toolbox";
            this.btnLoadPatternForm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoadPatternForm.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.btnLoadPatternForm.Click += new System.EventHandler(this.btnLoadPatternForm_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(973, 460);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Clipboard";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsHistSourceDir1,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(973, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsHistSourceDir1
            // 
            this.tsHistSourceDir1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsHistSourceDir1.Name = "tsHistSourceDir1";
            this.tsHistSourceDir1.ReadOnly = true;
            this.tsHistSourceDir1.Size = new System.Drawing.Size(433, 25);
            this.tsHistSourceDir1.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(89, 22);
            this.toolStripLabel1.Text = "Current Source:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cbAnalysisIndicatorY);
            this.groupBox2.Controls.Add(this.cbAnalysisType);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.btnAnalysisShowChart);
            this.groupBox2.Controls.Add(this.cbAnalysisIndicatorX);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(16, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(503, 103);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Custom Indicators";
            // 
            // cbAnalysisIndicatorY
            // 
            this.cbAnalysisIndicatorY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnalysisIndicatorY.FormattingEnabled = true;
            this.cbAnalysisIndicatorY.Location = new System.Drawing.Point(86, 73);
            this.cbAnalysisIndicatorY.Name = "cbAnalysisIndicatorY";
            this.cbAnalysisIndicatorY.Size = new System.Drawing.Size(188, 21);
            this.cbAnalysisIndicatorY.TabIndex = 8;
            // 
            // cbAnalysisType
            // 
            this.cbAnalysisType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnalysisType.FormattingEnabled = true;
            this.cbAnalysisType.Location = new System.Drawing.Point(86, 19);
            this.cbAnalysisType.Name = "cbAnalysisType";
            this.cbAnalysisType.Size = new System.Drawing.Size(188, 21);
            this.cbAnalysisType.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Y-Indicator:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "X-Indicator:";
            // 
            // btnAnalysisShowChart
            // 
            this.btnAnalysisShowChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalysisShowChart.Location = new System.Drawing.Point(316, 18);
            this.btnAnalysisShowChart.Name = "btnAnalysisShowChart";
            this.btnAnalysisShowChart.Size = new System.Drawing.Size(182, 23);
            this.btnAnalysisShowChart.TabIndex = 6;
            this.btnAnalysisShowChart.Text = "Run Chart";
            this.btnAnalysisShowChart.UseVisualStyleBackColor = true;
            // 
            // cbAnalysisIndicatorX
            // 
            this.cbAnalysisIndicatorX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnalysisIndicatorX.FormattingEnabled = true;
            this.cbAnalysisIndicatorX.Location = new System.Drawing.Point(86, 46);
            this.cbAnalysisIndicatorX.Name = "cbAnalysisIndicatorX";
            this.cbAnalysisIndicatorX.Size = new System.Drawing.Size(188, 21);
            this.cbAnalysisIndicatorX.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Chart Type:";
            // 
            // backgroundWorkerStat
            // 
            this.backgroundWorkerStat.WorkerReportsProgress = true;
            this.backgroundWorkerStat.WorkerSupportsCancellation = true;
            this.backgroundWorkerStat.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerStat_DoWork);
            // 
            // backgroundWorkerProgress
            // 
            this.backgroundWorkerProgress.WorkerReportsProgress = true;
            this.backgroundWorkerProgress.WorkerSupportsCancellation = true;
            this.backgroundWorkerProgress.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerProgress_DoWork);
            this.backgroundWorkerProgress.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerProgress_ProgressChanged);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(967, 373);
            // 
            // backgroundWorkerAnalysisProgress
            // 
            this.backgroundWorkerAnalysisProgress.WorkerReportsProgress = true;
            this.backgroundWorkerAnalysisProgress.WorkerSupportsCancellation = true;
            this.backgroundWorkerAnalysisProgress.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerAnalysisProgress_DoWork);
            this.backgroundWorkerAnalysisProgress.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerAnalysisProgress_ProgressChanged);
            // 
            // backgroundWorkerAnalysis
            // 
            this.backgroundWorkerAnalysis.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerAnalysis_DoWork);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 488);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "My Market Analyzer";
            this.tabControl1.ResumeLayout(false);
            this.tabHome.ResumeLayout(false);
            this.tabHome.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabStats.ResumeLayout(false);
            this.tabStats.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.dataMenuPanel.ResumeLayout(false);
            this.dataMenuPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMenuArrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStripStat.ResumeLayout(false);
            this.toolStripStat.PerformLayout();
            this.groupBoxStatFilter.ResumeLayout(false);
            this.groupBoxStatFilter.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabAnalysis.ResumeLayout(false);
            this.tabAnalysis.PerformLayout();
            this.analysisSplitContainer.Panel1.ResumeLayout(false);
            this.analysisSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.analysisSplitContainer)).EndInit();
            this.analysisSplitContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.sellRulesBox.ResumeLayout(false);
            this.tblPanelSellRule.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.buyRulesBox.ResumeLayout(false);
            this.tblPanelBuyRule.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.analysisAmtHelpBtn)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panelAnalysis1.ResumeLayout(false);
            this.panelAnalysis1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAnalysis)).EndInit();
            this.toolStripAnalysis.ResumeLayout(false);
            this.toolStripAnalysis.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabHome;
        private System.Windows.Forms.TabPage tabStats;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog dlgStatFolder;
        private System.Windows.Forms.ToolStrip toolStripStat;
        private System.Windows.Forms.ToolStripDropDownButton btnDataSrc;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.ComponentModel.BackgroundWorker backgroundWorkerStat;
        private System.ComponentModel.BackgroundWorker backgroundWorkerProgress;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExcelDowloader;
        private System.Windows.Forms.GroupBox groupBoxStatFilter;
        private System.Windows.Forms.ComboBox comboBoxStatFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtStatTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStatFrom;
        private System.Windows.Forms.Label lblStatResultNum;
        private System.Windows.Forms.TabPage tabAnalysis;
        private System.Windows.Forms.ToolStrip toolStripAnalysis;
        private System.Windows.Forms.SplitContainer analysisSplitContainer;
        //private System.Windows.Forms.DataVisualization.Charting.Chart chartAnalysis;
        private CustomChart chartAnalysis;
        private System.Windows.Forms.Button btnRunAnalysis;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblHistDataStatus2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblLiveDataStatus2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblLiveDataStatus1;
        private System.Windows.Forms.Label lblHistDataStatus1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem unloadToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox tsHistSourceDir2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.Panel panelAnalysis1;
        private System.Windows.Forms.Button btnChartPrev;
        private System.Windows.Forms.Button btnChartNext;
        private System.Windows.Forms.Label lblChartDate;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox analysisSelectBox;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbAnalysisIndicatorY;
        private System.Windows.Forms.ComboBox cbAnalysisType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAnalysisShowChart;
        private System.Windows.Forms.ComboBox cbAnalysisIndicatorX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox sellRulesBox;
        private System.Windows.Forms.GroupBox buyRulesBox;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton btnVisualsMenu;
        private System.Windows.Forms.ToolStripMenuItem showChartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem heatMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnLoadPatternForm;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStripMenuItem random10ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox tsHistSourceDir1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox cbRegionSelect;
        private System.Windows.Forms.ToolStripLabel tsRegionImg;
        private System.Windows.Forms.ToolStripButton imgRegionFlag;
        private System.Windows.Forms.ToolStripComboBox cbMarketSelect;
        private System.Windows.Forms.Label lblPoweredBy;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ProgressBar progressStats;
        private System.Windows.Forms.Label lblUpdate;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private Watchlist watchlist1;
        private Ticker ticker1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem loadProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Panel dataMenuPanel;
        private System.Windows.Forms.ComboBox cbLiveDataInterval;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox dataMenuArrow;
        private StatTable tblStatTableMain;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.TextBox analysisAmtTxt;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox analysisAmtHelpBtn;
        private System.ComponentModel.BackgroundWorker backgroundWorkerAnalysisProgress;
        private AnalysisSummaryPage analysisSummaryPage1;
        private System.ComponentModel.BackgroundWorker backgroundWorkerAnalysis;
        private System.Windows.Forms.TableLayoutPanel tblPanelBuyRule;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnBuyRuleExpandCollapse;
        private System.Windows.Forms.RichTextBox analysisBuy_RTxtBox;
        private System.Windows.Forms.TableLayoutPanel tblPanelSellRule;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnSellRuleExpandCollapse;
        private System.Windows.Forms.RichTextBox analysisSell_RTxtBox;
        private AnalysisToolbox analysisToolbox1;
    }
}

