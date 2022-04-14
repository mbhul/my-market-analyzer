namespace MyMarketAnalyzer
{
    partial class VisualsTab
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualsTab));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.tabVisualsData = new System.Windows.Forms.TabControl();
            this.tabVisualData = new System.Windows.Forms.TabPage();
            this.splitContainerTabVS = new System.Windows.Forms.SplitContainer();
            this.vsTabToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsComboTransformation = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsParam1Lbl = new System.Windows.Forms.ToolStripLabel();
            this.tsParam1Txt = new System.Windows.Forms.ToolStripTextBox();
            this.tsParam1Separator = new System.Windows.Forms.ToolStripSeparator();
            this.tsParam2Lbl = new System.Windows.Forms.ToolStripLabel();
            this.tsParam2Txt = new System.Windows.Forms.ToolStripTextBox();
            this.tsParam2Separator = new System.Windows.Forms.ToolStripSeparator();
            this.tsParam3Lbl = new System.Windows.Forms.ToolStripLabel();
            this.tsParam3Txt = new System.Windows.Forms.ToolStripTextBox();
            this.tsParam3Separator = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnTransformAll = new System.Windows.Forms.ToolStripButton();
            this.dataTransformationThread = new System.ComponentModel.BackgroundWorker();
            this.chartMain = new MyMarketAnalyzer.CustomChart();
            this.tblVisualsTab1 = new MyMarketAnalyzer.StatTable();
            this.visualSummaryTabPage1 = new MyMarketAnalyzer.VisualSummaryTabPage();
            this.tabVisualsData.SuspendLayout();
            this.tabVisualData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTabVS)).BeginInit();
            this.splitContainerTabVS.Panel1.SuspendLayout();
            this.splitContainerTabVS.Panel2.SuspendLayout();
            this.splitContainerTabVS.SuspendLayout();
            this.vsTabToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
            this.SuspendLayout();
            // 
            // tabVisualsData
            // 
            this.tabVisualsData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabVisualsData.Controls.Add(this.tabVisualData);
            this.tabVisualsData.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabVisualsData.Location = new System.Drawing.Point(0, 31);
            this.tabVisualsData.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.tabVisualsData.Name = "tabVisualsData";
            this.tabVisualsData.SelectedIndex = 0;
            this.tabVisualsData.Size = new System.Drawing.Size(1398, 136);
            this.tabVisualsData.TabIndex = 1;
            this.tabVisualsData.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabVisualsData_DrawItem);
            this.tabVisualsData.SelectedIndexChanged += new System.EventHandler(this.tabVisualsData_SelectedIndexChanged);
            this.tabVisualsData.Deselected += new System.Windows.Forms.TabControlEventHandler(this.tabVisualsData_Deselected);
            this.tabVisualsData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabVisualsData_MouseDown);
            this.tabVisualsData.MouseLeave += new System.EventHandler(this.tabVisualsData_MouseLeave);
            this.tabVisualsData.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabVisualsData_MouseMove);
            this.tabVisualsData.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tabVisualsData_MouseUp);
            // 
            // tabVisualData
            // 
            this.tabVisualData.Controls.Add(this.visualSummaryTabPage1);
            this.tabVisualData.Location = new System.Drawing.Point(4, 25);
            this.tabVisualData.Name = "tabVisualData";
            this.tabVisualData.Padding = new System.Windows.Forms.Padding(3);
            this.tabVisualData.Size = new System.Drawing.Size(1390, 107);
            this.tabVisualData.TabIndex = 0;
            this.tabVisualData.Text = "Raw Data";
            this.tabVisualData.UseVisualStyleBackColor = true;
            // 
            // splitContainerTabVS
            // 
            this.splitContainerTabVS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerTabVS.Location = new System.Drawing.Point(3, 169);
            this.splitContainerTabVS.Name = "splitContainerTabVS";
            // 
            // splitContainerTabVS.Panel1
            // 
            this.splitContainerTabVS.Panel1.Controls.Add(this.chartMain);
            this.splitContainerTabVS.Panel1MinSize = 300;
            // 
            // splitContainerTabVS.Panel2
            // 
            this.splitContainerTabVS.Panel2.Controls.Add(this.tblVisualsTab1);
            this.splitContainerTabVS.Panel2MinSize = 100;
            this.splitContainerTabVS.Size = new System.Drawing.Size(1394, 478);
            this.splitContainerTabVS.SplitterDistance = 876;
            this.splitContainerTabVS.TabIndex = 2;
            // 
            // vsTabToolStrip
            // 
            this.vsTabToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.vsTabToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsComboTransformation,
            this.toolStripSeparator1,
            this.tsBtnClear,
            this.toolStripSeparator4,
            this.tsBtnUpdate,
            this.toolStripSeparator3,
            this.tsParam1Lbl,
            this.tsParam1Txt,
            this.tsParam1Separator,
            this.tsParam2Lbl,
            this.tsParam2Txt,
            this.tsParam2Separator,
            this.tsParam3Lbl,
            this.tsParam3Txt,
            this.tsParam3Separator,
            this.tsBtnTransformAll});
            this.vsTabToolStrip.Location = new System.Drawing.Point(0, 0);
            this.vsTabToolStrip.Name = "vsTabToolStrip";
            this.vsTabToolStrip.Size = new System.Drawing.Size(1400, 28);
            this.vsTabToolStrip.TabIndex = 3;
            this.vsTabToolStrip.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(143, 25);
            this.toolStripLabel1.Text = "Apply Transformation:";
            // 
            // tsComboTransformation
            // 
            this.tsComboTransformation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsComboTransformation.Name = "tsComboTransformation";
            this.tsComboTransformation.Size = new System.Drawing.Size(121, 28);
            this.tsComboTransformation.SelectedIndexChanged += new System.EventHandler(this.tsComboTransform_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // tsBtnClear
            // 
            this.tsBtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnClear.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnClear.Image")));
            this.tsBtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnClear.Name = "tsBtnClear";
            this.tsBtnClear.Size = new System.Drawing.Size(47, 25);
            this.tsBtnClear.Text = "Clear";
            this.tsBtnClear.Click += new System.EventHandler(this.tsBtnClear_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 28);
            // 
            // tsBtnUpdate
            // 
            this.tsBtnUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnUpdate.Enabled = false;
            this.tsBtnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnUpdate.Image")));
            this.tsBtnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnUpdate.Name = "tsBtnUpdate";
            this.tsBtnUpdate.Size = new System.Drawing.Size(79, 25);
            this.tsBtnUpdate.Text = "Transform";
            this.tsBtnUpdate.Click += new System.EventHandler(this.tsBtnUpdate_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 28);
            // 
            // tsParam1Lbl
            // 
            this.tsParam1Lbl.Name = "tsParam1Lbl";
            this.tsParam1Lbl.Size = new System.Drawing.Size(58, 27);
            this.tsParam1Lbl.Text = "Param1";
            this.tsParam1Lbl.Visible = false;
            // 
            // tsParam1Txt
            // 
            this.tsParam1Txt.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsParam1Txt.Name = "tsParam1Txt";
            this.tsParam1Txt.Size = new System.Drawing.Size(30, 30);
            this.tsParam1Txt.Visible = false;
            // 
            // tsParam1Separator
            // 
            this.tsParam1Separator.Name = "tsParam1Separator";
            this.tsParam1Separator.Size = new System.Drawing.Size(6, 30);
            this.tsParam1Separator.Visible = false;
            // 
            // tsParam2Lbl
            // 
            this.tsParam2Lbl.Name = "tsParam2Lbl";
            this.tsParam2Lbl.Size = new System.Drawing.Size(58, 27);
            this.tsParam2Lbl.Text = "Param2";
            this.tsParam2Lbl.Visible = false;
            // 
            // tsParam2Txt
            // 
            this.tsParam2Txt.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsParam2Txt.Name = "tsParam2Txt";
            this.tsParam2Txt.Size = new System.Drawing.Size(30, 30);
            this.tsParam2Txt.Visible = false;
            // 
            // tsParam2Separator
            // 
            this.tsParam2Separator.Name = "tsParam2Separator";
            this.tsParam2Separator.Size = new System.Drawing.Size(6, 30);
            this.tsParam2Separator.Visible = false;
            // 
            // tsParam3Lbl
            // 
            this.tsParam3Lbl.Name = "tsParam3Lbl";
            this.tsParam3Lbl.Size = new System.Drawing.Size(58, 27);
            this.tsParam3Lbl.Text = "Param3";
            this.tsParam3Lbl.Visible = false;
            // 
            // tsParam3Txt
            // 
            this.tsParam3Txt.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsParam3Txt.Name = "tsParam3Txt";
            this.tsParam3Txt.Size = new System.Drawing.Size(30, 30);
            this.tsParam3Txt.Visible = false;
            // 
            // tsParam3Separator
            // 
            this.tsParam3Separator.Name = "tsParam3Separator";
            this.tsParam3Separator.Size = new System.Drawing.Size(6, 28);
            this.tsParam3Separator.Visible = false;
            // 
            // tsBtnTransformAll
            // 
            this.tsBtnTransformAll.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnTransformAll.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tsBtnTransformAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnTransformAll.Enabled = false;
            this.tsBtnTransformAll.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnTransformAll.Image")));
            this.tsBtnTransformAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnTransformAll.Margin = new System.Windows.Forms.Padding(0, 1, 15, 2);
            this.tsBtnTransformAll.Name = "tsBtnTransformAll";
            this.tsBtnTransformAll.Size = new System.Drawing.Size(101, 25);
            this.tsBtnTransformAll.Text = "Transform All";
            this.tsBtnTransformAll.ToolTipText = "Apply transformation to all equities bound to correlation table";
            this.tsBtnTransformAll.Click += new System.EventHandler(this.tsBtnTransformAll_Click);
            // 
            // dataTransformationThread
            // 
            this.dataTransformationThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.dataTransformationThread_DoWork);
            this.dataTransformationThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.dataTransformationThread_Completed);
            // 
            // chartMain
            // 
            this.chartMain.AllowZoom = true;
            this.chartMain.BackColor = System.Drawing.Color.Transparent;
            this.chartMain.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.VerticalCenter;
            this.chartMain.BackSecondaryColor = System.Drawing.Color.White;
            this.chartMain.BorderlineColor = System.Drawing.Color.Gainsboro;
            this.chartMain.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chartMain.CandleStickEnabled = false;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MinorGrid.Enabled = true;
            chartArea1.AxisX.MinorGrid.Interval = double.NaN;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MinorGrid.Enabled = true;
            chartArea1.AxisY.MinorGrid.Interval = double.NaN;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.Name = "ChartArea1";
            this.chartMain.ChartAreas.Add(chartArea1);
            this.chartMain.CurrentSeriesIndex = 0;
            this.chartMain.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.IsDockedInsideChartArea = false;
            legend1.Name = "chartMainLegend1";
            this.chartMain.Legends.Add(legend1);
            this.chartMain.Location = new System.Drawing.Point(0, 0);
            this.chartMain.Name = "chartMain";
            this.chartMain.Size = new System.Drawing.Size(876, 478);
            this.chartMain.TabIndex = 0;
            this.chartMain.Text = "chart1";
            this.chartMain.SelectedPointChanged += new System.EventHandler(this.chartMain_SelectedPointChanged);
            // 
            // tblVisualsTab1
            // 
            this.tblVisualsTab1.AutoScroll = true;
            this.tblVisualsTab1.AutoSize = true;
            this.tblVisualsTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblVisualsTab1.Location = new System.Drawing.Point(0, 0);
            this.tblVisualsTab1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tblVisualsTab1.Name = "tblVisualsTab1";
            this.tblVisualsTab1.Size = new System.Drawing.Size(514, 478);
            this.tblVisualsTab1.TabIndex = 0;
            this.tblVisualsTab1.TableType = MyMarketAnalyzer.StatTableType.INDIVIDUAL_PPC;
            this.tblVisualsTab1.Layout += new System.Windows.Forms.LayoutEventHandler(this.tblVisualsTab1_OnLayout);
            // 
            // visualSummaryTabPage1
            // 
            this.visualSummaryTabPage1.AutoSize = true;
            this.visualSummaryTabPage1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.visualSummaryTabPage1.BackColor = System.Drawing.Color.Transparent;
            this.visualSummaryTabPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visualSummaryTabPage1.Location = new System.Drawing.Point(3, 3);
            this.visualSummaryTabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.visualSummaryTabPage1.Name = "visualSummaryTabPage1";
            this.visualSummaryTabPage1.Size = new System.Drawing.Size(1384, 101);
            this.visualSummaryTabPage1.TabIndex = 0;
            this.visualSummaryTabPage1.Invalidated += new System.Windows.Forms.InvalidateEventHandler(this.vspage_Invalidated);
            // 
            // VisualsTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.vsTabToolStrip);
            this.Controls.Add(this.splitContainerTabVS);
            this.Controls.Add(this.tabVisualsData);
            this.Name = "VisualsTab";
            this.Size = new System.Drawing.Size(1400, 650);
            this.Load += new System.EventHandler(this.VisualsTab_Load);
            this.Resize += new System.EventHandler(this.VisualsTab_Resize);
            this.tabVisualsData.ResumeLayout(false);
            this.tabVisualData.ResumeLayout(false);
            this.tabVisualData.PerformLayout();
            this.splitContainerTabVS.Panel1.ResumeLayout(false);
            this.splitContainerTabVS.Panel2.ResumeLayout(false);
            this.splitContainerTabVS.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTabVS)).EndInit();
            this.splitContainerTabVS.ResumeLayout(false);
            this.vsTabToolStrip.ResumeLayout(false);
            this.vsTabToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomChart chartMain;
        private System.Windows.Forms.TabControl tabVisualsData;
        private System.Windows.Forms.TabPage tabVisualData;
        private System.Windows.Forms.SplitContainer splitContainerTabVS;
        private StatTable tblVisualsTab1;
        private VisualSummaryTabPage visualSummaryTabPage1;
        private System.Windows.Forms.ToolStrip vsTabToolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tsComboTransformation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsBtnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsBtnUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel tsParam1Lbl;
        private System.Windows.Forms.ToolStripTextBox tsParam1Txt;
        private System.Windows.Forms.ToolStripSeparator tsParam1Separator;
        private System.Windows.Forms.ToolStripLabel tsParam2Lbl;
        private System.Windows.Forms.ToolStripTextBox tsParam2Txt;
        private System.Windows.Forms.ToolStripSeparator tsParam2Separator;
        private System.Windows.Forms.ToolStripLabel tsParam3Lbl;
        private System.Windows.Forms.ToolStripTextBox tsParam3Txt;
        private System.Windows.Forms.ToolStripSeparator tsParam3Separator;
        private System.Windows.Forms.ToolStripButton tsBtnTransformAll;
        private System.ComponentModel.BackgroundWorker dataTransformationThread;


    }
}
