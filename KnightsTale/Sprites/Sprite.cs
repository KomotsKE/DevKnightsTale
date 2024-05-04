using KnightsTale.Models;

namespace KnightsTale.Sprites
{
    public class Sprite
    {
        public Texture2D texture;
        public Vector2 position;
        public int width;
        public int height;
        public float rotation;
        public float Depth { get; set; }
        public Vector2 Origin { get { return new Vector2(width / 2, height / 2); } }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y,
                    width, height);
            }
        }


        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            width = texture.Width;
            height = texture.Height;
        }
        
        public Sprite(Texture2D texture, Vector2 position, int width, int height)
        {
            this.texture = texture;
            this.position = position;
            this.width = width;
            this.height = height;
        }
        public virtual void Load()
        {
        }
        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(texture, Rectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, Depth);
        }
    }
}
