namespace KnightsTale.Sprites
{
    public class Tile : Sprite
    {
        private Rectangle Source { get; }

        public Tile(Texture2D texture, Vector2 position, int width, int height, Rectangle source) : base(texture, position, width, height)
        {
            this.Source = source;
        }

        public Tile(Texture2D texture, Vector2 position, int width, int height, Rectangle source, float depth) : base(texture, position, width, height)
        {
            this.Source = source;
            this.Depth = depth;
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, Rectangle, Source, Color.White, 0f, Vector2.Zero, SpriteEffects.None, Depth);
        }
    }
}
