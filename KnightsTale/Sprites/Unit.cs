using KnightsTale.Managers;
using KnightsTale.Models;
using KnightsTale.Objects;

namespace KnightsTale.Sprites
{
    public class Unit : Sprite
    {
        public AnimationManager animationManager;
        public Animation walkAnimation;
        public Animation idleAnimation;
        public bool inMove;
        public Direction direction;

        public List<Rectangle> CollisionGroup;
        public List<Door> DoorGroup;
        public HitBox UnitHitBox;

        public Unit(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, List<Door> doorGroup, Vector2 spawnPoint) : base(texture, position)
        {
            this.position = spawnPoint;
            this.CollisionGroup = collisionGroup;
            this.DoorGroup = doorGroup;
        }

        public override void Draw()
        {
            SpriteEffects flip = SpriteEffects.None;

            if (direction == Direction.right)
                flip = SpriteEffects.None;
            if (direction == Direction.left)
                flip = SpriteEffects.FlipHorizontally;

            animationManager.Draw(Globals.SpriteBatch, position, flip, Depth);
        }

    }
}
