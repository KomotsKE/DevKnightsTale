namespace KnightsTale.Sprites.Projectiles
{
    // Внимание костыли
    public class InvisibleMeleeAtack : Projectile
    {
        public InvisibleMeleeAtack(Texture2D texture, Vector2 position, Player owner, Vector2 target) : base(texture, position, owner, target)
        {
            Speed = 2f;
            TimerToDone = new Timer(250);
            pushDistance = 10;
            Recoil = -5;
            if (owner.DontStuck(-Direction * Recoil))
                owner.Position -= Direction * Recoil;
        }

        public override void Update(List<Unit> UnitList, List<Rectangle> collisionGroup, List<Door> doorGroup)
        {
            base.Update(UnitList, collisionGroup, doorGroup);
        }

        public override void Draw()
        {
            //Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, Rotation, Origin, 0.3f, SpriteEffects.None, 0);
        }
    }
}
