namespace KnightsTale.Models
{
    public class Animation
    {
        readonly Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
        }
        public int FrameWidth;

        public int FrameHeight
        {
            get { return texture.Height; }
        }

        readonly float frameTime;
        public float FrameTime
        {
            get { return frameTime; }
        }

        public int FrameCount;

        readonly bool isLooping;
        public bool IsLooping
        {
            get { return isLooping; }
        }

        public Vector2 Origin;

        public Animation(Texture2D newTexture, int newFrameWidth, float newFrameTime, bool newIsLooping)
        {
            texture = newTexture;
            FrameWidth = newFrameWidth;
            frameTime = newFrameTime;
            isLooping = newIsLooping;
            FrameCount = texture.Width / FrameWidth;
            Origin = new Vector2(FrameWidth / 2, FrameHeight);
        }

        public Animation(Texture2D newTexture, int newFrameWidth, float newFrameTime, bool newIsLooping, Vector2 origin)
        {
            Origin = origin;
            texture = newTexture;
            FrameWidth = newFrameWidth;
            frameTime = newFrameTime;
            isLooping = newIsLooping;
            FrameCount = texture.Width / FrameWidth;
        }
    }
}
