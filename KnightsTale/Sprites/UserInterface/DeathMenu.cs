using KnightsTale.Sprites;
using KnightsTale.Sprites.UserInterface;

namespace KnightsTale.Models
{
    public class DeathMenu : Menu
    {
        public DeathMenu(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Buttons = new()
            {
                new Button(Globals.Content.Load<Texture2D>("Buttons/Restart/restart01"), Globals.Content.Load<Texture2D>("Buttons/Restart/restart02"),
                    Globals.Content.Load<Texture2D>("Buttons/Restart/restart03"), new Vector2(Position.X + 37, Position.Y + 120), Restart, 0.3f),
            };
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
        public void Restart(object info) => RestartFlag = true;

        public void Exit(object info) => Environment.Exit(0);
    }
}