using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTale.Objects.Flasks
{
    public class HealthFlask : Flask
    {
        public HealthFlask(Rectangle rectangle) : base(rectangle)
        {
            Texture = Globals.Content.Load<Texture2D>("Map/SpriteSheets/flask_big_green");
            Heal = 2;
        }

        public override void Update(Player player)
        {
            if (ObjRectangle.Contains(player.Position) && !WasPicked)
            {
                WasPicked = true;
                player.HealthMax += 2;
                player.Health += Heal;
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
