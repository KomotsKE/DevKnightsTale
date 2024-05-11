namespace KnightsTale.Sprites
{
    public class Sprite
    {
        public Texture2D Texture;
        public Vector2 Position;
        public int Width, Height;
        public float Depth, Rotation;
        public Vector2 Origin { get; set; }
        public Color Color;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y,
                    Width, Height);
            }
        }


        public Sprite(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;
            Width = texture.Width;
            Height = texture.Height;
            Color = Color.White;
        }

        public Sprite(Texture2D texture, Vector2 position, int width, int height)
        {
            this.Texture = texture;
            this.Position = position;
            this.Width = width;
            this.Height = height;
            Color = Color.White;
        }

        public virtual void Update()
        {
        }

        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, Rectangle, null, Color, Rotation, Origin, SpriteEffects.None, Depth);
        }

        public virtual void Draw(Vector2 offset, Color color)
        {
            Globals.SpriteBatch.Draw(Texture, new Rectangle((int)(Rectangle.X + offset.X), (int)(Rectangle.Y + offset.Y), Width, Height)
                , null, color, Rotation, Origin, SpriteEffects.None, Depth);
        }
    }
}
