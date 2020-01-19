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
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbOverview.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAvgVolume);
            this.groupBox1.Controls.Add(this.lblAvgPrice);
            this.groupBox1.Controls.Add(this.lblAvgChange);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 82);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Point";
            // 
            // lblAvgVolume
            // 
            this.lblAvgVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAvgVolume.AutoSize = true;
            this.lblAvgVolume.Location = new System.Drawing.Point(6, 59);
            this.lblAvgVolume.Name = "lblAvgVolume";
            this.lblAvgVolume.Size = new System.Drawing.Size(68, 13);
            this.lblAvgVolume.TabIndex = 2;
            this.lblAvgVolume.Text = "Volume: xxxx";
            // 
            // lblAvgPrice
            // 
            this.lblAvgPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAvgPrice.AutoSize = true;
            this.lblAvgPrice.Location = new System.Drawing.Point(6, 19);
            this.lblAvgPrice.Name = "lblAvgPrice";
            this.lblAvgPrice.Size = new System.Drawing.Size(94, 13);
            this.lblAvgPrice.TabIndex = 0;
            this.lblAvgPrice.Text = "Closing Price: xxxx";
            // 
            // lblAvgChange
            // 
            this.lblAvgChange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAvgChange.AutoSize = true;
            this.lblAvgChange.Location = new System.Drawing.Point(6, 39);
            this.lblAvgChange.Name = "lblAvgChange";
            this.lblAvgChange.Size = new System.Drawing.Size(107, 13);
            this.lblAvgChange.TabIndex = 1;
            this.lblAvgChange.Text = "Daily % Change: xxxx";
            // 
            // btnShowCorrelation
            // 
            this.btnShowCorrelation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowCorrelation.Location = new System.Drawing.Point(904, 3);
            this.btnShowCorrelation.Name = "btnShowCorrelation";
            this.btnShowCorrelation.Size = new System.Drawing.Size(239, 23);
            this.btnShowCorrelation.TabIndex = 4;
            this.btnShowCorrelation.Text = "Show Correlation Table";
            this.btnShowCorrelation.UseVisualStyleBackColor = true;
            this.btnShowCorrelation.Click += new System.EventHandler(this.btnShowCorrelation_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnTiAdd3);
            this.groupBox2.Controls.Add(this.btnTiAdd2);
            this.groupBox2.Controls.Add(this.comboTi3);
            this.groupBox2.Controls.Add(this.comboTi2);
            this.groupBox2.Controls.Add(this.btnTiAdd1);
            this.groupBox2.Controls.Add(this.comboTi1);
            this.groupBox2.Location = new System.Drawing.Point(685, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(213, 82);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // btnTiAdd3
            // 
            this.btnTiAdd3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTiAdd3.Enabled = false;
            this.btnTiAdd3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTiAdd3.Location = new System.Drawing.Point(3, 56);
            this.btnTiAdd3.Name = "btnTiAdd3";
            this.btnTiAdd3.Size = new System.Drawing.Size(60, 24);
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
            this.btnTiAdd2.Location = new System.Drawing.Point(3, 32);
            this.btnTiAdd2.Name = "btnTiAdd2";
            this.btnTiAdd2.Size = new System.Drawing.Size(60, 24);
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
            this.comboTi3.Location = new System.Drawing.Point(65, 58);
            this.comboTi3.Name = "comboTi3";
            this.comboTi3.Size = new System.Drawing.Size(144, 21);
            this.comboTi3.TabIndex = 6;
            this.comboTi3.Visible = false;
            this.comboTi3.SelectedIndexChanged += new System.EventHandler(this.comboTi3_SelectedIndexChanged);
            // 
            // comboTi2
            // 
            this.comboTi2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboTi2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTi2.FormattingEnabled = true;
            this.comboTi2.Location = new System.Drawing.Point(65, 34);
            this.comboTi2.Name = "comboTi2";
            this.comboTi2.Size = new System.Drawing.Size(144, 21);
            this.comboTi2.TabIndex = 5;
            this.comboTi2.Visible = false;
            this.comboTi2.SelectedIndexChanged += new System.EventHandler(this.comboTi2_SelectedIndexChanged);
            // 
            // btnTiAdd1
            // 
            this.btnTiAdd1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTiAdd1.Enabled = false;
            this.btnTiAdd1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTiAdd1.Location = new System.Drawing.Point(3, 8);
            this.btnTiAdd1.Name = "btnTiAdd1";
            this.btnTiAdd1.Size = new System.Drawing.Size(60, 24);
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
            this.comboTi1.Location = new System.Drawing.Point(65, 10);
            this.comboTi1.Name = "comboTi1";
            this.comboTi1.Size = new System.Drawing.Size(144, 21);
            this.comboTi1.TabIndex = 3;
            this.comboTi1.SelectedIndexChanged += new System.EventHandler(this.comboTi1_SelectedIndexChanged);
            // 
            // lblVariable3
            // 
            this.lblVariable3.AutoSize = true;
            this.lblVariable3.Location = new System.Drawing.Point(6, 59);
            this.lblVariable3.Name = "lblVariable3";
            this.lblVariable3.Size = new System.Drawing.Size(63, 13);
            this.lblVariable3.TabIndex = 2;
            this.lblVariable3.Text = "Variable 3...";
            this.lblVariable3.Visible = false;
            // 
            // lblVariable2
            // 
            this.lblVariable2.AutoSize = true;
            this.lblVariable2.Location = new System.Drawing.Point(6, 39);
            this.lblVariable2.Name = "lblVariable2";
            this.lblVariable2.Size = new System.Drawing.Size(63, 13);
            this.lblVariable2.TabIndex = 1;
            this.lblVariable2.Text = "Variable 2...";
            this.lblVariable2.Visible = false;
            // 
            // lblVariable1
            // 
            this.lblVariable1.AutoSize = true;
            this.lblVariable1.Location = new System.Drawing.Point(6, 19);
            this.lblVariable1.Name = "lblVariable1";
            this.lblVariable1.Size = new System.Drawing.Size(63, 13);
            this.lblVariable1.TabIndex = 0;
            this.lblVariable1.Text = "Variable 1...";
            this.lblVariable1.Visible = false;
            // 
            // btnNewWind
            // 
            this.btnNewWind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewWind.Location = new System.Drawing.Point(904, 28);
            this.btnNewWind.Name = "btnNewWind";
            this.btnNewWind.Size = new System.Drawing.Size(239, 24);
            this.btnNewWind.TabIndex = 6;
            this.btnNewWind.Text = "Open In New Window";
            this.btnNewWind.UseVisualStyleBackColor = true;
            this.btnNewWind.Click += new System.EventHandler(this.btnNewWind_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.chkCandlestick);
            this.groupBox3.Controls.Add(this.chkVolume);
            this.groupBox3.Controls.Add(this.chkHistData);
            this.groupBox3.Controls.Add(this.chkLiveData);
            this.groupBox3.Location = new System.Drawing.Point(474, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(205, 82);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // chkCandlestick
            // 
            this.chkCandlestick.AutoSize = true;
            this.chkCandlestick.Location = new System.Drawing.Point(105, 23);
            this.chkCandlestick.Name = "chkCandlestick";
            this.chkCandlestick.Size = new System.Drawing.Size(81, 17);
            this.chkCandlestick.TabIndex = 5;
            this.chkCandlestick.Text = "Candlestick";
            this.chkCandlestick.UseVisualStyleBackColor = true;
            this.chkCandlestick.CheckedChanged += new System.EventHandler(this.ChkCandlestick_CheckedChanged);
            // 
            // chkVolume
            // 
            this.chkVolume.AutoSize = true;
            this.chkVolume.Enabled = false;
            this.chkVolume.Location = new System.Drawing.Point(105, 47);
            this.chkVolume.Name = "chkVolume";
            this.chkVolume.Size = new System.Drawing.Size(91, 17);
            this.chkVolume.TabIndex = 4;
            this.chkVolume.Text = "Show Volume";
            this.chkVolume.UseVisualStyleBackColor = true;
            // 
            // chkHistData
            // 
            this.chkHistData.AutoSize = true;
            this.chkHistData.Enabled = false;
            this.chkHistData.Location = new System.Drawing.Point(6, 47);
            this.chkHistData.Name = "chkHistData";
            this.chkHistData.Size = new System.Drawing.Size(95, 17);
            this.chkHistData.TabIndex = 3;
            this.chkHistData.Text = "Historical Data";
            this.chkHistData.UseVisualStyleBackColor = true;
            this.chkHistData.CheckedChanged += new System.EventHandler(this.chkHistData_CheckedChanged);
            // 
            // chkLiveData
            // 
            this.chkLiveData.AutoSize = true;
            this.chkLiveData.Enabled = false;
            this.chkLiveData.Location = new System.Drawing.Point(6, 24);
            this.chkLiveData.Name = "chkLiveData";
            this.chkLiveData.Size = new System.Drawing.Size(72, 17);
            this.chkLiveData.TabIndex = 2;
            this.chkLiveData.Text = "Live Data";
            this.chkLiveData.UseVisualStyleBackColor = true;
            this.chkLiveData.CheckedChanged += new System.EventHandler(this.chkLiveData_CheckedChanged);
            // 
            // gbOverview
            // 
            this.gbOverview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOverview.Controls.Add(this.lblVariable4);
            this.gbOverview.Controls.Add(this.lblVariable5);
            this.gbOverview.Controls.Add(this.lblVariable6);
            this.gbOverview.Controls.Add(this.lblVariable1);
            this.gbOverview.Controls.Add(this.lblVariable2);
            this.gbOverview.Controls.Add(this.lblVariable3);
            this.gbOverview.Location = new System.Drawing.Point(156, 0);
            this.gbOverview.Name = "gbOverview";
            this.gbOverview.Size = new System.Drawing.Size(312, 82);
            this.gbOverview.TabIndex = 8;
            this.gbOverview.TabStop = false;
            this.gbOverview.Text = "Overview";
            // 
            // lblVariable4
            // 
            this.lblVariable4.AutoSize = true;
            this.lblVariable4.Location = new System.Drawing.Point(154, 18);
            this.lblVariable4.Name = "lblVariable4";
            this.lblVariable4.Size = new System.Drawing.Size(63, 13);
            this.lblVariable4.TabIndex = 3;
            this.lblVariable4.Text = "Variable 1...";
            this.lblVariable4.Visible = false;
            // 
            // lblVariable5
            // 
            this.lblVariable5.AutoSize = true;
            this.lblVariable5.Location = new System.Drawing.Point(154, 38);
            this.lblVariable5.Name = "lblVariable5";
            this.lblVariable5.Size = new System.Drawing.Size(63, 13);
            this.lblVariable5.TabIndex = 4;
            this.lblVariable5.Text = "Variable 2...";
            this.lblVariable5.Visible = false;
            // 
            // lblVariable6
            // 
            this.lblVariable6.AutoSize = true;
            this.lblVariable6.Location = new System.Drawing.Point(154, 58);
            this.lblVariable6.Name = "lblVariable6";
            this.lblVariable6.Size = new System.Drawing.Size(63, 13);
            this.lblVariable6.TabIndex = 5;
            this.lblVariable6.Text = "Variable 3...";
            this.lblVariable6.Visible = false;
            // 
            // VisualSummaryTabPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.gbOverview);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnNewWind);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnShowCorrelation);
            this.Controls.Add(this.groupBox1);
            this.Name = "VisualSummaryTabPage";
            this.Size = new System.Drawing.Size(1146, 82);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.visualsummarypg_Layout);
            this.Validated += new System.EventHandler(this.vssummaryPg_Validated);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbOverview.ResumeLayout(false);
            this.gbOverview.PerformLayout();
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
    }
}
