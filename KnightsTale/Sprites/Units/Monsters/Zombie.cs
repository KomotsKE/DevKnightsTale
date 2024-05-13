namespace KnightsTale.Sprites.Units.Monsters
{
    public class Zombie : Monster
    {
        public Zombie(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, List<Door> doorGroup, string type) : base(texture, position, collisionGroup, doorGroup,type)
        {
            Speed = 0.4f;
            Health = 1f;
            Damage = 1;
            HealthMax = Health;
            WalkAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/Zombie_anim"), 16, 0.1f, true);
            IdleAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/Zombie_anim"), 16, 0.1f, true);
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
