using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTale.Objects
{
    public class Key : LootableItem
    {
        public string Name;
        public Key(Rectangle rectangle, string key) : base(rectangle)
        {
            Name = key;
            Texture = Globals.Content.Load<Texture2D>("Map/SpriteSheets/key");
        }

        public override void Update(Player player)
        {
            if (ObjRectangle.Contains(player.Position) && !player.Keys.Contains(Name) && !WasPicked)
            {
                player.Keys.Add(Name); WasPicked = true;
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
