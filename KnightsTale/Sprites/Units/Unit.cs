using KnightsTale.Grids;
using KnightsTale.Managers;
using KnightsTale.Models;
using KnightsTale.Objects;
using KnightsTale.Sprites.Weapons;

namespace KnightsTale.Sprites.Units
{
    public class Unit : Sprite
    {
        public AnimationManager AnimationManager;
        public Animation WalkAnimation;
        public Animation IdleAnimation;
        public bool InMove;
        public Direction Direction;

        public List<Rectangle> CollisionGroup;
        public List<Door> DoorGroup;

        public Weapon weapon;

        public bool Dead;
        public float HitDist, Speed, Health, HealthMax;
        public Timer ColorSwap;
        public bool UnitGetHit;

        protected Vector2 MoveTo;
        protected List<Vector2> PathNodes = new();

        public Unit(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, List<Door> doorGroup) : base(texture, position)
        {
            ColorSwap = new Timer(500);
            MoveTo = new Vector2(position.X, position.Y);
            CollisionGroup = collisionGroup;
            DoorGroup = doorGroup;
            Dead = false;
            HitDist = 20f;
            Health = 1;
            HealthMax = Health;
        }

        public virtual void GetHit(float damage)
        {
            Health -= damage;
            UnitGetHit = true;
            Color = Color.Red;
            if (Health <= 0)
            {
                Dead = true;
            }
        }

        public override void Update()
        {
            if (UnitGetHit)
            {
                ColorSwap.UpdateTimer();
                if (ColorSwap.Check())
                {
                    Color = Color.White;
                    ColorSwap.ResetToZero();
                    UnitGetHit = false;
                }
            }
            base.Update();
        }

        public override void Draw()
        {
            SpriteEffects flip = SpriteEffects.None;

            if (Direction == Direction.right)
                flip = SpriteEffects.None;
            if (Direction == Direction.left)
                flip = SpriteEffects.FlipHorizontally;

            AnimationManager.Draw(Globals.SpriteBatch, Position, flip, Rotation, 1f, Depth, Color);
        }

        public bool DontStuck(Vector2 offset)
        {
            foreach (var item in CollisionGroup)
            {
                if (item.Intersects(new Rectangle((int)(Rectangle.X - Width / 2 + offset.X), (int)(Rectangle.Y - Height / 2 + offset.Y), Width, Height))) return false;
            }
            foreach (var item in DoorGroup)
            {
                if (item.ObjRectangle.Intersects(new Rectangle((int)(Rectangle.X + offset.X), (int)(Rectangle.Y + offset.Y), Width, Height)) && !item.IsOpened) return false;
            }

            return true;


        }

        public virtual List<Vector2> FindPath(SquareGrid grid, Vector2 end)
        {
            PathNodes.Clear();
            Vector2 tempStartSlot = grid.GetSlotFromPixel(Position);
            List<Vector2> tempPath = grid.GetPath(tempStartSlot, end, true);

            if (tempPath == null || tempPath.Count == 0)
            {

            }
            return tempPath;
        }

        public virtual void MoveUnit()
        {
            if (Position.X != MoveTo.X || Position.Y != MoveTo.Y)
            {
                Position += Globals.RadialMovement(MoveTo, Position, Speed);
                if (MoveTo.X > Position.X) Direction = Direction.right;
                else Direction = Direction.left;
            }
            else if (PathNodes.Count > 0)
            {
                MoveTo = PathNodes[0];
                PathNodes.RemoveAt(0);

                Position += Globals.RadialMovement(MoveTo, Position, Speed);
            }
        }
    }
}
