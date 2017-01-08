namespace MyMarketAnalyzer
{
    partial class WatchlistItem
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
            this.components = new System.ComponentModel.Container();
            this.RowPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblChange = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMarket = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblRemove = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.RowPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblRemove)).BeginInit();
            this.SuspendLayout();
            // 
            // RowPanel
            // 
            this.RowPanel.BackColor = System.Drawing.Color.Transparent;
            this.RowPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RowPanel.Controls.Add(this.tableLayoutPanel1);
            this.RowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RowPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RowPanel.Location = new System.Drawing.Point(0, 0);
            this.RowPanel.Name = "RowPanel";
            this.RowPanel.Size = new System.Drawing.Size(503, 62);
            this.RowPanel.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPrice, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.27536F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.72464F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(501, 60);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.lblChange);
            this.flowLayoutPanel2.Controls.Add(this.pictureBox2);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(378, 32);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(120, 25);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // lblChange
            // 
            this.lblChange.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblChange.AutoSize = true;
            this.lblChange.Location = new System.Drawing.Point(46, 5);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(71, 13);
            this.lblChange.TabIndex = 2;
            this.lblChange.Text = "Price Change";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(15, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(25, 17);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // lblPrice
            // 
            this.lblPrice.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.Location = new System.Drawing.Point(407, 2);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(91, 24);
            this.lblPrice.TabIndex = 1;
            this.lblPrice.Text = "Last Price";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
            this.flowLayoutPanel1.Controls.Add(this.lblDate);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.lblMarket);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 32);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(369, 25);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MyMarketAnalyzer.Properties.Resources.clock2;
            this.pictureBox1.Location = new System.Drawing.Point(3, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 22);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblDate
            // 
            this.lblDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(28, 7);
            this.lblDate.Margin = new System.Windows.Forms.Padding(0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(30, 13);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Date";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(9, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "|";
            // 
            // lblMarket
            // 
            this.lblMarket.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMarket.AutoSize = true;
            this.lblMarket.Location = new System.Drawing.Point(73, 7);
            this.lblMarket.Margin = new System.Windows.Forms.Padding(0);
            this.lblMarket.Name = "lblMarket";
            this.lblMarket.Size = new System.Drawing.Size(40, 13);
            this.lblMarket.TabIndex = 4;
            this.lblMarket.Text = "Market";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel3.Controls.Add(this.lblRemove);
            this.flowLayoutPanel3.Controls.Add(this.lblName);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(375, 29);
            this.flowLayoutPanel3.TabIndex = 5;
            // 
            // lblRemove
            // 
            this.lblRemove.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.lblRemove.Image = global::MyMarketAnalyzer.Properties.Resources.x_icon_normal;
            this.lblRemove.InitialImage = global::MyMarketAnalyzer.Properties.Resources.x_icon_normal;
            this.lblRemove.Location = new System.Drawing.Point(3, 3);
            this.lblRemove.Name = "lblRemove";
            this.lblRemove.Size = new System.Drawing.Size(27, 20);
            this.lblRemove.TabIndex = 2;
            this.lblRemove.TabStop = false;
            this.lblRemove.Click += new System.EventHandler(this.lblRemove_Click);
            this.lblRemove.MouseEnter += new System.EventHandler(this.lblRemove_MouseEnter);
            this.lblRemove.MouseLeave += new System.EventHandler(this.lblRemove_MouseLeave);
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(33, 1);
            this.lblName.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(118, 24);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Equity Name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WatchlistItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.RowPanel);
            this.Name = "WatchlistItem";
            this.Size = new System.Drawing.Size(503, 62);
            this.RowPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblRemove)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel RowPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMarket;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox lblRemove;

    }
}
