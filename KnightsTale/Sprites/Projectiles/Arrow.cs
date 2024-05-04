using KnightsTale.Models;

namespace KnightsTale.Sprites
{
    public class Arrow: Projectile
    {
        public Arrow(Texture2D texture, Vector2 position, Unit owner, Vector2 target) : base(texture ,position, owner, target)
        {
        }

        public override void Update(List<Unit> UnitList)
        {
            base.Update(UnitList);
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(texture,Rectangle,null,Color.White,rotation,Origin,SpriteEffects.None,Depth);
        }
    }
}
