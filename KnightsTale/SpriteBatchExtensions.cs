namespace KnightsTale
{
    public static class SpriteBatchExtensions
    {
        private static Texture2D _pixel;

        public static void LoadPixel(this SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            _pixel = new Texture2D(graphicsDevice, 1, 1);
            _pixel.SetData(new Color[] { Color.White });
        }
        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rectangle, Color color, float depth = 1, int thickness = 1)
        {
            spriteBatch.Draw(_pixel, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, thickness), null, color, 0f, Vector2.Zero, SpriteEffects.None, depth);
            spriteBatch.Draw(_pixel, new Rectangle(rectangle.X, rectangle.Y, thickness, rectangle.Height), null, color, 0f, Vector2.Zero, SpriteEffects.None, depth);
            spriteBatch.Draw(_pixel, new Rectangle(rectangle.X + rectangle.Width - thickness, rectangle.Y, thickness, rectangle.Height), null, color, 0f, Vector2.Zero, SpriteEffects.None, depth);
            spriteBatch.Draw(_pixel, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - thickness, rectangle.Width, thickness), null, color, 0f, Vector2.Zero, SpriteEffects.None, depth);
        }
    }
}
