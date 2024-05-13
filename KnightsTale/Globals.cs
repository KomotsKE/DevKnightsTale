using KnightsTale.Input;
using KnightsTale.Managers;
using KnightsTale.Models;

namespace KnightsTale
{
    public delegate void PassObject(object info);
    public delegate object PassObjectAndReturn(object info);

    public static class Globals
    {
        public static MouseCursor CombatCursor { get; set; }
        public static MouseCursor BaseCursor { get; set; }
        private static Random random = new();
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static GraphicsDeviceManager Graphics { get; set; }
        public static SceneManager SceneManager { get; set; }
        public static SoundManager SoundManager { get; set; }
        public static Point WindowSize { get; set; }
        public static int ScreenWidth { get { return WindowSize.X; } }
        public static int ScreenHeight { get { return WindowSize.Y; } }
        public static float DeepCoef { get; set; }
        public static GameTime GameTime { get; set; }

        private static MouseControl mouse;
        private static MyKeyboard myKeyboard;

        public static Camera GameCamera { get; set; }
        public static MyKeyboard MyKeyboard { get => myKeyboard; set => myKeyboard = value; }
        public static MouseControl Mouse { get => mouse; set => mouse = value; }
        public static Random Random { get => random; set => random = value; }

        public static float RotateTowards(Vector2 pos, Vector2 focus)
        {
            return (float)Math.Atan2(pos.Y - focus.Y, pos.X - focus.X) - MathHelper.PiOver2;
        }

        public static Vector2 RadialMovement(Vector2 focus, Vector2 pos, float speed)
        {
            float dist = Vector2.Distance(pos, focus);

            if (dist <= speed)
            {
                return focus - pos;
            }
            else
            {
                return (focus - pos) * speed / dist;
            }
        }

        public static Vector2 ScreenToWorldSpace(in Vector2 point)
        {
            Matrix invertedMatrix = Matrix.Invert(GameCamera.Transform);
            return Vector2.Transform(point, invertedMatrix);
        }
    }
}
