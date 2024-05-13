namespace KnightsTale.Models
{
    public class HitBox
    {
        public Rectangle CollisionHitBox { get; set; }
        public int Width { get; }
        public int Height { get; }
        public Color Color { get; set; }

        public HitBox(int x, int y, int width, int height, Color color)
        {
            this.Width = width;
            this.Height = height;
            this.Color = color;
            CollisionHitBox = new Rectangle(x, y, width, height);
        }

        public HitBox(Rectangle rectangle, Color color)
        {
            this.Width = rectangle.Width;
            this.Height = rectangle.Height;
            this.Color = color;
            CollisionHitBox = rectangle;
        }

        public void Draw()
        {
            Globals.SpriteBatch.DrawRectangle(CollisionHitBox, Color);
        }
    }
}
