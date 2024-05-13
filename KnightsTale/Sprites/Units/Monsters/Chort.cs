using KnightsTale.Models;

namespace KnightsTale.Sprites.Units.Monsters
{
    public class Chort : Monster
    {
        public Chort(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, List<Door> doorGroup, string type) : base(texture, position, collisionGroup, doorGroup,type)
        {
            Speed = 0.7f;
            Health = 2;
            Damage = 1;
            HealthMax = Health;
            WalkAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/Chort_run_anim"), 16, 0.1f, true);
            IdleAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/Chort_idle_anim"), 16, 0.1f, true);
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
