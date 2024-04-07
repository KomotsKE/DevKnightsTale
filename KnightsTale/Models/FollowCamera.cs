using KnightsTale.Managers;
using KnightsTale.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace KnightsTale.Models
{
    public class FollowCamera
    {
        public Vector2 position;

        public FollowCamera(Vector2 position)
        {
            this.position = position;
        }

        public void Follow(Rectangle target, Vector2 screenSize)
        {
            position = new Vector2(
                -target.X + (screenSize.X / 2 - target.Width / 2),
                -target.Y + (screenSize.Y / 2 - target.Height / 2)
                );
        }

        public void Draw(SpriteBatch spriteBatch, List<Sprite> sprites, TileMapManager tilemap)
        {
            List<Sprite> sortedSprites = sprites.OrderBy(obj => obj.rectangle.Bottom).ToList();
            tilemap.Draw();
            foreach (Sprite sprite in sortedSprites)
            {
                sprite.Draw(spriteBatch, position);
            }
        }
    }
}
