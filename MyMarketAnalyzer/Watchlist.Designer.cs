namespace MyMarketAnalyzer
{
    partial class Watchlist
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
            this.SuspendLayout();
            // 
            // Watchlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Name = "Watchlist";
            this.Size = new System.Drawing.Size(538, 273);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.watchlist_OnPaint);
            this.MouseLeave += new System.EventHandler(this.watchlist_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.watchlist_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion





    }
}
