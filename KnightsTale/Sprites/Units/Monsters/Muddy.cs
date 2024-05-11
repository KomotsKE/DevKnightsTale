using KnightsTale.Models;
using KnightsTale.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTale.Sprites.Units.Monsters
{
    public class Muddy : Monster
    {
        public Muddy(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, List<Door> doorGroup) : base(texture, position, collisionGroup, doorGroup)
        {
            Speed = 1.2f;
            WalkAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/Muddy_anim"), 16, 0.1f, true);
            IdleAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/Muddy_anim"), 16, 0.1f, true);
        }

        public override void Update()
        {
            base.Update();
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
