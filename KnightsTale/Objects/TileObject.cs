namespace KnightsTale.Objects
{
    public class TileObject
    {
        public Vector2 Position { get; }
        public int Width { get; }
        public int Height { get; }
        public Rectangle ObjRectangle { get { return new Rectangle((int)Position.X, (int)Position.Y, Width, Height); } }
        public Vector2 Origin { get { return new Vector2(Width / 2, Height / 2); } }

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

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }
    }
}
