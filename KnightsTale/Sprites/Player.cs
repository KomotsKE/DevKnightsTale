using KnightsTale.Managers;
using KnightsTale.Models;
using SharpDX.Win32;
using System.Collections.Generic;
using KnightsTale.Input;

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

        private readonly Weapon weapon;


        public List<Rectangle> CollisionGroup;
        public HitBox HitBox { get { return new HitBox((int)position.X-5, (int)position.Y - 4, 10, 4, Color.Red); } }
        public Vector2 Origin { get { return new Vector2((int)position.X, (int)position.Y-8); } }

        public Player(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, Vector2 spawnPoint) : base(texture, position)
        {
            this.CollisionGroup = collisionGroup;
            this.position = spawnPoint;
            //this.weapon = new Weapon(Globals.Content.Load<Texture2D>("Weapon/weapon_bow"), position);
        }

        public override void Load()
        {
            walkAnimation = new Animation(Globals.Content.Load<Texture2D>("Player/knight_m_walk_anim"), 16, 0.1f, true);
            idleAnimation = new Animation(Globals.Content.Load<Texture2D>("Player/knight_m_idle_anim"), 16, 0.1f, true);
        }

        public void Movement()
        {
            inMove = false;
            float changeY = 0;
            if (Globals.MyKeyboard.GetPress("W"))
            { changeY -= 1; inMove = true; }
            if (Globals.MyKeyboard.GetPress("S"))
            { changeY += 1; inMove = true; }

            position.Y += changeY;


            foreach (var rectangle in CollisionGroup)
                if (rectangle.Intersects(HitBox.CollisionHitBox))
                    position.Y -= changeY;

            float changeX = 0;
            if (Globals.MyKeyboard.GetPress("A"))
            { changeX -= 1; inMove = true; direction = Direction.left; }
            if (Globals.MyKeyboard.GetPress("D")) 
            { changeX += 1; inMove = true; direction = Direction.right; }

            position.X += changeX;

            foreach (var rectangle in CollisionGroup)
                if (rectangle.Intersects(HitBox.CollisionHitBox))
                    position.X -= changeX;

            if (inMove)
                animationManager.PlayAnimation(walkAnimation);
            else
                animationManager.PlayAnimation(idleAnimation);
        }

        public override void Update(GameTime gameTime)
        {
            Depth = Rectangle.Bottom / 1000;
            //weapon.Update(this);
            Movement();
            base.Update(gameTime);
        }

        public override void Draw()
        {
            SpriteEffects flip = SpriteEffects.None;

            if (direction == Direction.right)
                flip = SpriteEffects.None;
            if (direction == Direction.left)
                flip = SpriteEffects.FlipHorizontally;

            animationManager.Draw(Globals.SpriteBatch, position, flip, Depth);
            //weapon.Draw();
        }

    }
}
