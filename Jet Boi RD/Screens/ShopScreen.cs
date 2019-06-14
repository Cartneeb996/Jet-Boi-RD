using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jet_Boi_RD.Screens
{
    public partial class ShopScreen : UserControl
    {
        public static bool switchS = false; // for switching back to gamescreen

        public ShopScreen()
        {
            InitializeComponent();
            
            teleporterButton.Visible = !GameScreen.mechs["teleporter"]; // hides if bought
            teleportPriceLabel.Visible = !GameScreen.mechs["teleporter"];

            gravitySuitButton.Visible = !GameScreen.mechs["gravity"]; // hides if bought
            gravSuitPriceLabel.Visible = !GameScreen.mechs["gravity"];
            
            hogButton.Visible = !GameScreen.mechs["superJump"]; // hides if bought
            hogPriceLabel.Visible = !GameScreen.mechs["superJump"];

            gravityBoiButton.Visible = !GameScreen.upgrades["ironBoi"]; // hides if bought
            gravBoiPriceLabel.Visible = !GameScreen.upgrades["ironBoi"];

            airBoiButton.Visible = !GameScreen.upgrades["jumpBoost"]; // hides if bought
            airBoiPriceLabel.Visible = !GameScreen.upgrades["jumpBoost"];

            xRayBoiButton.Visible = !GameScreen.upgrades["xray"]; // hides if bought
            xRayPriceLabel.Visible = !GameScreen.upgrades["xray"];

            jammerBoiButton.Visible = !GameScreen.upgrades["jammer"]; // hides if bought
            jamerPriceLabel.Visible = !GameScreen.upgrades["jammer"];

            coinLabel.Text = "Coins: " + GameScreen.coinScore; //displays currency
        }

        private void TeleporterButton_Click(object sender, EventArgs e)
        {
            if (GameScreen.coinScore >= 500) // purchase, hide and remove coins
            {
                GameScreen.coinScore -= 500;
                GameScreen.mechs["teleporter"] = true;
                teleporterButton.Enabled = false;
                teleporterButton.Visible = false;
                teleportPriceLabel.Visible = false;
                GameScreen.xmlSave();
            }
        }

        private void GravitySuitButton_Click(object sender, EventArgs e) 
        {
            if (GameScreen.coinScore >= 500)  // purchase, hide and remove coins
            {
                GameScreen.coinScore -= 500;
                GameScreen.mechs["gravity"] = true;
                gravitySuitButton.Enabled = false;
                gravitySuitButton.Visible = false;
                gravSuitPriceLabel.Visible = false;
                GameScreen.xmlSave();
            }
        }

        private void HogButton_Click(object sender, EventArgs e)
        {
            if (GameScreen.coinScore >= 500)  // purchase, hide and remove coins
            {
                GameScreen.coinScore -= 500;
                GameScreen.mechs["superJump"] = true;
                hogButton.Enabled = false;
                hogButton.Visible = false;
                hogPriceLabel.Visible = false;
                GameScreen.xmlSave();
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Form1.switchScreen(this, "game"); // return to game
        }

        private void ShopScreen_Load(object sender, EventArgs e)
        {

            if (switchS) // switches back to game
            {
                Form1.switchScreen(this, "game");
                switchS = false;
            }
        }

        private void AirBoiButton_Click(object sender, EventArgs e)
        {
            if (GameScreen.coinScore >= 200)  // purchase, hide and remove coins
            {
                GameScreen.coinScore -= 200;
                GameScreen.upgrades["jumpBoost"] = true;
                airBoiButton.Enabled = false;
                airBoiButton.Visible = false;
                airBoiPriceLabel.Visible = false;
                GameScreen.xmlSave();
            }
        }

        private void GravityBoiButton_Click(object sender, EventArgs e)
        {
            if (GameScreen.coinScore >= 300)  // purchase, hide and remove coins
            {
                GameScreen.coinScore -= 300;
                GameScreen.upgrades["ironBoi"] = true;
                gravityBoiButton.Enabled = false;
                gravityBoiButton.Visible = false;
                gravBoiPriceLabel.Visible = false;
                GameScreen.xmlSave();
            }
        }

        private void XRayBoiButton_Click(object sender, EventArgs e)
        {
            if (GameScreen.coinScore >= 400)  // purchase, hide and remove coins
            {
                GameScreen.coinScore -= 400;
                GameScreen.upgrades["xray"] = true;
                xRayBoiButton.Enabled = false;
                xRayBoiButton.Visible = false;
                xRayPriceLabel.Visible = false;
                GameScreen.xmlSave();
            }
        }

        private void JammerBoiButton_Click(object sender, EventArgs e)
        {
            if (GameScreen.coinScore >= 500)  // purchase, hide and remove coins
            {
                GameScreen.coinScore -= 500;
                GameScreen.upgrades["jammer"] = true;
                jammerBoiButton.Enabled = false;
                jammerBoiButton.Visible = false;
                jamerPriceLabel.Visible = false;
                GameScreen.xmlSave();
            }
        }
    }
}
