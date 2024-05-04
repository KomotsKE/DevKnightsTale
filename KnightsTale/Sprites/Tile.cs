namespace KnightsTale.Sprites
{
    public class Tile : Sprite
    {
        private Rectangle source { get; }
        
        public Tile(Texture2D texture, Vector2 position, int width, int height, Rectangle source) : base(texture, position,width,height)
        {
            this.source = source;
        }

        public Tile(Texture2D texture, Vector2 position, int width, int height, Rectangle source, float depth) : base(texture, position, width, height)
        {
            this.source = source;
            this.Depth = depth;
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(texture, Rectangle, source, Color.White, 0f, Vector2.Zero, SpriteEffects.None, Depth);
        }
    }
}
