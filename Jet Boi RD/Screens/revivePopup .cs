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
    public partial class revivePopup : Form
    {
        public static int dist;
        public  revivePopup()
        {
            InitializeComponent();
            youFlewLabel.Text = "You Flew\n" + dist + "m\nFurthest Flown: " + GameScreen.maxDist/100 + "m"; // displays distance
            coindisp.Text = "Coins: " + GameScreen.coinScore; //displays coins
        }

        private void Yes_Click(object sender, EventArgs e) //handled on gamescreen \/
        {
            DialogResult = DialogResult.Yes;
        }

        private void No_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }
    }
}
