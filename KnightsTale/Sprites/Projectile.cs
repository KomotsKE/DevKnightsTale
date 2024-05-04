using KnightsTale.Models;
using SharpDX.Direct3D9;

namespace KnightsTale.Sprites
{
    public class Projectile : Sprite
    {
        public float speed;
        public Vector2 direction;
        public bool done;
        public Unit Owner;
        public Timer timer;
        public Projectile(Texture2D texture, Vector2 position, Unit owner, Vector2 target) : base(texture, position)
        {
            done = false;
            speed = 5f;
            this.Owner = owner;

            direction = target - Owner.position;
            direction.Normalize();

            rotation = Globals.RotateTowards(position,new Vector2(target.X, target.Y)); ;

            timer = new Timer(1200);
        }

        public virtual void Update(List<Unit> UnitList)
        {
            Depth = position.Y * Globals.DeepCoef;
            position += direction * speed;

            timer.UpdateTimer();
            if (timer.Test())
            {
                done = true;
            }
            if (HitSomething(UnitList))
            {
                done = true;
            }
        }

        public virtual bool HitSomething(List<Unit> UnitList)
        {
            return false;
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
