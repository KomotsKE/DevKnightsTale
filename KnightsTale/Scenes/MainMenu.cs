using KnightsTale.Sprites;

namespace KnightsTale.Scenes
{
    internal class MainMenu : IScene
    {
        public Sprite BackGround;

        public PassObject PlayClick, ExitClick;

        public List<Button> Buttons;

        public MainMenu(PassObject playClick, PassObject exitClick)
        {
            PlayClick = playClick;
            ExitClick = exitClick;
        }
        public void Load()
        {
            Globals.SoundManager.ChangeBackGroundMusic(Globals.Content.Load<SoundEffect>("Audio/Music/dungeon_theme_1"));
            Mouse.SetCursor(Globals.BaseCursor);
            Buttons = new();
            var texture = Globals.Content.Load<Texture2D>("Backgrounds/MenuBackGround3");
            //float scale = Math.Min((float)Globals.ScreenWidth / text.Width, (float)Globals.ScreenHeight / text.Height);
            //int newWidth = (int)(text.Width * scale);
            //int newHeight = (int)(text.Height * scale);
            BackGround = new Sprite(texture, Vector2.Zero);
            Buttons = new()
            {
                new Button(Globals.Content.Load<Texture2D>("Buttons/Exit/Exit"),Globals.Content.Load<Texture2D>("Buttons/Exit/Exit02"),
                    Globals.Content.Load<Texture2D>("Buttons/Exit/Exit03"),new Vector2(785, 645),ExitClick,0.4f),
                new Button(Globals.Content.Load<Texture2D>("Buttons/Play/play01"),Globals.Content.Load<Texture2D>("Buttons/Play/play02"),
                    Globals.Content.Load<Texture2D>("Buttons/Play/play03"), new Vector2(785, 395), PlayClick, 0.4f),
                new Button(Globals.Content.Load<Texture2D>("Buttons/About/about01"), Globals.Content.Load<Texture2D>("Buttons/About/about02"),
                    Globals.Content.Load<Texture2D>("Buttons/About/about03"), new Vector2(785, 520), null, 0.4f)
            };

        }

        public void Update()
        {
            foreach (var button in Buttons)
            {
                button.Update();
            }
        }

        public void Draw()
        {
            Globals.SpriteBatch.Begin();
            BackGround.Draw();
            foreach (var button in Buttons)
            {
                button.Draw();
            }
            Globals.SpriteBatch.End();
        }
    }
}
