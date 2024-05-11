using KnightsTale.Objects;
using KnightsTale.Sprites.Projectiles;
using KnightsTale.Sprites.Units;

namespace KnightsTale.Sprites
{
    public class Arrow : Projectile
    {
        public Arrow(Texture2D texture, Vector2 position, Unit owner, Vector2 target) : base(texture, position, owner, target)
        {
            Origin = new Vector2(Width / 2, 0);
        }

        public override void Update(List<Unit> UnitList, List<Rectangle> collisionGroup, List<Door> doorGroup)
        {
            base.Update(UnitList,collisionGroup,doorGroup);
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, Rotation, Origin, 0.3f, SpriteEffects.None, Depth);
        }
    }
}
