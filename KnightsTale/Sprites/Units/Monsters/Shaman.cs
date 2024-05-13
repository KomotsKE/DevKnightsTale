namespace KnightsTale.Sprites.Units.Monsters
{
    public class Shaman : Monster
    {
        public float AttackRange;
        public bool IsAttacking;
        public Shaman(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, List<Door> doorGroup, string type) : base(texture, position, collisionGroup, doorGroup,type)
        {
            Speed = 0.6f;
            Health = 2;
            Damage = 1;
            HealthMax = Health;
            TimerToAttack = new Timer(2000);
            WalkAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/Shaman_run_anim"), 16, 0.1f, true);
            IdleAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/Shaman_idle_anim"), 16, 0.1f, true);
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
                    GameGlobals.PassProjectile(new Fireball(Globals.Content.Load<Texture2D>("Projectiles/FB001"), Position, this, player.Position));
                    TimerToAttack.ResetToZero();
                    IsAttacking = false;
                }
            }
            else
            {   
                base.Ai(player, grid);
            }
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
