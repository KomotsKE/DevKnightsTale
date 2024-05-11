using KnightsTale.Grids;
using KnightsTale.Models;

namespace KnightsTale.Objects
{
    public class Door : TileObject
    {
        public bool IsOpened;
        public bool CanOpen;
        public bool NeedKey;
        public HitBox DoorHitBox;
        private Texture2D OpenedTexture;
        private Texture2D CloseedTexture;
        private readonly float UsingDistance;
        private float DistanceToPlayer;
        private SquareGrid Grid;
        public Door(Rectangle rectangle, SquareGrid grid) : base(rectangle)
        {
            Grid = grid;
            IsOpened = false;
            CanOpen = true;
            UsingDistance = 25f;
            this.DoorHitBox = new HitBox((int)Position.X - Width / 2, (int)Position.Y - Height / 2 + Height - 5, (int)Width, 5, Color.Blue);
            OpenedTexture = Globals.Content.Load<Texture2D>("Map/SpriteSheets/doors_leaf_open");
            CloseedTexture = Globals.Content.Load<Texture2D>("Map/SpriteSheets/doors_leaf_closed");
        }

        public void Update(Vector2 playerPosition)
        {
            DistanceToPlayer = Vector2.Distance(Position, playerPosition);
            if (DistanceToPlayer < UsingDistance && Globals.MyKeyboard.GetSinglePress("F"))
            {
                if (IsOpened) { Globals.SoundManager.PlaySound("Close"); } 
                else { Globals.SoundManager.PlaySound("Open"); }
                IsOpened = !IsOpened;
            }
            var posOnGrid = Grid.GetSlotFromPixel(Position);
            if (IsOpened)
            {
                Grid.slots[(int)posOnGrid.X][(int)posOnGrid.Y].SetToFilled(false);
                Grid.slots[(int)posOnGrid.X - 1][(int)posOnGrid.Y].SetToFilled(false);
            }
            else
            {
                Grid.slots[(int)posOnGrid.X][(int)posOnGrid.Y].SetToFilled(true);
                Grid.slots[(int)posOnGrid.X - 1][(int)posOnGrid.Y].SetToFilled(true);
            }
        }

        public override void Draw()
        {
            if (IsOpened) Globals.SpriteBatch.Draw(OpenedTexture, ObjRectangle, null, Color.White, 0f, Origin, SpriteEffects.None, (ObjRectangle.Bottom - Width / 2) * Globals.DeepCoef);
            else Globals.SpriteBatch.Draw(CloseedTexture, ObjRectangle, null, Color.White, 0f, Origin, SpriteEffects.None, (ObjRectangle.Bottom - Width / 2) * Globals.DeepCoef);
        }
    }
}
