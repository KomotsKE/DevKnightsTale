using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTale.Objects.Flasks
{
    public class Flask : LootableItem
    {
        public int Heal;
        public Flask(Rectangle rectangle) : base(rectangle)
        {
            Heal = 1;
        }

        public override void Update(Player player)
        {
            if (ObjRectangle.Contains(player.Position) && !WasPicked && player.Health < player.HealthMax)
            {
                WasPicked = true;
                player.Health += Heal;
            }
        }

        public override void Draw()
        {
            if (!WasPicked) { Globals.SpriteBatch.Draw(Texture, ObjRectangle, null, Color.White, 0f, Origin, SpriteEffects.None, ObjRectangle.Bottom * Globals.DeepCoef); }
        }
    }
}
