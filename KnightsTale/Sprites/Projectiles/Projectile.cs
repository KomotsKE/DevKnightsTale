using KnightsTale.Models;
using KnightsTale.Objects;
using KnightsTale.Sprites.Units;

namespace KnightsTale.Sprites.Projectiles
{
    public class Projectile : Sprite
    {
        public float Speed;
        public Vector2 Direction;
        public bool Done;
        public Unit Owner;
        public Timer TimerToDone;
        public Projectile(Texture2D texture, Vector2 position, Unit owner, Vector2 target) : base(texture, position)
        {
            Done = false;
            Speed = 5f;
            this.Owner = owner;
            Position = owner.Position + new Vector2(0, -5);
            Direction = target - Owner.Position;
            Direction.Normalize();
            if (owner.DontStuck(-Direction * 1))
                owner.Position -= Direction * 1;
            

            Rotation = owner.weapon.Rotation;

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
                if (rect.Intersects(new Rectangle((int)Position.X, (int)Position.Y, 3, 3)))
                {
                    Done = true;
                }
            }
            foreach (var door in doorGroup)
            {
                if (door.DoorHitBox.CollisionHitBox.Intersects(new Rectangle((int)Position.X,(int)Position.Y,3,3)))
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
                if (distance < unit.HitDist)
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
