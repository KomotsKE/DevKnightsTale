using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsTale.Sprites
{
    public class Sprite
    {
        private readonly float scale = 6f;
        public Texture2D texture;
        public Vector2 position;
        public int width;
        public int height;
        public Rectangle rectangle
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

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 offSet)
        {
            spriteBatch.Draw(
                texture,
                new Rectangle(
                    rectangle.X + (int)offSet.X,
                    rectangle.Y + (int)offSet.Y,
                    rectangle.Width,
                    rectangle.Height),
                Color.White);
        }
    }
}
