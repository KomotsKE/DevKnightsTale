namespace KnightsTale.Sprites.Units
{
    public class Player : Unit
    {
        private HitBox HitBox { get { return new HitBox((int)Position.X - 5, (int)Position.Y - 4, 10, 4, Color.Red); ; } }
        private Vector2 Movement { get; set; }
        private readonly Crossbow weapon;
        private bool CanPressShift;
        public float Stamina, StaminaMax;
        public int ArrowsCount;
        public List<string> Keys;
        public Player(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, List<Door> doorGroup, string type) : base(texture, position, collisionGroup, doorGroup,type)
        {
            Keys = new();
            CanPressShift = true;
            weapon = new Crossbow(Globals.Content.Load<Texture2D>("Weapon/Crossbow_2"), Globals.Content.Load<Texture2D>("Weapon/Loaded_Crossbow_2"), Position);
            Speed = 1f;
            Health = 20;
            HealthMax = Health;
            Stamina = 100;
            StaminaMax = Stamina;
            ArrowsCount = 20;
            WalkAnimation = new Animation(Globals.Content.Load<Texture2D>("Player/knight_m_walk_anim"), 16, 0.1f, true);
            IdleAnimation = new Animation(Globals.Content.Load<Texture2D>("Player/knight_m_idle_anim"), 16, 0.1f, true);
        }

        public override void Update()
        {
            if (Health > HealthMax) { Health = HealthMax;}
            if (Dead) GameGlobals.IsOver = true;
            Depth = (Position.Y) * Globals.DeepCoef;
            weapon.Update(this);
            MovementLogic();
            RunLogic();

            
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            weapon.Draw();
        }

        public void RunLogic()
        {
            if (Globals.MyKeyboard.GetPress("LeftShift") && Stamina > 0 && CanPressShift)
            {
                Speed = 1.3f;
                Stamina -= 0.7f;
            }
            else if (Stamina > 0 && Stamina < 30)
            {
                Stamina += 0.25f;
                Speed = 0.5f;
                CanPressShift = false;
            }
            else if (Stamina < StaminaMax && !Globals.MyKeyboard.GetPress("LeftShift"))
            {
                Stamina += 0.25f;
                Speed = 0.8f;
            }
            else
            {
                Speed = 1f;
                CanPressShift = true;
            }
        }

        private void PlayAnimation()
        {
            if (InMove)
                AnimationManager.PlayAnimation(WalkAnimation);
            else
                AnimationManager.PlayAnimation(IdleAnimation);
        }

        public void CheckCollision(float changeX, float changeY)
        {
            foreach (var rectangle in CollisionGroup)
                if (rectangle.Intersects(HitBox.CollisionHitBox))
                    Position.Y -= changeY;

            foreach (var door in DoorGroup)
                if (door.DoorHitBox.CollisionHitBox.Intersects(HitBox.CollisionHitBox) && !door.IsOpened)
                    Position.Y -= changeY;

            foreach (var rectangle in CollisionGroup)
                if (rectangle.Intersects(HitBox.CollisionHitBox))
                    Position.X -= changeX;

            foreach (var door in DoorGroup)
                if (door.DoorHitBox.CollisionHitBox.Intersects(HitBox.CollisionHitBox) && !door.IsOpened)
                    Position.X -= changeX;
        }

        public void MovementLogic()
        {
            InMove = false;
            Movement = Vector2.Zero;

            if (Globals.MyKeyboard.GetPress("W"))
            { Movement += new Vector2(0, -1); }
            if (Globals.MyKeyboard.GetPress("S"))
            { Movement += new Vector2(0, 1); }
            if (Globals.MyKeyboard.GetPress("A"))
            { Movement += new Vector2(-1, 0); Direction = Direction.left; }
            if (Globals.MyKeyboard.GetPress("D"))
            { Movement += new Vector2(1, 0); Direction = Direction.right; }

            if (Movement != Vector2.Zero) InMove = true;

            Movement *= Speed;

            if (Movement.LengthSquared() > Speed * Speed) Movement *= (float)(1 / Math.Sqrt(2)); ;

            Position += Movement;

            CheckCollision(Movement.X, Movement.Y);
            PlayAnimation();
        }

    }
}
