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
        public Rectangle hb; //hb 

        public Image i; //image
        
        public laser(int _x, int _y, int _height, int _width, Image _i)
        {
            x = _x;
            y = _y; // hb
            height = _height;
            width = _width;
            hb = new Rectangle(x, y, width, height);

            i = _i; // image to disp         
        }

        public void move ()
        {
            x-= Screens.GameScreen.backgroundMoveSpd; //moves
            hb.X-= Screens.GameScreen.backgroundMoveSpd;
        }
    }
}
