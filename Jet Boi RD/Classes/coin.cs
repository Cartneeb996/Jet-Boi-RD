using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Jet_Boi_RD.Classes
{
    public class coin
    {
        float x;
        float y; //hb for coin
        public RectangleF hb;
        public coin (float _x, float _y) //creates coin
        {
            x = _x;
            y = _y;
            hb = new RectangleF(x, y, 10, 10);
        }
        public void move() // moves coin
        {
            x -= Screens.GameScreen.backgroundMoveSpd;
            hb.X -= Screens.GameScreen.backgroundMoveSpd;
        }
    }
}
