namespace MyMarketAnalyzer
{
    partial class AlgorithmDesignForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageData = new System.Windows.Forms.TabPage();
            this.dataTableMain = new MyMarketAnalyzer.StatTable();
            this.tabPageChart = new System.Windows.Forms.TabPage();
            this.btnChartSliderLeft = new System.Windows.Forms.Button();
            this.btnChartSliderRight = new System.Windows.Forms.Button();
            this.chartSlider = new System.Windows.Forms.TrackBar();
            this.chartMain = new MyMarketAnalyzer.CustomChart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.autofillCheckBox = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbParameters1 = new System.Windows.Forms.GroupBox();
            this.lblParam1 = new System.Windows.Forms.Label();
            this.lblParam3 = new System.Windows.Forms.Label();
            this.numParam1 = new System.Windows.Forms.NumericUpDown();
            this.numParam3 = new System.Windows.Forms.NumericUpDown();
            this.numParam2 = new System.Windows.Forms.NumericUpDown();
            this.lblParam2 = new System.Windows.Forms.Label();
            this.gbParameters2 = new System.Windows.Forms.GroupBox();
            this.gbParameters3 = new System.Windows.Forms.GroupBox();
            this.gbPrototype = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbProtoAttributeY = new System.Windows.Forms.ComboBox();
            this.cbProtoFunction = new System.Windows.Forms.ComboBox();
            this.cbProtoAttributeX = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numGroupMax = new System.Windows.Forms.NumericUpDown();
            this.numGroupMin = new System.Windows.Forms.NumericUpDown();
            this.numTestPct = new System.Windows.Forms.NumericUpDown();
            this.algorithmSelectListBox = new System.Windows.Forms.ListBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.trackBarTrainingPct = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numTrainingPct = new System.Windows.Forms.NumericUpDown();
            this.tabConsoleContainer = new System.Windows.Forms.TabControl();
            this.tabPageFnOptions = new System.Windows.Forms.TabPage();
            this.tabPageConsole = new System.Windows.Forms.TabPage();
            this.consoleTxt = new System.Windows.Forms.RichTextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageData.SuspendLayout();
            this.tabPageChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbParameters1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numParam1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numParam3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numParam2)).BeginInit();
            this.gbPrototype.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGroupMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGroupMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTestPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTrainingPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTrainingPct)).BeginInit();
            this.tabConsoleContainer.SuspendLayout();
            this.tabPageConsole.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabConsoleContainer, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1741, 652);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 2);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1741, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageData);
            this.tabControl1.Controls.Add(this.tabPageChart);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(4, 183);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(862, 465);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPageData
            // 
            this.tabPageData.Controls.Add(this.dataTableMain);
            this.tabPageData.Location = new System.Drawing.Point(4, 25);
            this.tabPageData.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageData.Name = "tabPageData";
            this.tabPageData.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageData.Size = new System.Drawing.Size(854, 436);
            this.tabPageData.TabIndex = 0;
            this.tabPageData.Text = "Data";
            this.tabPageData.UseVisualStyleBackColor = true;
            // 
            // dataTableMain
            // 
            this.dataTableMain.AutoScroll = true;
            this.dataTableMain.AutoSize = true;
            this.dataTableMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataTableMain.Location = new System.Drawing.Point(4, 4);
            this.dataTableMain.Margin = new System.Windows.Forms.Padding(5);
            this.dataTableMain.Name = "dataTableMain";
            this.dataTableMain.Size = new System.Drawing.Size(846, 428);
            this.dataTableMain.TabIndex = 0;
            this.dataTableMain.TableType = MyMarketAnalyzer.StatTableType.HIST_STATS;
            // 
            // tabPageChart
            // 
            this.tabPageChart.Controls.Add(this.btnChartSliderLeft);
            this.tabPageChart.Controls.Add(this.btnChartSliderRight);
            this.tabPageChart.Controls.Add(this.chartSlider);
            this.tabPageChart.Controls.Add(this.chartMain);
            this.tabPageChart.Location = new System.Drawing.Point(4, 25);
            this.tabPageChart.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageChart.Name = "tabPageChart";
            this.tabPageChart.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageChart.Size = new System.Drawing.Size(854, 436);
            this.tabPageChart.TabIndex = 1;
            this.tabPageChart.Text = "Chart";
            this.tabPageChart.UseVisualStyleBackColor = true;
            // 
            // btnChartSliderLeft
            // 
            this.btnChartSliderLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChartSliderLeft.Location = new System.Drawing.Point(4, 372);
            this.btnChartSliderLeft.Margin = new System.Windows.Forms.Padding(4);
            this.btnChartSliderLeft.Name = "btnChartSliderLeft";
            this.btnChartSliderLeft.Size = new System.Drawing.Size(28, 55);
            this.btnChartSliderLeft.TabIndex = 5;
            this.btnChartSliderLeft.Text = "<";
            this.btnChartSliderLeft.UseVisualStyleBackColor = true;
            this.btnChartSliderLeft.Click += new System.EventHandler(this.btnChartSliderLeft_Click);
            // 
            // btnChartSliderRight
            // 
            this.btnChartSliderRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChartSliderRight.Location = new System.Drawing.Point(815, 372);
            this.btnChartSliderRight.Margin = new System.Windows.Forms.Padding(4);
            this.btnChartSliderRight.Name = "btnChartSliderRight";
            this.btnChartSliderRight.Size = new System.Drawing.Size(28, 55);
            this.btnChartSliderRight.TabIndex = 4;
            this.btnChartSliderRight.Text = ">";
            this.btnChartSliderRight.UseVisualStyleBackColor = true;
            this.btnChartSliderRight.Click += new System.EventHandler(this.btnChartSliderRight_Click);
            // 
            // chartSlider
            // 
            this.chartSlider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartSlider.LargeChange = 10;
            this.chartSlider.Location = new System.Drawing.Point(33, 372);
            this.chartSlider.Margin = new System.Windows.Forms.Padding(4);
            this.chartSlider.Maximum = 100;
            this.chartSlider.Name = "chartSlider";
            this.chartSlider.Size = new System.Drawing.Size(780, 56);
            this.chartSlider.TabIndex = 3;
            this.chartSlider.TickFrequency = 5;
            this.chartSlider.Scroll += new System.EventHandler(this.chartSlider_Scroll);
            // 
            // chartMain
            // 
            this.chartMain.AllowZoom = false;
            this.chartMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartMain.CandleStickEnabled = false;
            chartArea1.Name = "ChartArea1";
            this.chartMain.ChartAreas.Add(chartArea1);
            this.chartMain.CurrentSeriesIndex = 0;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "chartLegend";
            this.chartMain.Legends.Add(legend1);
            this.chartMain.Location = new System.Drawing.Point(4, 4);
            this.chartMain.Margin = new System.Windows.Forms.Padding(4);
            this.chartMain.Name = "chartMain";
            this.chartMain.Size = new System.Drawing.Size(839, 361);
            this.chartMain.TabIndex = 0;
            this.chartMain.Text = "customChart1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.numTestPct);
            this.panel1.Controls.Add(this.algorithmSelectListBox);
            this.panel1.Controls.Add(this.btnRun);
            this.panel1.Controls.Add(this.trackBarTrainingPct);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.numTrainingPct);
            this.panel1.Location = new System.Drawing.Point(4, 35);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1733, 140);
            this.panel1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.autofillCheckBox);
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Controls.Add(this.gbPrototype);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numGroupMax);
            this.groupBox1.Controls.Add(this.numGroupMin);
            this.groupBox1.Location = new System.Drawing.Point(583, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1147, 140);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // autofillCheckBox
            // 
            this.autofillCheckBox.AutoSize = true;
            this.autofillCheckBox.Location = new System.Drawing.Point(8, 116);
            this.autofillCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.autofillCheckBox.Name = "autofillCheckBox";
            this.autofillCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.autofillCheckBox.Size = new System.Drawing.Size(153, 21);
            this.autofillCheckBox.TabIndex = 17;
            this.autofillCheckBox.Text = "Auto-Fill Date Gaps";
            this.autofillCheckBox.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.gbParameters1);
            this.flowLayoutPanel1.Controls.Add(this.gbParameters2);
            this.flowLayoutPanel1.Controls.Add(this.gbParameters3);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(456, 10);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(691, 127);
            this.flowLayoutPanel1.TabIndex = 16;
            // 
            // gbParameters1
            // 
            this.gbParameters1.Controls.Add(this.lblParam1);
            this.gbParameters1.Controls.Add(this.lblParam3);
            this.gbParameters1.Controls.Add(this.numParam1);
            this.gbParameters1.Controls.Add(this.numParam3);
            this.gbParameters1.Controls.Add(this.numParam2);
            this.gbParameters1.Controls.Add(this.lblParam2);
            this.gbParameters1.Location = new System.Drawing.Point(4, 0);
            this.gbParameters1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.gbParameters1.Name = "gbParameters1";
            this.gbParameters1.Padding = new System.Windows.Forms.Padding(4);
            this.gbParameters1.Size = new System.Drawing.Size(221, 119);
            this.gbParameters1.TabIndex = 18;
            this.gbParameters1.TabStop = false;
            this.gbParameters1.Text = "Parameters";
            this.gbParameters1.Visible = false;
            // 
            // lblParam1
            // 
            this.lblParam1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblParam1.AutoSize = true;
            this.lblParam1.Location = new System.Drawing.Point(96, 20);
            this.lblParam1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblParam1.Name = "lblParam1";
            this.lblParam1.Size = new System.Drawing.Size(56, 17);
            this.lblParam1.TabIndex = 9;
            this.lblParam1.Text = "param1";
            this.lblParam1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblParam3
            // 
            this.lblParam3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblParam3.AutoSize = true;
            this.lblParam3.Location = new System.Drawing.Point(96, 92);
            this.lblParam3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblParam3.Name = "lblParam3";
            this.lblParam3.Size = new System.Drawing.Size(56, 17);
            this.lblParam3.TabIndex = 13;
            this.lblParam3.Text = "param3";
            // 
            // numParam1
            // 
            this.numParam1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numParam1.Location = new System.Drawing.Point(160, 17);
            this.numParam1.Margin = new System.Windows.Forms.Padding(4);
            this.numParam1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numParam1.Name = "numParam1";
            this.numParam1.Size = new System.Drawing.Size(53, 22);
            this.numParam1.TabIndex = 8;
            this.numParam1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numParam3
            // 
            this.numParam3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numParam3.Location = new System.Drawing.Point(160, 90);
            this.numParam3.Margin = new System.Windows.Forms.Padding(4);
            this.numParam3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numParam3.Name = "numParam3";
            this.numParam3.Size = new System.Drawing.Size(53, 22);
            this.numParam3.TabIndex = 12;
            this.numParam3.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numParam2
            // 
            this.numParam2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numParam2.Location = new System.Drawing.Point(160, 53);
            this.numParam2.Margin = new System.Windows.Forms.Padding(4);
            this.numParam2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numParam2.Name = "numParam2";
            this.numParam2.Size = new System.Drawing.Size(53, 22);
            this.numParam2.TabIndex = 10;
            this.numParam2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblParam2
            // 
            this.lblParam2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblParam2.AutoSize = true;
            this.lblParam2.Location = new System.Drawing.Point(96, 55);
            this.lblParam2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblParam2.Name = "lblParam2";
            this.lblParam2.Size = new System.Drawing.Size(56, 17);
            this.lblParam2.TabIndex = 11;
            this.lblParam2.Text = "param2";
            // 
            // gbParameters2
            // 
            this.gbParameters2.Location = new System.Drawing.Point(233, 0);
            this.gbParameters2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.gbParameters2.Name = "gbParameters2";
            this.gbParameters2.Padding = new System.Windows.Forms.Padding(4);
            this.gbParameters2.Size = new System.Drawing.Size(221, 119);
            this.gbParameters2.TabIndex = 19;
            this.gbParameters2.TabStop = false;
            this.gbParameters2.Visible = false;
            // 
            // gbParameters3
            // 
            this.gbParameters3.Location = new System.Drawing.Point(462, 0);
            this.gbParameters3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.gbParameters3.Name = "gbParameters3";
            this.gbParameters3.Padding = new System.Windows.Forms.Padding(4);
            this.gbParameters3.Size = new System.Drawing.Size(221, 119);
            this.gbParameters3.TabIndex = 20;
            this.gbParameters3.TabStop = false;
            this.gbParameters3.Visible = false;
            // 
            // gbPrototype
            // 
            this.gbPrototype.Controls.Add(this.label7);
            this.gbPrototype.Controls.Add(this.label6);
            this.gbPrototype.Controls.Add(this.label5);
            this.gbPrototype.Controls.Add(this.cbProtoAttributeY);
            this.gbPrototype.Controls.Add(this.cbProtoFunction);
            this.gbPrototype.Controls.Add(this.cbProtoAttributeX);
            this.gbPrototype.Location = new System.Drawing.Point(185, 4);
            this.gbPrototype.Margin = new System.Windows.Forms.Padding(4);
            this.gbPrototype.Name = "gbPrototype";
            this.gbPrototype.Padding = new System.Windows.Forms.Padding(4);
            this.gbPrototype.Size = new System.Drawing.Size(267, 133);
            this.gbPrototype.TabIndex = 15;
            this.gbPrototype.TabStop = false;
            this.gbPrototype.Text = "Feature Selection";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1, 90);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 24);
            this.label7.TabIndex = 18;
            this.label7.Text = "Fn:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 54);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 24);
            this.label6.TabIndex = 17;
            this.label6.Text = "Y:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 18);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 24);
            this.label5.TabIndex = 16;
            this.label5.Text = "X:";
            // 
            // cbProtoAttributeY
            // 
            this.cbProtoAttributeY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProtoAttributeY.FormattingEnabled = true;
            this.cbProtoAttributeY.Location = new System.Drawing.Point(43, 52);
            this.cbProtoAttributeY.Margin = new System.Windows.Forms.Padding(4);
            this.cbProtoAttributeY.Name = "cbProtoAttributeY";
            this.cbProtoAttributeY.Size = new System.Drawing.Size(212, 24);
            this.cbProtoAttributeY.TabIndex = 2;
            this.cbProtoAttributeY.SelectedIndexChanged += new System.EventHandler(this.cbProtoAttribute_YIndexChanged);
            // 
            // cbProtoFunction
            // 
            this.cbProtoFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProtoFunction.FormattingEnabled = true;
            this.cbProtoFunction.Location = new System.Drawing.Point(43, 89);
            this.cbProtoFunction.Margin = new System.Windows.Forms.Padding(4);
            this.cbProtoFunction.Name = "cbProtoFunction";
            this.cbProtoFunction.Size = new System.Drawing.Size(212, 24);
            this.cbProtoFunction.TabIndex = 1;
            this.cbProtoFunction.SelectedIndexChanged += new System.EventHandler(this.cbProtoFunction_SelectedIndexChanged);
            // 
            // cbProtoAttributeX
            // 
            this.cbProtoAttributeX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProtoAttributeX.FormattingEnabled = true;
            this.cbProtoAttributeX.Location = new System.Drawing.Point(43, 17);
            this.cbProtoAttributeX.Margin = new System.Windows.Forms.Padding(4);
            this.cbProtoAttributeX.Name = "cbProtoAttributeX";
            this.cbProtoAttributeX.Size = new System.Drawing.Size(212, 24);
            this.cbProtoAttributeX.TabIndex = 0;
            this.cbProtoAttributeX.SelectedIndexChanged += new System.EventHandler(this.cbProtoAttribute_XIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 48);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Max. Class Size:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Min. Class Size:";
            // 
            // numGroupMax
            // 
            this.numGroupMax.Location = new System.Drawing.Point(124, 46);
            this.numGroupMax.Margin = new System.Windows.Forms.Padding(4);
            this.numGroupMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numGroupMax.Name = "numGroupMax";
            this.numGroupMax.Size = new System.Drawing.Size(53, 22);
            this.numGroupMax.TabIndex = 5;
            this.numGroupMax.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numGroupMin
            // 
            this.numGroupMin.Location = new System.Drawing.Point(124, 10);
            this.numGroupMin.Margin = new System.Windows.Forms.Padding(4);
            this.numGroupMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numGroupMin.Name = "numGroupMin";
            this.numGroupMin.Size = new System.Drawing.Size(53, 22);
            this.numGroupMin.TabIndex = 4;
            this.numGroupMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numTestPct
            // 
            this.numTestPct.Location = new System.Drawing.Point(457, 10);
            this.numTestPct.Margin = new System.Windows.Forms.Padding(4);
            this.numTestPct.Name = "numTestPct";
            this.numTestPct.Size = new System.Drawing.Size(53, 22);
            this.numTestPct.TabIndex = 4;
            this.numTestPct.ValueChanged += new System.EventHandler(this.numTestPct_ValueChanged);
            // 
            // algorithmSelectListBox
            // 
            this.algorithmSelectListBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.algorithmSelectListBox.FormattingEnabled = true;
            this.algorithmSelectListBox.ItemHeight = 17;
            this.algorithmSelectListBox.Location = new System.Drawing.Point(4, 4);
            this.algorithmSelectListBox.Margin = new System.Windows.Forms.Padding(4);
            this.algorithmSelectListBox.Name = "algorithmSelectListBox";
            this.algorithmSelectListBox.Size = new System.Drawing.Size(257, 123);
            this.algorithmSelectListBox.TabIndex = 0;
            this.algorithmSelectListBox.SelectedIndexChanged += new System.EventHandler(this.algorithm_SelectionChanged);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(273, 111);
            this.btnRun.Margin = new System.Windows.Forms.Padding(4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(301, 27);
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "Run Classifier";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // trackBarTrainingPct
            // 
            this.trackBarTrainingPct.LargeChange = 10;
            this.trackBarTrainingPct.Location = new System.Drawing.Point(273, 48);
            this.trackBarTrainingPct.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarTrainingPct.Maximum = 100;
            this.trackBarTrainingPct.Name = "trackBarTrainingPct";
            this.trackBarTrainingPct.Size = new System.Drawing.Size(301, 56);
            this.trackBarTrainingPct.SmallChange = 5;
            this.trackBarTrainingPct.TabIndex = 2;
            this.trackBarTrainingPct.TickFrequency = 5;
            this.trackBarTrainingPct.Value = 75;
            this.trackBarTrainingPct.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(519, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "% Test";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(273, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "% Training";
            // 
            // numTrainingPct
            // 
            this.numTrainingPct.Location = new System.Drawing.Point(356, 10);
            this.numTrainingPct.Margin = new System.Windows.Forms.Padding(4);
            this.numTrainingPct.Name = "numTrainingPct";
            this.numTrainingPct.Size = new System.Drawing.Size(53, 22);
            this.numTrainingPct.TabIndex = 3;
            this.numTrainingPct.ValueChanged += new System.EventHandler(this.numTrainingPct_ValueChanged);
            // 
            // tabConsoleContainer
            // 
            this.tabConsoleContainer.Controls.Add(this.tabPageFnOptions);
            this.tabConsoleContainer.Controls.Add(this.tabPageConsole);
            this.tabConsoleContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabConsoleContainer.Location = new System.Drawing.Point(874, 183);
            this.tabConsoleContainer.Margin = new System.Windows.Forms.Padding(4);
            this.tabConsoleContainer.Name = "tabConsoleContainer";
            this.tabConsoleContainer.SelectedIndex = 0;
            this.tabConsoleContainer.Size = new System.Drawing.Size(863, 465);
            this.tabConsoleContainer.TabIndex = 4;
            // 
            // tabPageFnOptions
            // 
            this.tabPageFnOptions.Location = new System.Drawing.Point(4, 25);
            this.tabPageFnOptions.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageFnOptions.Name = "tabPageFnOptions";
            this.tabPageFnOptions.Size = new System.Drawing.Size(855, 436);
            this.tabPageFnOptions.TabIndex = 1;
            this.tabPageFnOptions.Text = "Function";
            this.tabPageFnOptions.UseVisualStyleBackColor = true;
            // 
            // tabPageConsole
            // 
            this.tabPageConsole.Controls.Add(this.consoleTxt);
            this.tabPageConsole.Location = new System.Drawing.Point(4, 25);
            this.tabPageConsole.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageConsole.Name = "tabPageConsole";
            this.tabPageConsole.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageConsole.Size = new System.Drawing.Size(855, 436);
            this.tabPageConsole.TabIndex = 0;
            this.tabPageConsole.Text = "Console";
            this.tabPageConsole.UseVisualStyleBackColor = true;
            // 
            // consoleTxt
            // 
            this.consoleTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.consoleTxt.Location = new System.Drawing.Point(4, 4);
            this.consoleTxt.Margin = new System.Windows.Forms.Padding(4);
            this.consoleTxt.Name = "consoleTxt";
            this.consoleTxt.Size = new System.Drawing.Size(847, 428);
            this.consoleTxt.TabIndex = 1;
            this.consoleTxt.Text = "";
            this.consoleTxt.TextChanged += new System.EventHandler(this.console_TextChanged);
            // 
            // AlgorithmDesignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1741, 652);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1320, 699);
            this.Name = "AlgorithmDesignForm";
            this.Text = "Pattern Discovery";
            this.Shown += new System.EventHandler(this.algDesignForm_FirstShown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageData.ResumeLayout(false);
            this.tabPageData.PerformLayout();
            this.tabPageChart.ResumeLayout(false);
            this.tabPageChart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.gbParameters1.ResumeLayout(false);
            this.gbParameters1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numParam1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numParam3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numParam2)).EndInit();
            this.gbPrototype.ResumeLayout(false);
            this.gbPrototype.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGroupMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGroupMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTestPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTrainingPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTrainingPct)).EndInit();
            this.tabConsoleContainer.ResumeLayout(false);
            this.tabPageConsole.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageData;
        private StatTable dataTableMain;
        private System.Windows.Forms.TabPage tabPageChart;
        private CustomChart chartMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numTestPct;
        private System.Windows.Forms.ListBox algorithmSelectListBox;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TrackBar trackBarTrainingPct;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numTrainingPct;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numGroupMax;
        private System.Windows.Forms.NumericUpDown numGroupMin;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox gbPrototype;
        private System.Windows.Forms.ComboBox cbProtoFunction;
        private System.Windows.Forms.ComboBox cbProtoAttributeX;
        private System.Windows.Forms.ComboBox cbProtoAttributeY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabConsoleContainer;
        private System.Windows.Forms.TabPage tabPageConsole;
        private System.Windows.Forms.RichTextBox consoleTxt;
        private System.Windows.Forms.TabPage tabPageFnOptions;
        private System.Windows.Forms.TrackBar chartSlider;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox gbParameters1;
        private System.Windows.Forms.Label lblParam1;
        private System.Windows.Forms.Label lblParam3;
        private System.Windows.Forms.NumericUpDown numParam1;
        private System.Windows.Forms.NumericUpDown numParam3;
        private System.Windows.Forms.NumericUpDown numParam2;
        private System.Windows.Forms.Label lblParam2;
        private System.Windows.Forms.GroupBox gbParameters2;
        private System.Windows.Forms.GroupBox gbParameters3;
        private System.Windows.Forms.CheckBox autofillCheckBox;
        private System.Windows.Forms.Button btnChartSliderRight;
        private System.Windows.Forms.Button btnChartSliderLeft;

    }
}