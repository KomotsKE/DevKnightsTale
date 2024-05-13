namespace KnightsTale.Sprites.Units.Monsters
{
    public class Necromancer : Monster
    {
        public float AttackRange;
        public bool IsAttacking;
        public Necromancer(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, List<Door> doorGroup, string type) : base(texture, position, collisionGroup, doorGroup,type)
        {
            Speed = 0.6f;
            Health = 2;
            Damage = 1;
            HealthMax = Health;
            TimerToAttack = new Timer(5000);
            WalkAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/Necromancer_anim"), 16, 0.1f, true);
            IdleAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/Necromancer_anim"), 16, 0.1f, true);
            AttackRange = 70f;
        }

        public override void Ai(Player player, SquareGrid grid)
        {
            if ((player != null && Vector2.Distance(Position, player.Position) < AttackRange || IsAttacking) && CanSeePlayer(player))
            {
                LastKnownPlayerLocatuin = player.Position;
                InMove = false;
                IsAttacking = true;
                TimerToAttack.UpdateTimer();

                if (TimerToAttack.Check())
                {
                    GameGlobals.PassMonster(new Zombie(Globals.Content.Load<Texture2D>("Monsters/BaseMonster"), Position, CollisionGroup, DoorGroup, "Monster"));
                    TimerToAttack.ResetToZero();
                    IsAttacking = false;
                }
            }
            else
            {
                base.Ai(player, grid);
            }
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
