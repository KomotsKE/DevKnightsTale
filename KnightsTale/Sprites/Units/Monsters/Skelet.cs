using KnightsTale.Models;
using KnightsTale.Objects;

namespace KnightsTale.Sprites.Units.Monsters
{
    public class Skelet : Monster
    {
        public Skelet(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, List<Door> doorGroup) : base(texture, position, collisionGroup, doorGroup)
        {
            Speed = 1.2f;
            WalkAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/skelet_run_anim"), 16, 0.1f, true);
            IdleAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/skelet_idle_anim"), 16, 0.1f, true);
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
