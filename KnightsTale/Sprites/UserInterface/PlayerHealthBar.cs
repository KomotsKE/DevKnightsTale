namespace KnightsTale.Sprites.UserInterface
{
    public class PlayerHealthBar : Sprite
    {
        private readonly Texture2D HalfHeart;
        private readonly Texture2D EmptyHeart;

        private float HeartsCount { get { return HealthMax / 2; } }

        private float Health;
        private readonly float HealthMax;

        public PlayerHealthBar(Texture2D fullHeart, Texture2D halfHeart, Texture2D emptyHeart, float healthMax, Vector2 position) : base(fullHeart, position)
        {
            HalfHeart = halfHeart;
            EmptyHeart = emptyHeart;
            HealthMax = healthMax;
        }

        public void Update(Player player)
        {
            Health = player.Health;
        }

        public override void Draw()
        {
            for (int i = 0; i < HeartsCount; i++)
            {
                Globals.SpriteBatch.Draw(EmptyHeart, new Vector2(Position.X * i * 2.2f, Position.Y), null, Color.White, 0f, Origin, 8f, SpriteEffects.None, 1);
            }
            if (!GameGlobals.IsOver)
            {
                int fullHeartsCount = (int)Health / 2;
                int halfHeartsCount = (int)Health % 2;

                // Отрисовка полных сердец
                for (int i = 0; i < fullHeartsCount; i++)
                {
                    Globals.SpriteBatch.Draw(Texture, new Vector2(Position.X * i * 2.2f, Position.Y), null, Color.White, 0f, Origin, 8f, SpriteEffects.None, 1);
                }

                if (halfHeartsCount > 0)
                {
                    Globals.SpriteBatch.Draw(HalfHeart, new Vector2(fullHeartsCount * Position.X * 2.2f, Position.Y), null, Color.White, 0f, Origin, 8f, SpriteEffects.None, 1);
                }
            }
        }
    }
}
