using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTale.Objects.Flasks
{
    public class StaminaFlask : Flask
    {
        public StaminaFlask(Rectangle rectangle) : base(rectangle)
        {
            Texture = Globals.Content.Load<Texture2D>("Map/SpriteSheets/flask_big_yellow");
            Heal = 0;
        }

        public override void Update(Player player)
        {
            if (ObjRectangle.Contains(player.Position) && !WasPicked)
            {
                WasPicked = true;
                player.StaminaMax += 20;
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
