using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OscarSnake
{
    enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    class SNAKEPIECE
    {
        public int x;
        public int y;
        public int width;

        public Rectangle HitBox
        {
            get { return new Rectangle(x, y, width, width); }
        }

        public Direction direction = Direction.None;
        //0 none, 1 up, 2 down, 3 left, 4 right 
        public SNAKEPIECE(int x, int y, int width)
        {
            this.x = x;
            this.y = y;
            this.width = width;            
        }
        public void SnakeDirection()
        {
            if (direction == Direction.Down)
            {
                y += width;
            }
            if (direction == Direction.Right)
            {
                x += width;
            }
            if (direction == Direction.Up)
            {
                y -= width;
            }
            if (direction == Direction.Left)
            {
                x -= width;
            }
        }
        public void Draw(Graphics gfx)
        {
            //gfx.FillRectangle(Brushes.Red, x, y, width, length);
            gfx.FillRectangle(Brushes.Purple, x, y, width, width);
        }
    }
}
