using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTale.Objects.Flasks
{
    public class BigHealFlask : Flask
    {
        public BigHealFlask(Rectangle rectangle) : base(rectangle)
        {
            Texture = Globals.Content.Load<Texture2D>("Map/SpriteSheets/flask_big_red");
            Heal = 8;
        }

        public override void Update(Player player)
        {
            base.Update(player);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
