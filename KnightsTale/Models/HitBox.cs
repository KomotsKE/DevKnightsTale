namespace KnightsTale.Models
{
    public class HitBox
    {
        public Rectangle CollisionHitBox {  get; }
        public int width { get; }
        public int height {  get; }
        public Color color {  get; set; }

        public HitBox(int x, int y, int width, int height, Color color) 
        {
            this.width = width;
            this.height = height;
            this.color = color;
            CollisionHitBox = new Rectangle(x, y, width, height);
        }

        public HitBox(Rectangle rectangle, Color color)
        {
            this.width = rectangle.Width;
            this.height = rectangle.Height;
            this.color = color;
            CollisionHitBox = rectangle;
        }

        public void Draw()
        {
            Globals.SpriteBatch.DrawRectangle(CollisionHitBox, color);
        }
    }
}
