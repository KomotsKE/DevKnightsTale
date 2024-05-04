using KnightsTale.Input;
using KnightsTale.Models;
using System;

namespace KnightsTale
{
    public delegate void PassObject(object obj);
    public delegate object PassObjectAndReturn(Object obj);

    public static class Globals
    {
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static GraphicsDeviceManager Graphics { get; set; }
        public static Point WindowSize { get; set; } 
        public static int ScreenWidth { get { return WindowSize.X; } }
        public static int ScreenHeight { get { return WindowSize.Y; } }
        public static float DeepCoef { get; set; }
        public static GameTime gameTime { get; set; }

        public static MouseControl Mouse;
        public static MyKeyboard MyKeyboard;

        public static Camera GameCamera { get; set; }

        public static float RotateTowards(Vector2 pos, Vector2 focus)
        {
            return (float)Math.Atan2(pos.Y - focus.Y, pos.X - focus.X) - MathHelper.PiOver2;
        }

        public static Vector2 ScreenToWorldSpace(in Vector2 point)
        {
            Matrix invertedMatrix = Matrix.Invert(GameCamera.Transform);
            return Vector2.Transform(point, invertedMatrix);
        }
    }
}
