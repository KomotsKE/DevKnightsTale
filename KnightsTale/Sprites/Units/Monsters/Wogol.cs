namespace KnightsTale.Sprites.Units.Monsters
{
    public class Wogol: Monster
    {
        public Wogol(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, List<Door> doorGroup, string type) : base(texture, position, collisionGroup, doorGroup,type)
        {
            Speed = 0.4f;
            Health = 4;
            Damage = 2;
            HealthMax = Health;
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
