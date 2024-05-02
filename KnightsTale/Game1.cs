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
            Globals.WindowSize = new(1200, 1080);
            _graphics.PreferredBackBufferWidth = Globals.WindowSize.X;
            _graphics.PreferredBackBufferHeight = Globals.WindowSize.Y;
            _graphics.ApplyChanges();

            Globals.Content = Content;
            Globals.Mouse = new();
            Globals.MyKeyboard = new();
            sceneManager = new();

            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = _spriteBatch;
            Globals.Graphics = _graphics;

            // TODO: use this.Content to load your game content here
            Globals.SpriteBatch.LoadPixel(Globals.Graphics.GraphicsDevice);
            sceneManager.AddScene(new GameScene(Content, sceneManager, _graphics));
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Globals.Update(gameTime);
            Globals.Mouse.Update();
            Globals.MyKeyboard.Update();
            sceneManager.GetCurrentScene().Update(gameTime);
            Globals.Mouse.UpdateOld();
            Globals.MyKeyboard.UpdateOld();


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(72,59,58));

            // TODO: Add your drawing code here
            //_spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            sceneManager.GetCurrentScene().Draw();

            //_spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}