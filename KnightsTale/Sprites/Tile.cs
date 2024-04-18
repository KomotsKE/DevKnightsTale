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
        public Tile(Texture2D texture, Vector2 position, int width, int height, Rectangle source,int depth) : base(texture, position, width, height)
        {
            this.source = source;
            //this.depth = depth;
        }
        public override void Draw()
        {
            Globals.SpriteBatch.Draw(texture, rectangle, source, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth) ;
        }
    }
}
