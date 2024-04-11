using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTale.Sprites
{
    public class Tile : Sprite
    {
        Rectangle source;
        public Tile(Texture2D texture, Vector2 position, int width, int height, Rectangle source) : base(texture, position,width,height)
        {
            this.source = source;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,rectangle,source,Color.White);
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offSet)
        {
            var rect = new Rectangle(
                    rectangle.X + (int)offSet.X,
                    rectangle.Y + (int)offSet.Y,
                    rectangle.Width,
                    rectangle.Height);
            spriteBatch.Draw(texture,rect,source,Color.White);
                   
        }
    }
}
