using KnightsTale.Models;
using KnightsTale.Sprites;

namespace KnightsTale.Objects
{
    public class Door : TileObject
    {
        public bool IsOpened;
        public bool CanOpen;
        public HitBox DoorHitBox;
        private Texture2D OpenedTexture;
        private Texture2D CloseedTexture;
        private readonly float UsingDistance;
        private float DistanceToPlayer;
        public Door(Rectangle rectangle) : base(rectangle)
        {
            IsOpened = false;
            CanOpen = true;
            UsingDistance = 25f;
            this.DoorHitBox = new HitBox((int)Position.X - Width/2,(int)Position.Y - Height/2 + Height - 5,(int)Width,5,Color.Blue);
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
            else DoNothing();
        }

        private void DoNothing()
        {
        }

        public override void Load()
        {
            OpenedTexture = Globals.Content.Load<Texture2D>("Map/SpriteSheets/doors_leaf_open");
            CloseedTexture = Globals.Content.Load<Texture2D>("Map/SpriteSheets/doors_leaf_closed");
        }
        public override void Draw()
        {
            if (IsOpened) Globals.SpriteBatch.Draw(OpenedTexture, ObjRectangle, null, Color.White, 0f, Origin, SpriteEffects.None, (ObjRectangle.Bottom - Width/2) * Globals.DeepCoef);
            else Globals.SpriteBatch.Draw(CloseedTexture, ObjRectangle, null, Color.White, 0f, Origin, SpriteEffects.None, (ObjRectangle.Bottom - Width/2) * Globals.DeepCoef);
            DoorHitBox.Draw();
        }
    }
}
