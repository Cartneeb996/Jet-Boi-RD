using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jet_Boi_RD
{
    public partial class Form1 : Form
    {
        public static Form form;
        public static bool start = false;
        public Form1()
        {
            InitializeComponent();
            Screens.GameScreen ms = new Screens.GameScreen();
            this.Controls.Add(ms);
            ms.Width = this.Width;
            ms.Height = this.Height;
            form = FindForm();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Start the program centred on the Menu Screen
            
            //ms.Location = new Point((this.Width - ms.Width) / 2, (this.Height - ms.Height) / 2);
        }

        public static void switchScreen(UserControl current, string next)
        {
            UserControl ms = new Screens.ShopScreen();

            switch(next)
            {
                case "game":
                    ms = new Screens.GameScreen();
                    break;
                case "shop":
                    ms = new Screens.ShopScreen();
                    break;
            }

            form.Controls.Add(ms);
            form.Controls.Remove(current);
            ms.Width = form.Width;
            ms.Height = form.Height;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Screens.GameScreen.xmlSave();
        }

        public void close()
        {
            this.Close();
        }
    }
}
