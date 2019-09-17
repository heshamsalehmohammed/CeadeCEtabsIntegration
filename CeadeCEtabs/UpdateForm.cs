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
    public partial class UpdateForm : Form
    {
        public UpdateForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://localhost/CeadeC/CeadeC/public/CeadeC-PlatForm/users/CeadeCEtabs.php");
            Application.Exit();
        }

        private void UpdateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
