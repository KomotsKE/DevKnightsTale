using KnightsTale.Managers;
using KnightsTale.Scenes;

namespace KnightsTale
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
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
            _graphics.ApplyChanges();
            _graphics.ToggleFullScreen();

            Globals.Content = Content;
            sceneManager = new();
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = _spriteBatch;
            Globals.deepthcof = 21f;

            // TODO: use this.Content to load your game content here
            sceneManager.AddScene(new GameScene(Content, sceneManager, _graphics));
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.Update(gameTime);
            sceneManager.GetCurrentScene().Update(gameTime);


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            //_spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            sceneManager.GetCurrentScene().Draw();

            //_spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}