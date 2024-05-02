using KnightsTale.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace KnightsTale.Sprites
{
    public class Weapon : Sprite
    {
        public Vector2 Origin { get; set; }
        public float Rotation { get; set;}
        
        public HitBox hitBox { get; set; }

        public Weapon(Texture2D texture, Vector2 position) : base(texture, position)
        {
            hitBox = new HitBox(new Rectangle((int)position.X, (int)position.Y, 5, 5), Color.Blue);
            Origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
        }

        public void Update(Player player)
        {
            position = player.position;
            Rotation = Globals.RotateTowards(player.position, Globals.ScreenToWorldSpace(new Vector2(Globals.Mouse.newMouse.X, Globals.Mouse.newMouse.Y)));
            hitBox = new HitBox(new Rectangle((int)position.X, (int)position.Y, 5, 5), Color.Blue);
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(texture, Rectangle, null, Color.White, Rotation, Origin, SpriteEffects.None,Depth);
        }
    }
}