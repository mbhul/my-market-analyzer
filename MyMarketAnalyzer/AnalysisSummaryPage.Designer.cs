namespace MyMarketAnalyzer
{
    partial class AnalysisSummaryPage
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.analysisResultsTable = new System.Windows.Forms.TableLayoutPanel();
            this.lblAnalysisPM = new System.Windows.Forms.Label();
            this.lblTopLeft = new System.Windows.Forms.Label();
            this.lblAnalysisDates = new System.Windows.Forms.Label();
            this.linkDetails = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.analysisResultsTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(517, 230);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.progressBar1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.analysisResultsTable, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(517, 230);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(505, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Analysis Results";
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(6, 210);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(505, 14);
            this.progressBar1.TabIndex = 3;
            // 
            // analysisResultsTable
            // 
            this.analysisResultsTable.AutoSize = true;
            this.analysisResultsTable.ColumnCount = 4;
            this.analysisResultsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.analysisResultsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.analysisResultsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.analysisResultsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.analysisResultsTable.Controls.Add(this.lblAnalysisPM, 1, 0);
            this.analysisResultsTable.Controls.Add(this.lblTopLeft, 0, 0);
            this.analysisResultsTable.Controls.Add(this.lblAnalysisDates, 2, 0);
            this.analysisResultsTable.Controls.Add(this.linkDetails, 3, 0);
            this.analysisResultsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analysisResultsTable.Location = new System.Drawing.Point(6, 33);
            this.analysisResultsTable.Name = "analysisResultsTable";
            this.analysisResultsTable.RowCount = 4;
            this.analysisResultsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.analysisResultsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.analysisResultsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.analysisResultsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.analysisResultsTable.Size = new System.Drawing.Size(505, 168);
            this.analysisResultsTable.TabIndex = 4;
            // 
            // lblAnalysisPM
            // 
            this.lblAnalysisPM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAnalysisPM.AutoSize = true;
            this.lblAnalysisPM.Font = new System.Drawing.Font("Candara", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnalysisPM.Location = new System.Drawing.Point(117, 0);
            this.lblAnalysisPM.Name = "lblAnalysisPM";
            this.lblAnalysisPM.Size = new System.Drawing.Size(78, 23);
            this.lblAnalysisPM.TabIndex = 4;
            this.lblAnalysisPM.Text = "+/- XXXX";
            // 
            // lblTopLeft
            // 
            this.lblTopLeft.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTopLeft.AutoSize = true;
            this.lblTopLeft.Font = new System.Drawing.Font("Candara", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTopLeft.Location = new System.Drawing.Point(3, 0);
            this.lblTopLeft.Name = "lblTopLeft";
            this.lblTopLeft.Size = new System.Drawing.Size(108, 23);
            this.lblTopLeft.TabIndex = 3;
            this.lblTopLeft.Text = "Net Change:";
            // 
            // lblAnalysisDates
            // 
            this.lblAnalysisDates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAnalysisDates.AutoSize = true;
            this.lblAnalysisDates.Font = new System.Drawing.Font("Candara", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnalysisDates.Location = new System.Drawing.Point(201, 4);
            this.lblAnalysisDates.Name = "lblAnalysisDates";
            this.lblAnalysisDates.Size = new System.Drawing.Size(65, 15);
            this.lblAnalysisDates.TabIndex = 7;
            this.lblAnalysisDates.Text = "(from - to)";
            // 
            // linkDetails
            // 
            this.linkDetails.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.linkDetails.AutoSize = true;
            this.linkDetails.Location = new System.Drawing.Point(430, 5);
            this.linkDetails.Name = "linkDetails";
            this.linkDetails.Size = new System.Drawing.Size(72, 13);
            this.linkDetails.TabIndex = 8;
            this.linkDetails.TabStop = true;
            this.linkDetails.Text = "View Detailed";
            this.linkDetails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // AnalysisSummaryPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "AnalysisSummaryPage";
            this.Size = new System.Drawing.Size(517, 230);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.analysisResultsTable.ResumeLayout(false);
            this.analysisResultsTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TableLayoutPanel analysisResultsTable;
        private System.Windows.Forms.Label lblAnalysisPM;
        private System.Windows.Forms.Label lblTopLeft;
        private System.Windows.Forms.Label lblAnalysisDates;
        private System.Windows.Forms.LinkLabel linkDetails;

    }
}
