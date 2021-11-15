using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OscarSnake
{
    class Food
    {
        public int x;
        public int y;
        public int height;
        public int width;

        public Rectangle HitBox
        {
            get { return new Rectangle(x, y, width, height); }
        }

        public Food (int x, int y, int height, int width)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public void Draw(Graphics gfx)
        {
            //gfx.FillRectangle(Brushes.Red, x, y, width, length);
            gfx.FillRectangle(Brushes.Red, x, y, width, height);
        }
    }
}
