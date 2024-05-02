using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnightsTale.Objects
{
    public class TileObject
    {
        public Vector2 Position { get; }
        public int Width { get; }
        public int Height { get; }
        public Rectangle ObjRectangle { get { return new Rectangle((int)Position.X, (int)Position.Y, Width, Height); } }
        public Vector2 Origin { get { return new Vector2(Width/2, Height/2); } }

        public TileObject(Vector2 position, int width, int height) 
        {
            this.Position = position;
            this.Width = width;
            this.Height = height;
        }

        public TileObject(Rectangle rectangle)
        {
            Position = new Vector2(rectangle.X, rectangle.Y);
            Width = rectangle.Width;
            Height = rectangle.Height;
        }


        public virtual void Load()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }
    }
}
