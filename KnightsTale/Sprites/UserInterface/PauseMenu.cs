namespace KnightsTale.Sprites.UserInterface
{
    public class PauseMenu : Menu
    {
        public PauseMenu(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Buttons = new()
            {
                new Button(Globals.Content.Load<Texture2D>("Buttons/Play/play01"), Globals.Content.Load<Texture2D>("Buttons/Play/play02"),
                    Globals.Content.Load<Texture2D>("Buttons/Play/play03"), new Vector2(Position.X + 37, Position.Y + 30), Play, 0.3f),
                new Button(Globals.Content.Load<Texture2D>("Buttons/Restart/restart01"), Globals.Content.Load<Texture2D>("Buttons/Restart/restart02"),
                    Globals.Content.Load<Texture2D>("Buttons/Restart/restart03"), new Vector2(Position.X + 37, Position.Y + 120), Restart, 0.3f),
                new Button(Globals.Content.Load<Texture2D>("Buttons/Exit/Exit"), Globals.Content.Load<Texture2D>("Buttons/Exit/Exit02"),
                    Globals.Content.Load<Texture2D>("Buttons/Exit/Exit03"), new Vector2(Position.X + 37 , Position.Y + 210), Exit, 0.3f)
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

        public static void Play(object info) => GameGlobals.IsPaused = false;
        public void Restart(object info) => RestartFlag = true;

        public static void Exit(object info) => Environment.Exit(0);
    }
}
