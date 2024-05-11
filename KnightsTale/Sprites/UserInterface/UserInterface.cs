
using KnightsTale.Sprites.Units;
using KnightsTale.Sprites.UserInterface;

namespace KnightsTale.Models
{
    public class UserInterface
    {
        public SpriteFont Font;
        public PlayerHealthBar PlayerHealthBar;
        public PauseMenu PauseMenu;
        public DeathMenu DeathMenu;
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
            PlayerHealthBar.Update(player);
            if (GameGlobals.IsPaused) PauseMenu.Update();
            if (GameGlobals.IsOver) DeathMenu.Update();
        }

        public void Draw(Player player)
        {
            if (!GameGlobals.IsOver && !GameGlobals.IsPaused)
            {
                PlayerHealthBar.Draw();
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