namespace KnightsTale.Objects
{
    public class Door : TileObject
    {
        public bool IsOpened;
        public bool CanOpen;
        public bool NeedKey;
        public string Key;
        public HitBox DoorHitBox;
        private readonly Texture2D OpenedTexture;
        private readonly Texture2D CloseedTexture;
        private readonly float UsingDistance;
        private float DistanceToPlayer;
        public Door(Rectangle rectangle, string key) : base(rectangle)
        {
            if (key == "None") { NeedKey = false; }
            else { NeedKey = true; Key = key; }
            IsOpened = false;
            CanOpen = !NeedKey;
            UsingDistance = 25f;
            this.DoorHitBox = new HitBox((int)Position.X - Width / 2, (int)Position.Y - Height / 2 + Height - 5, (int)Width, 5, Color.Blue);
            OpenedTexture = Globals.Content.Load<Texture2D>("Map/SpriteSheets/doors_leaf_open");
            CloseedTexture = Globals.Content.Load<Texture2D>("Map/SpriteSheets/doors_leaf_closed");
        }

        public void Update(Player player, SquareGrid grid)
        {
            if (NeedKey)
            {
               if (player.Keys.Contains(Key)) { CanOpen = true; player.Keys.Remove(Key); }
            }
            var posOnGrid = grid.GetSlotFromPixel(Position);
            DistanceToPlayer = Vector2.Distance(Position, player.Position);
            grid.slots[(int)posOnGrid.X][(int)posOnGrid.Y].SwitchGridImassability(!IsOpened);
            grid.slots[(int)posOnGrid.X - 1][(int)posOnGrid.Y].SwitchGridImassability(!IsOpened);
            grid.slots[(int)posOnGrid.X][(int)posOnGrid.Y + 1].SwitchGridImassability(!IsOpened);
            grid.slots[(int)posOnGrid.X - 1][(int)posOnGrid.Y + 1].SwitchGridImassability(!IsOpened);
            if (DistanceToPlayer < UsingDistance && Globals.MyKeyboard.GetSinglePress("F") && CanOpen)
            {
                if (IsOpened) { Globals.SoundManager.PlaySound("Close"); } 
                else { Globals.SoundManager.PlaySound("Open"); }
                IsOpened = !IsOpened;
                grid.slots[(int)posOnGrid.X][(int)posOnGrid.Y + 1].SwitchGridImassability(!IsOpened);
                grid.slots[(int)posOnGrid.X - 1][(int)posOnGrid.Y + 1].SwitchGridImassability(!IsOpened);
                grid.slots[(int)posOnGrid.X][(int)posOnGrid.Y].SwitchGridImassability(!IsOpened);
                grid.slots[(int)posOnGrid.X - 1][(int)posOnGrid.Y].SwitchGridImassability(!IsOpened);
            }
        }

        public override void Draw()
        {
            if (IsOpened) Globals.SpriteBatch.Draw(OpenedTexture, ObjRectangle, null, Color.White, 0f, Origin, SpriteEffects.None, (ObjRectangle.Bottom - Width / 2) * Globals.DeepCoef);
            else Globals.SpriteBatch.Draw(CloseedTexture, ObjRectangle, null, Color.White, 0f, Origin, SpriteEffects.None, (ObjRectangle.Bottom - Width / 2) * Globals.DeepCoef);
        }
    }
}
