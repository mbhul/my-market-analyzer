namespace MyMarketAnalyzer
{
    partial class VisualSummaryTabPage
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAvgVolume = new System.Windows.Forms.Label();
            this.lblAvgPrice = new System.Windows.Forms.Label();
            this.lblAvgChange = new System.Windows.Forms.Label();
            this.btnShowCorrelation = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTiAdd3 = new System.Windows.Forms.Button();
            this.btnTiAdd2 = new System.Windows.Forms.Button();
            this.comboTi3 = new System.Windows.Forms.ComboBox();
            this.comboTi2 = new System.Windows.Forms.ComboBox();
            this.btnTiAdd1 = new System.Windows.Forms.Button();
            this.comboTi1 = new System.Windows.Forms.ComboBox();
            this.lblVariable3 = new System.Windows.Forms.Label();
            this.lblVariable2 = new System.Windows.Forms.Label();
            this.lblVariable1 = new System.Windows.Forms.Label();
            this.btnNewWind = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkCandlestick = new System.Windows.Forms.CheckBox();
            this.chkVolume = new System.Windows.Forms.CheckBox();
            this.chkHistData = new System.Windows.Forms.CheckBox();
            this.chkLiveData = new System.Windows.Forms.CheckBox();
            this.gbOverview = new System.Windows.Forms.GroupBox();
            this.lblVariable4 = new System.Windows.Forms.Label();
            this.lblVariable5 = new System.Windows.Forms.Label();
            this.lblVariable6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbOverview.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.lblAvgVolume);
            this.groupBox1.Controls.Add(this.lblAvgPrice);
            this.groupBox1.Controls.Add(this.lblAvgChange);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(4, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(192, 97);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Point";
            // 
            // lblAvgVolume
            // 
            this.lblAvgVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAvgVolume.AutoSize = true;
            this.lblAvgVolume.Location = new System.Drawing.Point(8, 73);
            this.lblAvgVolume.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvgVolume.Name = "lblAvgVolume";
            this.lblAvgVolume.Size = new System.Drawing.Size(87, 17);
            this.lblAvgVolume.TabIndex = 2;
            this.lblAvgVolume.Text = "Volume: xxxx";
            // 
            // lblAvgPrice
            // 
            this.lblAvgPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAvgPrice.AutoSize = true;
            this.lblAvgPrice.Location = new System.Drawing.Point(8, 23);
            this.lblAvgPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvgPrice.Name = "lblAvgPrice";
            this.lblAvgPrice.Size = new System.Drawing.Size(122, 17);
            this.lblAvgPrice.TabIndex = 0;
            this.lblAvgPrice.Text = "Closing Price: xxxx";
            // 
            // lblAvgChange
            // 
            this.lblAvgChange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAvgChange.AutoSize = true;
            this.lblAvgChange.Location = new System.Drawing.Point(8, 48);
            this.lblAvgChange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvgChange.Name = "lblAvgChange";
            this.lblAvgChange.Size = new System.Drawing.Size(140, 17);
            this.lblAvgChange.TabIndex = 1;
            this.lblAvgChange.Text = "Daily % Change: xxxx";
            // 
            // btnShowCorrelation
            // 
            this.btnShowCorrelation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowCorrelation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnShowCorrelation.Location = new System.Drawing.Point(21, 5);
            this.btnShowCorrelation.Margin = new System.Windows.Forms.Padding(4);
            this.btnShowCorrelation.Name = "btnShowCorrelation";
            this.btnShowCorrelation.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnShowCorrelation.Size = new System.Drawing.Size(187, 28);
            this.btnShowCorrelation.TabIndex = 4;
            this.btnShowCorrelation.Text = "Show Correlation Table";
            this.btnShowCorrelation.UseVisualStyleBackColor = true;
            this.btnShowCorrelation.Click += new System.EventHandler(this.btnShowCorrelation_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTiAdd3);
            this.groupBox2.Controls.Add(this.btnTiAdd2);
            this.groupBox2.Controls.Add(this.comboTi3);
            this.groupBox2.Controls.Add(this.comboTi2);
            this.groupBox2.Controls.Add(this.btnTiAdd1);
            this.groupBox2.Controls.Add(this.comboTi1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(854, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(284, 97);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // btnTiAdd3
            // 
            this.btnTiAdd3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTiAdd3.Enabled = false;
            this.btnTiAdd3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTiAdd3.Location = new System.Drawing.Point(4, 66);
            this.btnTiAdd3.Margin = new System.Windows.Forms.Padding(4);
            this.btnTiAdd3.Name = "btnTiAdd3";
            this.btnTiAdd3.Size = new System.Drawing.Size(80, 30);
            this.btnTiAdd3.TabIndex = 8;
            this.btnTiAdd3.Text = "Add";
            this.btnTiAdd3.UseVisualStyleBackColor = true;
            this.btnTiAdd3.Visible = false;
            this.btnTiAdd3.Click += new System.EventHandler(this.btnTiAdd3_Click);
            // 
            // btnTiAdd2
            // 
            this.btnTiAdd2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTiAdd2.Enabled = false;
            this.btnTiAdd2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTiAdd2.Location = new System.Drawing.Point(4, 36);
            this.btnTiAdd2.Margin = new System.Windows.Forms.Padding(4);
            this.btnTiAdd2.Name = "btnTiAdd2";
            this.btnTiAdd2.Size = new System.Drawing.Size(80, 30);
            this.btnTiAdd2.TabIndex = 7;
            this.btnTiAdd2.Text = "Add";
            this.btnTiAdd2.UseVisualStyleBackColor = true;
            this.btnTiAdd2.Visible = false;
            this.btnTiAdd2.Click += new System.EventHandler(this.btnTiAdd2_Click);
            // 
            // comboTi3
            // 
            this.comboTi3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboTi3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTi3.FormattingEnabled = true;
            this.comboTi3.Location = new System.Drawing.Point(87, 68);
            this.comboTi3.Margin = new System.Windows.Forms.Padding(4);
            this.comboTi3.Name = "comboTi3";
            this.comboTi3.Size = new System.Drawing.Size(191, 24);
            this.comboTi3.TabIndex = 6;
            this.comboTi3.Visible = false;
            this.comboTi3.SelectedIndexChanged += new System.EventHandler(this.comboTi3_SelectedIndexChanged);
            // 
            // comboTi2
            // 
            this.comboTi2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboTi2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTi2.FormattingEnabled = true;
            this.comboTi2.Location = new System.Drawing.Point(87, 39);
            this.comboTi2.Margin = new System.Windows.Forms.Padding(4);
            this.comboTi2.Name = "comboTi2";
            this.comboTi2.Size = new System.Drawing.Size(191, 24);
            this.comboTi2.TabIndex = 5;
            this.comboTi2.Visible = false;
            this.comboTi2.SelectedIndexChanged += new System.EventHandler(this.comboTi2_SelectedIndexChanged);
            // 
            // btnTiAdd1
            // 
            this.btnTiAdd1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTiAdd1.Enabled = false;
            this.btnTiAdd1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTiAdd1.Location = new System.Drawing.Point(4, 7);
            this.btnTiAdd1.Margin = new System.Windows.Forms.Padding(4);
            this.btnTiAdd1.Name = "btnTiAdd1";
            this.btnTiAdd1.Size = new System.Drawing.Size(80, 30);
            this.btnTiAdd1.TabIndex = 4;
            this.btnTiAdd1.Text = "Add";
            this.btnTiAdd1.UseVisualStyleBackColor = true;
            this.btnTiAdd1.Click += new System.EventHandler(this.btnTiAdd1_Click);
            // 
            // comboTi1
            // 
            this.comboTi1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboTi1.BackColor = System.Drawing.SystemColors.Window;
            this.comboTi1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTi1.FormattingEnabled = true;
            this.comboTi1.Location = new System.Drawing.Point(87, 10);
            this.comboTi1.Margin = new System.Windows.Forms.Padding(4);
            this.comboTi1.Name = "comboTi1";
            this.comboTi1.Size = new System.Drawing.Size(191, 24);
            this.comboTi1.TabIndex = 3;
            this.comboTi1.SelectedIndexChanged += new System.EventHandler(this.comboTi1_SelectedIndexChanged);
            // 
            // lblVariable3
            // 
            this.lblVariable3.AutoSize = true;
            this.lblVariable3.Location = new System.Drawing.Point(8, 73);
            this.lblVariable3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVariable3.Name = "lblVariable3";
            this.lblVariable3.Size = new System.Drawing.Size(84, 17);
            this.lblVariable3.TabIndex = 2;
            this.lblVariable3.Text = "Variable 3...";
            this.lblVariable3.Visible = false;
            // 
            // lblVariable2
            // 
            this.lblVariable2.AutoSize = true;
            this.lblVariable2.Location = new System.Drawing.Point(8, 48);
            this.lblVariable2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVariable2.Name = "lblVariable2";
            this.lblVariable2.Size = new System.Drawing.Size(84, 17);
            this.lblVariable2.TabIndex = 1;
            this.lblVariable2.Text = "Variable 2...";
            this.lblVariable2.Visible = false;
            // 
            // lblVariable1
            // 
            this.lblVariable1.AutoSize = true;
            this.lblVariable1.Location = new System.Drawing.Point(8, 23);
            this.lblVariable1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVariable1.Name = "lblVariable1";
            this.lblVariable1.Size = new System.Drawing.Size(84, 17);
            this.lblVariable1.TabIndex = 0;
            this.lblVariable1.Text = "Variable 1...";
            this.lblVariable1.Visible = false;
            // 
            // btnNewWind
            // 
            this.btnNewWind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewWind.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNewWind.Location = new System.Drawing.Point(21, 37);
            this.btnNewWind.Margin = new System.Windows.Forms.Padding(4);
            this.btnNewWind.Name = "btnNewWind";
            this.btnNewWind.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnNewWind.Size = new System.Drawing.Size(187, 30);
            this.btnNewWind.TabIndex = 6;
            this.btnNewWind.Text = "Open In New Window";
            this.btnNewWind.UseVisualStyleBackColor = true;
            this.btnNewWind.Click += new System.EventHandler(this.btnNewWind_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.chkCandlestick);
            this.groupBox3.Controls.Add(this.chkVolume);
            this.groupBox3.Controls.Add(this.chkHistData);
            this.groupBox3.Controls.Add(this.chkLiveData);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(554, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(292, 97);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // chkCandlestick
            // 
            this.chkCandlestick.AutoSize = true;
            this.chkCandlestick.Location = new System.Drawing.Point(140, 28);
            this.chkCandlestick.Margin = new System.Windows.Forms.Padding(4);
            this.chkCandlestick.Name = "chkCandlestick";
            this.chkCandlestick.Size = new System.Drawing.Size(102, 21);
            this.chkCandlestick.TabIndex = 5;
            this.chkCandlestick.Text = "Candlestick";
            this.chkCandlestick.UseVisualStyleBackColor = true;
            this.chkCandlestick.CheckedChanged += new System.EventHandler(this.ChkCandlestick_CheckedChanged);
            // 
            // chkVolume
            // 
            this.chkVolume.AutoSize = true;
            this.chkVolume.Enabled = false;
            this.chkVolume.Location = new System.Drawing.Point(140, 58);
            this.chkVolume.Margin = new System.Windows.Forms.Padding(4);
            this.chkVolume.Name = "chkVolume";
            this.chkVolume.Size = new System.Drawing.Size(115, 21);
            this.chkVolume.TabIndex = 4;
            this.chkVolume.Text = "Show Volume";
            this.chkVolume.UseVisualStyleBackColor = true;
            // 
            // chkHistData
            // 
            this.chkHistData.Enabled = false;
            this.chkHistData.Location = new System.Drawing.Point(8, 58);
            this.chkHistData.Margin = new System.Windows.Forms.Padding(4);
            this.chkHistData.Name = "chkHistData";
            this.chkHistData.Size = new System.Drawing.Size(122, 21);
            this.chkHistData.TabIndex = 3;
            this.chkHistData.Text = "Historical Data";
            this.chkHistData.UseVisualStyleBackColor = true;
            this.chkHistData.CheckedChanged += new System.EventHandler(this.chkHistData_CheckedChanged);
            // 
            // chkLiveData
            // 
            this.chkLiveData.Enabled = false;
            this.chkLiveData.Location = new System.Drawing.Point(8, 30);
            this.chkLiveData.Margin = new System.Windows.Forms.Padding(4);
            this.chkLiveData.Name = "chkLiveData";
            this.chkLiveData.Size = new System.Drawing.Size(90, 21);
            this.chkLiveData.TabIndex = 2;
            this.chkLiveData.Text = "Live Data";
            this.chkLiveData.UseVisualStyleBackColor = true;
            this.chkLiveData.CheckedChanged += new System.EventHandler(this.chkLiveData_CheckedChanged);
            // 
            // gbOverview
            // 
            this.gbOverview.AutoSize = true;
            this.gbOverview.Controls.Add(this.lblVariable4);
            this.gbOverview.Controls.Add(this.lblVariable5);
            this.gbOverview.Controls.Add(this.lblVariable6);
            this.gbOverview.Controls.Add(this.lblVariable1);
            this.gbOverview.Controls.Add(this.lblVariable2);
            this.gbOverview.Controls.Add(this.lblVariable3);
            this.gbOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOverview.Location = new System.Drawing.Point(204, 0);
            this.gbOverview.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.gbOverview.Name = "gbOverview";
            this.gbOverview.Padding = new System.Windows.Forms.Padding(4);
            this.gbOverview.Size = new System.Drawing.Size(342, 97);
            this.gbOverview.TabIndex = 8;
            this.gbOverview.TabStop = false;
            this.gbOverview.Text = "Overview";
            // 
            // lblVariable4
            // 
            this.lblVariable4.AutoSize = true;
            this.lblVariable4.Location = new System.Drawing.Point(205, 22);
            this.lblVariable4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVariable4.Name = "lblVariable4";
            this.lblVariable4.Size = new System.Drawing.Size(84, 17);
            this.lblVariable4.TabIndex = 3;
            this.lblVariable4.Text = "Variable 1...";
            this.lblVariable4.Visible = false;
            // 
            // lblVariable5
            // 
            this.lblVariable5.AutoSize = true;
            this.lblVariable5.Location = new System.Drawing.Point(205, 47);
            this.lblVariable5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVariable5.Name = "lblVariable5";
            this.lblVariable5.Size = new System.Drawing.Size(84, 17);
            this.lblVariable5.TabIndex = 4;
            this.lblVariable5.Text = "Variable 2...";
            this.lblVariable5.Visible = false;
            // 
            // lblVariable6
            // 
            this.lblVariable6.AutoSize = true;
            this.lblVariable6.Location = new System.Drawing.Point(205, 71);
            this.lblVariable6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVariable6.Name = "lblVariable6";
            this.lblVariable6.Size = new System.Drawing.Size(84, 17);
            this.lblVariable6.TabIndex = 5;
            this.lblVariable6.Text = "Variable 3...";
            this.lblVariable6.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 292F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbOverview, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1360, 101);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnShowCorrelation);
            this.panel1.Controls.Add(this.btnNewWind);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1145, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(212, 95);
            this.panel1.TabIndex = 9;
            // 
            // VisualSummaryTabPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "VisualSummaryTabPage";
            this.Size = new System.Drawing.Size(1360, 101);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.visualsummarypg_Layout);
            this.Validated += new System.EventHandler(this.vssummaryPg_Validated);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbOverview.ResumeLayout(false);
            this.gbOverview.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblAvgVolume;
        private System.Windows.Forms.Label lblAvgPrice;
        private System.Windows.Forms.Label lblAvgChange;
        private System.Windows.Forms.Button btnShowCorrelation;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblVariable1;
        private System.Windows.Forms.Label lblVariable2;
        private System.Windows.Forms.Label lblVariable3;
        private System.Windows.Forms.ComboBox comboTi1;
        private System.Windows.Forms.ComboBox comboTi3;
        private System.Windows.Forms.ComboBox comboTi2;
        private System.Windows.Forms.Button btnTiAdd1;
        private System.Windows.Forms.Button btnTiAdd3;
        private System.Windows.Forms.Button btnTiAdd2;
        private System.Windows.Forms.Button btnNewWind;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkLiveData;
        private System.Windows.Forms.CheckBox chkHistData;
        private System.Windows.Forms.CheckBox chkVolume;
        private System.Windows.Forms.GroupBox gbOverview;
        private System.Windows.Forms.Label lblVariable4;
        private System.Windows.Forms.Label lblVariable5;
        private System.Windows.Forms.Label lblVariable6;
        private System.Windows.Forms.CheckBox chkCandlestick;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
    }
}
