using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Jet_Boi_RD.Classes
{
    public class mechToken 
    {
        float x;
        float y; //hb
        public RectangleF hb;

        public string type; //type of powerup

        Random r = new Random();

        public mechToken(float _x, float _y) 
        {
            x = _x;
            y = _y; //hb
            hb = new RectangleF(x, y, 50, 50);

            switch(r.Next(0, 3)) //picks random powerup
            {
                case 0:
                    type = "teleporter";
                    break;
                case 1:
                    type = "superJump";
                    break;
                case 2:
                    type = "gravity";
                    break;
            }

            Screens.GameScreen.abort = !Screens.GameScreen.mechs[type]; // if not bought, abort creation
        }

        public void move()
        {
            x -= Screens.GameScreen.backgroundMoveSpd; //move
            hb.X -= Screens.GameScreen.backgroundMoveSpd;
        } 
    }
}
