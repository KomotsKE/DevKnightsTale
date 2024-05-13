namespace KnightsTale.Sprites.Weapons
{
    public class PlayerWeapon : Sprite
    {
        public float Scale;
        public Vector2 WeaponOffset;
        public PlayerWeapon(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Origin = new Vector2(Width / 2, Height);
            Scale = 0.5f;
        }

        public virtual void Update(Player player)
        {
            Position = player.Position + WeaponOffset;
            Depth = (player.Position.Y) * Globals.DeepCoef;
            Rotation = Globals.RotateTowards(player.Position, Globals.ScreenToWorldSpace(new Vector2(Globals.Mouse.NewMouse.X, Globals.Mouse.NewMouse.Y)));
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, Rotation, Origin, Scale, SpriteEffects.None, Depth);
        }
    }
}