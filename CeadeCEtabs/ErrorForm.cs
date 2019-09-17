using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CeadeCEtabs
{
    public partial class ErrorForm : Form
    {
        bool exitAtClose;
        string errorType;
        string errorMessage;
        public ErrorForm(string errorType,string errorMessage, bool exitAtClose)
        {
            this.exitAtClose = exitAtClose;
            this.errorType = errorType;
            this.errorMessage = errorMessage;
            InitializeComponent();
        }

        private void ErrorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.exitAtClose)
            {
                Application.Exit();
            }
        }
        private void ErrorForm_Load(object sender, EventArgs e)
        {
            switch (this.errorType)
            {
                case "INTERNETERROR":
                    label1.Text = "No Internet Connection !";
                    label2.Text = "kindly check your internet connection and try again";
                    break;
                case "ERROR":
                default:
                    label1.Text = "ERROR !";
                    label2.Text = this.errorMessage;
                    break;
            }
        }
    }
}
