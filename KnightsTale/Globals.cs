using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsTale
{
    public static class Globals
    {
        public static float TotalSeconds { get; set; }
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static GraphicsDevice graphics {  get; set; }
        public static Point WindowSize { get; set; } 
        public static float deepthcof { get; set; }


        public static void Update(GameTime gametime)
        {
            TotalSeconds = (float)gametime.ElapsedGameTime.TotalSeconds;
        }
    }
}
