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
        public Rectangle hb;
        public bool launchingRocket = true;
        public int launchingTicks = 0;
        public int frame = 0;
        public Image i;
        FrameDimension dimension;
        const int warnh = 46;
        const int warnw = 80;
        const int w = 40;
        const int h = 23;
        public rocket(int _x, int _y)
        {
            x = _x;
            y = _y;
            hb = new Rectangle(x, y, warnw, warnh);
            i = Properties.Resources.rocket;
            dimension = new FrameDimension(i.FrameDimensionsList[0]);
            i.SelectActiveFrame(dimension, 0);
        }
        public void move()
        {
            x -= 40;
            hb.X -= 40;
            i.SelectActiveFrame(dimension, 2);
            hb.Width = w;
            hb.Height = h;
        }
    }
}
