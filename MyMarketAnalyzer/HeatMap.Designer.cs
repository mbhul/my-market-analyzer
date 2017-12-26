namespace MyMarketAnalyzer
{
    partial class HeatMap
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
            this.heatMapPicBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.heatMapPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // heatMapPicBox
            // 
            this.heatMapPicBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.heatMapPicBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.heatMapPicBox.Location = new System.Drawing.Point(0, 0);
            this.heatMapPicBox.Name = "heatMapPicBox";
            this.heatMapPicBox.Size = new System.Drawing.Size(935, 559);
            this.heatMapPicBox.TabIndex = 0;
            this.heatMapPicBox.TabStop = false;
            // 
            // HeatMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.heatMapPicBox);
            this.Name = "HeatMap";
            this.Size = new System.Drawing.Size(935, 559);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.heatMap_OnPaint);
            this.Resize += new System.EventHandler(this.heatMap_OnResize);
            ((System.ComponentModel.ISupportInitialize)(this.heatMapPicBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox heatMapPicBox;
    }
}
