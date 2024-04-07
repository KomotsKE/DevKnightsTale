using Microsoft.Xna.Framework;

namespace KnightsTale.Managers
{
    public class AnimationManager
    {
        int numColumns;
        int numFrames;
        Vector2 size;

        int counter;
        int activeFrame;
        int interval;

        int rowPos;
        int colPos;

        public int OffsetX { get; set; } = 0;
        public int OffsetY { get; set; } = 0;

        public AnimationManager(int numFrames, int numColumns, Vector2 size)
        {
            this.numFrames = numFrames;
            this.numColumns = numColumns;
            this.size = size;

            counter = 0;
            activeFrame = 0;
            interval = 30;

            rowPos = 0;
            colPos = 0;
        }

        public void Update()
        {
            counter++;
            if (counter > interval)
            {
                counter = 0;
                NextFrame();
            }
        }

        private void NextFrame()
        {
            activeFrame++;
            colPos++;
            if (activeFrame >= numFrames)
            {
                ResetAnimation();
            }
            if (colPos >= numColumns)
            {
                colPos = 0;
                rowPos++;
            }
        }

        private void ResetAnimation()
        {
            activeFrame = 0;
            colPos = 0;
            rowPos = 0;
        }

        public Rectangle GetFrame()
        {
            return new Rectangle(colPos * (int)size.X + OffsetX,
                                rowPos * (int)size.Y + OffsetY,
                                (int)size.X, (int)size.Y);
        }
    }
}
