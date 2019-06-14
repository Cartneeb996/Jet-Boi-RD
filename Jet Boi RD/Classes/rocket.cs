using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Jet_Boi_RD.Classes
{
    public class rocket
    {
        public int x, y;
        public Rectangle hb; //hb
        const int warnh = 46; //hb for warning
        const int warnw = 80;
        const int w = 40;
        const int h = 23; //hb for rocket

        public bool launchingRocket = true; // in warning stage?
        public int launchingTicks = 0; 

        public int frame = 0;
        public Image i; // for anim
        FrameDimension dimension;
        
        public rocket(int _x, int _y)
        {
            x = _x;
            y = _y; //hb
            hb = new Rectangle(x, y, warnw, warnh);

            i = Properties.Resources.rocket;
            dimension = new FrameDimension(i.FrameDimensionsList[0]); //anim
            i.SelectActiveFrame(dimension, 0);
        }
        public void move()
        {
            x -= 40;
            hb.X -= 40; // move

            i.SelectActiveFrame(dimension, 2); //change anim

            hb.Width = w; //change hb
            hb.Height = h;
        }
    }
}
