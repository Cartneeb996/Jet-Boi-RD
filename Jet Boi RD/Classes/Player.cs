using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Jet_Boi_RD.Classes
{
    class Player
    {
        public int x, y;
        public Rectangle hb;
        public const int height = 120;
        public const int width = 80; //values for player's hitbox

        public Player(int _x, int _y)
        {
            x = _x;
            y = _y;
            hb = new Rectangle(x, y, width, height);

        }
        public void move(int spd) // moves @spd
        {
            y += spd;
            hb.Y += spd;
        }
        public void moveTo(int loc) //moves to loc
        {
            y = loc;
            hb.Y = loc;
        }
    }
}
