using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MyMarketAnalyzer
{
    public partial class AnalysisToolbox : UserControl
    {
        private const UInt32 WM_ANALYSISFUNCSELECT = 0xA00B;
        private const UInt32 WM_ANALYSISVARSELECT = 0xA00C;

        private List<Button> BtnListFunctions;
        private List<Button> BtnListVariables;

        public AnalysisToolbox()
        {
            InitializeComponent();
            loadButtons();
        }

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SendMessage(
                          IntPtr hWnd,      // handle to destination window
                          UInt32 Msg,       // message
                          IntPtr wParam,  // first message parameter
                          IntPtr lParam   // second message parameter
                          );

        private void loadButtons()
        {
            int itterator = 0;
            int textLen = 0;
            BtnListFunctions = new List<Button>();
            BtnListVariables = new List<Button>();

            foreach(Fn func in RuleParserInputs.Fns)
            {
                BtnListFunctions.Add(new Button());
                BtnListFunctions[itterator].Text = StringEnum.GetStringValue(func);
                BtnListFunctions[itterator].Click += new EventHandler(this.analysisBtnFunc_OnClick);
                this.flpFunctions.Controls.Add(BtnListFunctions[itterator]);
                itterator++;
            }

            itterator = 0;
            foreach (Variable vEn in RuleParserInputs.VarList)
            {
                textLen = RuleParserInputs.VarCaptions[itterator].Length;
                BtnListVariables.Add(new Button());
                BtnListVariables[itterator].Text = RuleParserInputs.VarCaptions[itterator];
                BtnListVariables[itterator].Click += new EventHandler(this.analysisBtnVar_OnClick);
                this.flpVariables.Controls.Add(BtnListVariables[itterator]);

                //Increase the size of the button if necessary to get all text to fit on 1 line
                if (textLen > 9)
                {
                    BtnListVariables[itterator].Width += (textLen - 9) * 8;
                }
                itterator++;
            }
        }

        private void analysisBtnFunc_OnClick(object sender, EventArgs e)
        {
            Fn selected_function = RuleParserInputs.Fns[BtnListFunctions.IndexOf((Button)sender)];
            SendMessage(Application.OpenForms[0].Handle, WM_ANALYSISFUNCSELECT, (IntPtr)(int)selected_function, IntPtr.Zero);
        }

        private void analysisBtnVar_OnClick(object sender, EventArgs e)
        {
            Variable selected_var = RuleParserInputs.VarList[BtnListVariables.IndexOf((Button)sender)];
            SendMessage(Application.OpenForms[0].Handle, WM_ANALYSISVARSELECT, (IntPtr)(int)selected_var, IntPtr.Zero);
        }

    }
}
