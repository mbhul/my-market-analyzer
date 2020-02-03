using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyMarketAnalyzer
{
    public partial class AnalysisSummaryPage : UserControl
    {
        private AnalysisResult _Result = null;

        public AnalysisSummaryPage()
        {
            InitializeComponent();
            SetDefaultDisplay();
        }

        public void UpdateProgress(int pProgress)
        {
            if (pProgress >= 0 && pProgress <= 100)
            {
                this.progressBar1.Value = pProgress;

                if(pProgress == 100)
                {
                    this.progressBar1.Value = 0;
                }
            }
        }

        /*****************************************************************************
         *  FUNCTION:           SetResult
         *  Description:        Binds the passed AnalysisResult to this control.
         *  Parameters:     
         *          pResult -   The passed AnalysisResult
         *****************************************************************************/
        public void SetResult(AnalysisResult pResult)
        {
            //Validation?
            this._Result = pResult;
        }

        /*****************************************************************************
         *  FUNCTION:           DisplayResult
         *  Description:        Populates the control display data based on the current
         *                      AnalysisResult binding.
         *  Parameters:         None
         *****************************************************************************/
        public void DisplayResult()
        {
            if(this._Result != null)
            {
                if(_Result.message_string != "")
                {
                    this.lblTopLeft.Text = _Result.message_string;
                    this.lblTopLeft.Visible = true;
                    this.analysisResultsTable.SetColumnSpan(this.lblTopLeft, 3);
                    return;
                }

                this.lblTopLeft.Text = "Net Change:";
                this.analysisResultsTable.SetColumnSpan(this.lblTopLeft, 1);
                foreach (Control item in analysisResultsTable.Controls)
                {
                    item.Visible = true;
                }

                this.lblAnalysisPM.Text = "$ " + this._Result.net_change.ToString();
                this.lblAnalysisDates.Text = String.Format("({0} - {1})", this._Result.dates_from_to.Item1.ToString("MMMM d, yyyy"),
                    this._Result.dates_from_to.Item2.ToString("MMMM d, yyyy"));

                //additional formatting
                if(this._Result.net_change > 0)
                {
                    this.lblAnalysisPM.ForeColor = Color.Green;
                }
                else if (this._Result.net_change < 0)
                {
                    this.lblAnalysisPM.ForeColor = Color.Red;
                }
                else 
                {
                    this.lblAnalysisPM.ForeColor = Color.Black;
                }
            }
        }

        /*****************************************************************************
         *  FUNCTION:           SetDefaultDisplay
         *  Description:        Sets the default display parameters
         *  Parameters:         None
         *****************************************************************************/
        private void SetDefaultDisplay()
        {
            foreach(Control item in analysisResultsTable.Controls)
            {
                item.Visible = false;
            }

            this.lblTopLeft.Text = "No data available.";
            this.lblTopLeft.Visible = true;
            this.analysisResultsTable.SetColumnSpan(this.lblTopLeft, 3);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
