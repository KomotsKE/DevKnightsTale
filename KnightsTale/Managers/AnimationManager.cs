namespace KnightsTale.Managers
{
    public struct AnimationManager
    {
        Animation animation;
        public readonly Animation Animation
        {
            get { return animation; }
        }

        int frameIndex;
        public int FrameIndex
        {
            readonly get { return frameIndex; }
            set { frameIndex = value; }
        }

        private float timer;
        public readonly Vector2 Origin
        {
            get { return Animation.Origin; }
        }

        public void PlayAnimation(Animation newAnimation)
        {
            if (animation == newAnimation)
                return;

            animation = newAnimation;
            frameIndex = 0;
            timer = 0;
        }

        public void RemoveAnimation()
        {
            animation = null;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects, float rotation, float scale, float layerDepth, Color color)
        {
            if (animation == null)
                throw new NotSupportedException("No animation selected");
            if (!GameGlobals.IsPaused && !GameGlobals.IsOver)
            {
                timer += (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
                while (timer >= animation.FrameTime)
                {
                    timer -= animation.FrameTime;

                    if (animation.IsLooping)
                    {
                        frameIndex = (frameIndex + 1) % animation.FrameCount;
                    }
                    else frameIndex = Math.Min(frameIndex + 1, animation.FrameCount - 1);
                }

                Rectangle rectangle = new(frameIndex * Animation.FrameWidth, 0, Animation.FrameWidth, Animation.FrameHeight);

                spriteBatch.Draw(Animation.Texture, position, rectangle, color, rotation, Origin, scale, spriteEffects, layerDepth);
            }
            else
            {
                Rectangle rectangle = new(frameIndex * Animation.FrameWidth, 0, Animation.FrameWidth, Animation.FrameHeight);
                spriteBatch.Draw(Animation.Texture, position, rectangle, color, rotation, Origin, scale, spriteEffects, layerDepth);
            }

        }
    }
}
