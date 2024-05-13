namespace KnightsTale.Sprites.Units.Monsters
{
    public class Imp : Monster
    {
        public Imp(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, List<Door> doorGroup, string type) : base(texture, position, collisionGroup, doorGroup,type)
        {
            Speed = 1.2f ;
            Health = 1;
            Damage = 2;
            HealthMax = Health;
            WalkAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/Imp_run_anim"), 16, 0.1f, true);
            IdleAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/Imp_idle_anim"), 16, 0.1f, true);
        }

        public override void Update(Player player, SquareGrid grid)
        {
            base.Update(player, grid);
        }

        public override void MeleeAtack(Player player, SquareGrid grid)
        {
            if (Vector2.Distance(player.Position, Position) < grid.SlotDims.X * 1.5f)
            {
                InMove = false;
                if (TimerToAttack.Check())
                {
                    TimerToAttack.ResetToZero();
                    player.GetHit(Damage); PushPlayer(player);
                    GetHit(Damage);
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
