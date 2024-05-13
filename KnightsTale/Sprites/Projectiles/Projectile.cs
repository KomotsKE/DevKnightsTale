namespace KnightsTale.Sprites.Projectiles
{
    public class Projectile : Sprite
    {
        public float Speed;
        public Vector2 Direction;
        public bool Done;
        public Unit Owner;
        public Timer TimerToDone;
        public float pushDistance;
        public int Recoil;
        public Projectile(Texture2D texture, Vector2 position, Unit owner, Vector2 target) : base(texture, position)
        {
            Done = false;
            this.Owner = owner;
            Position = owner.Position + new Vector2(0, -5);
            Direction = target - Owner.Position;
            Direction.Normalize();


            Rotation = Globals.RotateTowards(Position, target);

            TimerToDone = new Timer(800);
        }

        public virtual void Update(List<Unit> UnitList, List<Rectangle> collisionGroup, List<Door> doorGroup)
        {
            Depth = Position.Y * Globals.DeepCoef;
            Position += Direction * Speed;

            TimerToDone.UpdateTimer();
            if (TimerToDone.Check())
            {
                Done = true;
            }
            if (HitSomething(UnitList))
            {
                Done = true;
            }
            foreach (var rect in collisionGroup)
            {
                if (rect.Intersects(new Rectangle((int)Position.X, (int)Position.Y, 1, 1)))
                {
                    Done = true;
                }
            }
            foreach (var door in doorGroup)
            {
                if (door.DoorHitBox.CollisionHitBox.Intersects(new Rectangle((int)Position.X, (int)Position.Y, 1, 1)))
                {
                    Done = true;
                }
            }
        }

        public virtual bool HitSomething(List<Unit> UnitList)
        {
            foreach (var unit in UnitList)
            {
                float distance = Vector2.Distance(Position, unit.Position);
                if (Owner.Type != unit.Type && distance < unit.HitDist && !unit.Dead)
                {
                    unit.GetHit(1);
                    float pushDistance = 5;
                    unit.Position += Direction * pushDistance;

                    return true;
                }
            }
            return false;
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
