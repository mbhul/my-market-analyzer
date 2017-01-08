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
            this.tiVariable3 = new System.Windows.Forms.Label();
            this.tiVariable2 = new System.Windows.Forms.Label();
            this.tiVariable1 = new System.Windows.Forms.Label();
            this.btnNewWind = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkLiveData = new System.Windows.Forms.CheckBox();
            this.chkHistData = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.btnShowCorrelation.Location = new System.Drawing.Point(811, 3);
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
            this.groupBox2.Controls.Add(this.tiVariable3);
            this.groupBox2.Controls.Add(this.tiVariable2);
            this.groupBox2.Controls.Add(this.tiVariable1);
            this.groupBox2.Location = new System.Drawing.Point(462, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(343, 82);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Technical Indicators";
            // 
            // btnTiAdd3
            // 
            this.btnTiAdd3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTiAdd3.Enabled = false;
            this.btnTiAdd3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTiAdd3.Location = new System.Drawing.Point(133, 56);
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
            this.btnTiAdd2.Location = new System.Drawing.Point(133, 32);
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
            this.comboTi3.Location = new System.Drawing.Point(195, 58);
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
            this.comboTi2.Location = new System.Drawing.Point(195, 34);
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
            this.btnTiAdd1.Location = new System.Drawing.Point(133, 8);
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
            this.comboTi1.Location = new System.Drawing.Point(195, 10);
            this.comboTi1.Name = "comboTi1";
            this.comboTi1.Size = new System.Drawing.Size(144, 21);
            this.comboTi1.TabIndex = 3;
            this.comboTi1.SelectedIndexChanged += new System.EventHandler(this.comboTi1_SelectedIndexChanged);
            // 
            // tiVariable3
            // 
            this.tiVariable3.AutoSize = true;
            this.tiVariable3.Location = new System.Drawing.Point(6, 59);
            this.tiVariable3.Name = "tiVariable3";
            this.tiVariable3.Size = new System.Drawing.Size(63, 13);
            this.tiVariable3.TabIndex = 2;
            this.tiVariable3.Text = "Variable 3...";
            this.tiVariable3.Visible = false;
            // 
            // tiVariable2
            // 
            this.tiVariable2.AutoSize = true;
            this.tiVariable2.Location = new System.Drawing.Point(6, 39);
            this.tiVariable2.Name = "tiVariable2";
            this.tiVariable2.Size = new System.Drawing.Size(63, 13);
            this.tiVariable2.TabIndex = 1;
            this.tiVariable2.Text = "Variable 2...";
            this.tiVariable2.Visible = false;
            // 
            // tiVariable1
            // 
            this.tiVariable1.AutoSize = true;
            this.tiVariable1.Location = new System.Drawing.Point(6, 19);
            this.tiVariable1.Name = "tiVariable1";
            this.tiVariable1.Size = new System.Drawing.Size(63, 13);
            this.tiVariable1.TabIndex = 0;
            this.tiVariable1.Text = "Variable 1...";
            this.tiVariable1.Visible = false;
            // 
            // btnNewWind
            // 
            this.btnNewWind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewWind.Location = new System.Drawing.Point(811, 28);
            this.btnNewWind.Name = "btnNewWind";
            this.btnNewWind.Size = new System.Drawing.Size(239, 24);
            this.btnNewWind.TabIndex = 6;
            this.btnNewWind.Text = "Open In New Window";
            this.btnNewWind.UseVisualStyleBackColor = true;
            this.btnNewWind.Click += new System.EventHandler(this.btnNewWind_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.chkHistData);
            this.groupBox3.Controls.Add(this.chkLiveData);
            this.groupBox3.Location = new System.Drawing.Point(156, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(300, 82);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // chkLiveData
            // 
            this.chkLiveData.AutoSize = true;
            this.chkLiveData.Enabled = false;
            this.chkLiveData.Location = new System.Drawing.Point(6, 18);
            this.chkLiveData.Name = "chkLiveData";
            this.chkLiveData.Size = new System.Drawing.Size(109, 17);
            this.chkLiveData.TabIndex = 2;
            this.chkLiveData.Text = "Display Live Data";
            this.chkLiveData.UseVisualStyleBackColor = true;
            this.chkLiveData.CheckedChanged += new System.EventHandler(this.chkLiveData_CheckedChanged);
            // 
            // chkHistData
            // 
            this.chkHistData.AutoSize = true;
            this.chkHistData.Enabled = false;
            this.chkHistData.Location = new System.Drawing.Point(6, 38);
            this.chkHistData.Name = "chkHistData";
            this.chkHistData.Size = new System.Drawing.Size(132, 17);
            this.chkHistData.TabIndex = 3;
            this.chkHistData.Text = "Display Historical Data";
            this.chkHistData.UseVisualStyleBackColor = true;
            this.chkHistData.CheckedChanged += new System.EventHandler(this.chkHistData_CheckedChanged);
            // 
            // VisualSummaryTabPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnNewWind);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnShowCorrelation);
            this.Controls.Add(this.groupBox1);
            this.Name = "VisualSummaryTabPage";
            this.Size = new System.Drawing.Size(1053, 82);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.visualsummarypg_Layout);
            this.Validated += new System.EventHandler(this.vssummaryPg_Validated);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblAvgVolume;
        private System.Windows.Forms.Label lblAvgPrice;
        private System.Windows.Forms.Label lblAvgChange;
        private System.Windows.Forms.Button btnShowCorrelation;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label tiVariable1;
        private System.Windows.Forms.Label tiVariable2;
        private System.Windows.Forms.Label tiVariable3;
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
    }
}
