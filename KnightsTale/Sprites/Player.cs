using KnightsTale.Managers;
using KnightsTale.Models;
using SharpDX.Win32;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace KnightsTale.Sprites
{
    enum Direction
    {
        left,right
    }
    public class Player : Sprite
    {
        private AnimationManager animationManager;
        private Animation walkAnimation;
        private Animation idleAnimation;
        private bool inMove;
        private Direction direction;
        private float layerDepth; 


        public List<Rectangle> CollisionGroup;
        public Rectangle CollisionHitbox { get { return new Rectangle((int)position.X - 6, (int)position.Y-4, 12, 4); } }
        public Input input = new Input() { Up = Keys.W, Down = Keys.S, Left = Keys.A, Right = Keys.D };
        public Player(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup) : base(texture, position)
        {
            this.CollisionGroup = collisionGroup;
        }

        public void Load()
        {
            walkAnimation = new Animation(Globals.Content.Load<Texture2D>("Player/knight_m_walk_anim"), 16, 0.1f, true);
            idleAnimation = new Animation(Globals.Content.Load<Texture2D>("Player/knight_m_idle_anim"), 16, 0.1f, true);
        }

        public override void Update(GameTime gameTime)
        {
            layerDepth = position.Y * Globals.deepthcof + 16;
            base.Update(gameTime);
            inMove = false;
            float changeY = 0;
            if (Keyboard.GetState().IsKeyDown(input.Up))
            { changeY -= 1; inMove = true; }
            if (Keyboard.GetState().IsKeyDown(input.Down))
            { changeY += 1; inMove = true; }

            position.Y += changeY;


            foreach (var rectangle in CollisionGroup)
                if (rectangle.Intersects(CollisionHitbox))
                    position.Y -= changeY;

            float changeX = 0;
            if (Keyboard.GetState().IsKeyDown(input.Left))
            { changeX -= 1; inMove = true; direction = Direction.left; }
            if (Keyboard.GetState().IsKeyDown(input.Right))
            { changeX += 1; inMove = true; direction = Direction.right; }

            position.X += changeX;

            foreach (var rectangle in CollisionGroup)
                if (rectangle.Intersects(CollisionHitbox))
                    position.X -= changeX;

            if (inMove)
                animationManager.PlayAnimation(walkAnimation);
            else
                animationManager.PlayAnimation(idleAnimation);
        }

        public override void Draw()
        {
            SpriteEffects flip = SpriteEffects.None;

            if (direction == Direction.right)
                flip = SpriteEffects.None;
            if (direction == Direction.left)
                flip = SpriteEffects.FlipHorizontally;

            animationManager.Draw(Globals.SpriteBatch, position, flip, layerDepth);
        }

    }
}
