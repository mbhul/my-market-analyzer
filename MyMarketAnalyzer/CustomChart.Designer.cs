namespace MyMarketAnalyzer
{
    partial class CustomChart
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
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // CustomChart
            // 
            this.PostPaint += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ChartPaintEventArgs>(this.customchart_PostPaint);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.customchart_OnPaint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.custom_OnMouseDown);
            this.MouseEnter += new System.EventHandler(this.custom_OnMouseEnter);
            this.MouseLeave += new System.EventHandler(this.custom_OnMouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.custom_OnMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.custom_OnMouseUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.custom_OnMouseWheel);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
