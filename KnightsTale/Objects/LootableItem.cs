using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTale.Objects
{
    public class LootableItem : TileObject
    {
        public Texture2D Texture;
        public bool WasPicked;
        public LootableItem(Rectangle rectangle) : base(rectangle)
        {
        }

        public override void Update(Player player)
        {
            if (ObjRectangle.Contains(player.Position) && !WasPicked)
            {
                 WasPicked = true;
            }
        }

        public override void Draw()
        {
            if (!WasPicked) { Globals.SpriteBatch.Draw(Texture, ObjRectangle, null, Color.White, 0f, Origin, SpriteEffects.None, ObjRectangle.Bottom * Globals.DeepCoef); }
        }
    }
}
