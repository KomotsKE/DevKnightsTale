using KnightsTale.Models;
using KnightsTale.Objects;

namespace KnightsTale.Sprites.Units.Monsters
{
    public class Wogol: Monster
    {
        public Wogol(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, List<Door> doorGroup) : base(texture, position, collisionGroup, doorGroup)
        {
            Speed = 0.4f;
            Health = 4;
            Damage = 2;
            WalkAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/Wogol_run_anim"), 16, 0.1f, true);
            IdleAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/Wogol_idle_anim"), 16, 0.1f, true);
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
