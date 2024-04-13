using KnightsTale.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace KnightsTale.Sprites
{
    public class Player : Sprite
    {
        public List<Rectangle> CollisionGroup;
        public Rectangle Hitbox { get { return new Rectangle(rectangle.X, rectangle.Y, 16, 16); } }
        public Input input = new Input() { Up = Keys.W, Down = Keys.S, Left = Keys.A, Right = Keys.D };
        public Player(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup) : base(texture, position)
        {
            this.CollisionGroup = collisionGroup;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float changeY = 0;

            if (Keyboard.GetState().IsKeyDown(input.Up))
                changeY -= 5;
            if (Keyboard.GetState().IsKeyDown(input.Down))
                changeY += 5;

            position.Y += changeY;

            foreach (var rectangle in CollisionGroup)
                //if (sprite != this && sprite.rectangle.Intersects(rectangle))
                if (rectangle.Intersects(Hitbox))
                    position.Y -= changeY;

            float changeX = 0;
            if (Keyboard.GetState().IsKeyDown(input.Left))
                changeX -= 5;
            if (Keyboard.GetState().IsKeyDown(input.Right))
                changeX += 5;

            position.X += changeX;

            foreach (var rectangle in CollisionGroup)
                //if (sprite != this && sprite.rectangle.Intersects(rectangle))
                if (rectangle.Intersects(Hitbox))
                    position.X -= changeX;
        }
    }
}
