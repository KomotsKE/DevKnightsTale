using KnightsTale.Models;
using KnightsTale.Sprites.Units;

namespace KnightsTale.Sprites
{
    public class Weapon : Sprite
    {
        public float Rotation { get; set;}
        
        public HitBox hitBox { get; set; }

        public Weapon(Texture2D texture, Vector2 position) : base(texture, position)
        {
            hitBox = new HitBox(new Rectangle((int)position.X, (int)position.Y, 5, 5), Color.Blue);
        }

        public void Update(Player player)
        {
            position = player.position;
            Depth = (player.position.Y) * Globals.DeepCoef;
            Rotation = Globals.RotateTowards(player.position, Globals.ScreenToWorldSpace(new Vector2(Globals.Mouse.newMouse.X, Globals.Mouse.newMouse.Y)));
            hitBox = new HitBox(new Rectangle((int)position.X, (int)position.Y, 5, 5), Color.Blue);
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(texture, Rectangle, null, Color.White, Rotation + 30f, Origin, SpriteEffects.None, Depth);
        }
    }
}