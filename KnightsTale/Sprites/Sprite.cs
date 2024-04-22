namespace KnightsTale.Sprites
{
    public class Sprite
    {
        public Texture2D texture;
        public Vector2 position;
        public int width;
        public int height;
        public Rectangle CollisionHitBox { get { return rectangle; } }
        public float depth { get { return CollisionHitBox.Bottom * Globals.deepthcof; } }
        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y,
                    width, height);
            }
        }


        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            width = texture.Width;
            height = texture.Height;
        }
        
        public Sprite(Texture2D texture, Vector2 position, int width, int height)
        {
            this.texture = texture;
            this.position = position;
            this.width = width;
            this.height = height;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(texture, rectangle, rectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
        }
    }
}
