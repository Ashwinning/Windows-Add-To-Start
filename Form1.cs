using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_Add_To_Start
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Setting default form visibility to hidden on load.
        /// Form is only set to be visible when user input is required.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            base.OnLoad(e);
        }

        public Form1()
        {
            InitializeComponent();
            string[] args = Environment.GetCommandLineArgs();
            MessageBox.Show(args[1]);
        }
    }
}
