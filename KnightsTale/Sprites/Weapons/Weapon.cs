using KnightsTale.Sprites.Units;

namespace KnightsTale.Sprites.Weapons
{
    public class Weapon : Sprite
    {
        public Weapon(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Origin = new Vector2(Width / 2, Height);
        }

        public virtual void Update(Unit unit)
        {
            Position = unit.Position + new Vector2(0, -5);
            Depth = (unit.Position.Y) * Globals.DeepCoef;
            Rotation = Globals.RotateTowards(unit.Position, Globals.ScreenToWorldSpace(new Vector2(Globals.Mouse.NewMouse.X, Globals.Mouse.NewMouse.Y)));
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, Rotation, Origin, 0.5f, SpriteEffects.None, Depth);
        }
    }
}