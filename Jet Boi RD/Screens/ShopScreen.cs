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
        public static bool switchS = false;



        public ShopScreen()
        {
            InitializeComponent();
            
            teleporterButton.Visible = !GameScreen.mechs["teleporter"];
            teleportPriceLabel.Visible = !GameScreen.mechs["teleporter"];

            gravitySuitButton.Visible = !GameScreen.mechs["gravity"];
            gravSuitPriceLabel.Visible = !GameScreen.mechs["gravity"];
            
            hogButton.Visible = !GameScreen.mechs["superJump"];
            hogPriceLabel.Visible = !GameScreen.mechs["superJump"];

            gravityBoiButton.Visible = !GameScreen.upgrades["ironBoi"];
            gravBoiPriceLabel.Visible = !GameScreen.upgrades["ironBoi"];

            airBoiButton.Visible = !GameScreen.upgrades["jumpBoost"];
            airBoiPriceLabel.Visible = !GameScreen.upgrades["jumpBoost"];

            xRayBoiButton.Visible = !GameScreen.upgrades["xray"];
            xRayPriceLabel.Visible = !GameScreen.upgrades["xray"];

            jammerBoiButton.Visible = !GameScreen.upgrades["jammer"];
            jamerPriceLabel.Visible = !GameScreen.upgrades["jammer"];

            coinLabel.Text = "Coins: " + GameScreen.coinScore;



        }

        private void TeleporterButton_Click(object sender, EventArgs e)
        {
            if (GameScreen.coinScore >= 500)
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
            if (GameScreen.coinScore >= 500)
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
            if (GameScreen.coinScore >= 500)
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
            Form1.switchScreen(this, "game");
        }

        private void ShopScreen_Load(object sender, EventArgs e)
        {

            if (switchS)
            {
                Form1.switchScreen(this, "game");
                switchS = false;
            }
        }

        private void AirBoiButton_Click(object sender, EventArgs e)
        {
            if (GameScreen.coinScore >= 200)
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
            if (GameScreen.coinScore >= 300)
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
            if (GameScreen.coinScore >= 400)
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
            if (GameScreen.coinScore >= 500)
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
