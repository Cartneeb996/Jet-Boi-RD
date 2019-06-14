using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jet_Boi_RD.Screens
{
    public partial class pausePopup : Form
    {
        public pausePopup()
        {
            InitializeComponent();
        }

        private void Yes_Click(object sender, EventArgs e) // these dialogs are used in gamescreen
        {
            DialogResult = DialogResult.Yes;
        }

        private void No_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Form1.form.Close();
        }
    }
}
