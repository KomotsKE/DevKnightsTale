using KnightsTale.Models;
using KnightsTale.Sprites;

namespace KnightsTale.Objects
{
    public class Door : TileObject
    {
        public bool IsOpened;
        public HitBox CollisionHitBox;
        private Texture2D OpenedTexture;
        private Texture2D CloseedTexture;
        private readonly float UsingDistance;
        private float DistanceToPlayer;
        public Door(Rectangle rectangle) : base(rectangle)
        {
            IsOpened = false;
            UsingDistance = 16f;
        }

        public void Update(Vector2 playerPosition)
        {
            DistanceToPlayer = Vector2.Distance(Position, playerPosition);
            if (DistanceToPlayer < UsingDistance && !IsOpened && Globals.MyKeyboard.GetSinglePress("F"))
            {
                IsOpened = true;
            }
            else if (DistanceToPlayer < UsingDistance && IsOpened && Globals.MyKeyboard.GetSinglePress("F"))
            {
                IsOpened = false;
            }
        }
        public override void Load()
        {
            OpenedTexture = Globals.Content.Load<Texture2D>("Map/SpriteSheets/doors_leaf_open");
            CloseedTexture = Globals.Content.Load<Texture2D>("Map/SpriteSheets/doors_leaf_closed");
        }
        public override void Draw()
        {
            if (IsOpened) Globals.SpriteBatch.Draw(OpenedTexture, ObjRectangle, null, Color.White, 0f, Origin, SpriteEffects.None, 1);
            else Globals.SpriteBatch.Draw(CloseedTexture, ObjRectangle, null, Color.White, 0f, Origin, SpriteEffects.None, 1);
        }
    }
}
