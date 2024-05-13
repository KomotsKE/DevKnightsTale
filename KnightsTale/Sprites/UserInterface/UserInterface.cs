namespace KnightsTale.Sprites.UserInterface
{
    public class UserInterface
    {
        public SpriteFont Font;
        public PlayerHealthBar PlayerHealthBar;
        public PauseMenu PauseMenu;
        public DeathMenu DeathMenu;
        public DialogBox DialogBox;
        public string tempStr;
        public string tempStr2;
        public UserInterface(Player player)
        {
            Font = Globals.Content.Load<SpriteFont>("Fonts/8-bit");
            PlayerHealthBar = new(Globals.Content.Load<Texture2D>("UserInterface/ui_heart_full"), Globals.Content.Load<Texture2D>("UserInterface/ui_heart_half"),
                Globals.Content.Load<Texture2D>("UserInterface/ui_heart_empty"), player.HealthMax, new Vector2(50, 0));
            Texture2D Board = Globals.Content.Load<Texture2D>("UserInterface/Board");
            PauseMenu = new(Board, new Vector2(Globals.ScreenWidth / 2 - Board.Width / 2, Globals.ScreenHeight / 2 - Board.Height / 2));
            DeathMenu = new(Board, new Vector2(Globals.ScreenWidth / 2 - Board.Width / 2, Globals.ScreenHeight / 2 - Board.Height / 2));
        }

        public void Update(Player player)
        {
            tempStr = "Stamina - " + Math.Round(player.Stamina) + "/100";
            tempStr2 = "Arrows - " + player.ArrowsCount;
            PlayerHealthBar.Update(player);
            if (GameGlobals.IsPaused) PauseMenu.Update();
            if (GameGlobals.IsOver) DeathMenu.Update();
        }

        public void Draw()
        {
            if (!GameGlobals.IsOver && !GameGlobals.IsPaused)
            {
                PlayerHealthBar.Draw();
                Globals.SpriteBatch.DrawString(Font, tempStr, new Vector2(0,100), Color.White, 0f, Vector2.Zero, 1.5F, SpriteEffects.None, 1);
                Globals.SpriteBatch.DrawString(Font, tempStr2, new Vector2(0, 150), Color.White, 0f, Vector2.Zero, 1.5F, SpriteEffects.None, 1);
            }
            else if (GameGlobals.IsPaused)
            {
                Globals.SpriteBatch.DrawString(Font, "Game paused", new Vector2(PauseMenu.Position.X - 150, PauseMenu.Position.Y - 80), Color.White, 0f, Vector2.Zero, 3, SpriteEffects.None, 1);
                PauseMenu.Draw();
            }
            if (GameGlobals.IsOver)
            {
                Globals.SpriteBatch.DrawString(Font, "YOU DIED", new Vector2(PauseMenu.Position.X - 50, PauseMenu.Position.Y - 80), Color.Red, 0f, Vector2.Zero, 3, SpriteEffects.None, 1);
                DeathMenu.Draw();
            }
        }
    }
}