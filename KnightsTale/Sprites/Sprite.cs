using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsTale.Sprites
{
    public class Sprite
    {
        private readonly float scale = 1f;
        public Texture2D texture;
        public Vector2 position;
        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y,
                    texture.Width * (int)scale, texture.Height * (int)scale);
            }
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
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
