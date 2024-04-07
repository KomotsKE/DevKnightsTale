using KnightsTale.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace KnightsTale.Sprites
{
    public class Player : Sprite
    {
        public List<Sprite> CollisionGroup;
        public Input input = new Input() { Up = Keys.W, Down = Keys.S, Left = Keys.A, Right = Keys.D };
        public Player(Texture2D texture, Vector2 position, List<Sprite> collisionGroup) : base(texture, position)
        {
            this.CollisionGroup = collisionGroup;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float changeY = 0;

            if (Keyboard.GetState().IsKeyDown(input.Up))
                changeY -= 40;
            if (Keyboard.GetState().IsKeyDown(input.Down))
                changeY += 40;

            position.Y += changeY;

            foreach (var sprite in CollisionGroup)
                if (sprite != this && sprite.rectangle.Intersects(rectangle))
                    position.Y -= changeY;

            float changeX = 0;
            if (Keyboard.GetState().IsKeyDown(input.Left))
                changeX -= 40;
            if (Keyboard.GetState().IsKeyDown(input.Right))
                changeX += 40;

            position.X += changeX;

            foreach (var sprite in CollisionGroup)
                if (sprite != this && sprite.rectangle.Intersects(rectangle))
                    position.X -= changeX;
        }
    }
}
