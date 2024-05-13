namespace KnightsTale.Sprites.Units.Monsters
{
    public class Skelet : Monster
    {
        public Skelet(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, List<Door> doorGroup, string type) : base(texture, position, collisionGroup, doorGroup,type)
        {
            Speed = 1;
            Health = 1;
            Damage = 1;
            HealthMax = Health;
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
