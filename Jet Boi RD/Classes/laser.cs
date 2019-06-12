using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Jet_Boi_RD.Classes
{
    public class laser
    {
        public int x, y, height, width;
        public Rectangle hb;
        public Image i;
        
        public laser(int _x, int _y, int _height, int _width, Image _i)
        {
            x = _x;
            y = _y;
            height = _height;
            i = _i;
            width = _width;
            hb = new Rectangle(x, y, width, height);
        }
        public void move ()
        {
            x-= Screens.GameScreen.backgroundMoveSpd;
            hb.X-= Screens.GameScreen.backgroundMoveSpd;
        }
    }
}
