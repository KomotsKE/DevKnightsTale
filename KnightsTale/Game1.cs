namespace KnightsTale
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SceneManager sceneManager;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Globals.WindowSize = new(1920, 1080);
            _graphics.PreferredBackBufferWidth = Globals.WindowSize.X;
            _graphics.PreferredBackBufferHeight = Globals.WindowSize.Y;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            Globals.Content = Content;
            Globals.Mouse = new();
            Globals.MyKeyboard = new();
            Globals.DeepCoef = 0.001f;
            sceneManager = new();

            Globals.CombatCursor = MouseCursor.FromTexture2D(Globals.Content.Load<Texture2D>("Cursors/Scope"), 16, 16);
            Globals.BaseCursor = MouseCursor.FromTexture2D(Globals.Content.Load<Texture2D>("Cursors/BaseCursor"), 0, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = _spriteBatch;
            Globals.Graphics = _graphics;

            Globals.SceneManager = sceneManager;
            Globals.SoundManager = new SoundManager(null);

            Globals.SceneManager.AddScene(new GameScene());
            Globals.SceneManager.AddScene(new CutsceneFirst());
            Globals.SceneManager.AddScene(new MainMenu(NextScene, ExitGame));

            Globals.SpriteBatch.LoadPixel();
            LoadSounds();
        }

        protected override void Update(GameTime gameTime)
        {

            Globals.GameTime = gameTime;
            Globals.Mouse.Update();
            Globals.MyKeyboard.Update();

            Globals.SceneManager.GetCurrentScene().Update();

            Globals.Mouse.UpdateOld();
            Globals.MyKeyboard.UpdateOld();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(34, 34, 34));
            Globals.SceneManager.GetCurrentScene().Draw();
            base.Draw(gameTime);
        }

        public void ExitGame(object Info)
        {
            Exit();
        }

        public static void NextScene(object Info)
        {
            if (Globals.SceneManager.sceneStack.Count > 1)
            {
                Globals.SceneManager.NextScene();
            }
        }

        public static void LoadSounds()
        {
            Globals.SoundManager.AddSound("Shoot", 0.25f, Globals.Content.Load<SoundEffect>("Audio/Sounds/Crossbow_shoot2"));
            Globals.SoundManager.AddSound("Loading", 0.25f, Globals.Content.Load<SoundEffect>("Audio/Sounds/Crossbow_loading_middle1"));
            Globals.SoundManager.AddSound("LoadingEnd", 1f, Globals.Content.Load<SoundEffect>("Audio/Sounds/Crossbow_loading_end_1"));
            Globals.SoundManager.AddSound("UIClick", 0.25f, Globals.Content.Load<SoundEffect>("Audio/Sounds/Wood Block3"));
            Globals.SoundManager.AddSound("UIHover", 0.25f, Globals.Content.Load<SoundEffect>("Audio/Sounds/Coffee1"));
            Globals.SoundManager.AddSound("Open", 0.25f, Globals.Content.Load<SoundEffect>("Audio/Sounds/05_door_open_1"));
            Globals.SoundManager.AddSound("Close", 0.25f, Globals.Content.Load<SoundEffect>("Audio/Sounds/06_door_close_1"));
        }
    }
}