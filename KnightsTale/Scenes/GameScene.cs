using KnightsTale.Managers;
using KnightsTale.Models;
using KnightsTale.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TiledCS;

namespace KnightsTale.Scenes
{
    public class GameScene : IScene
    {
        private List<Sprite> sprites;
        private GraphicsDeviceManager _graphics;
        private ContentManager Content;
        private SceneManager sceneManager;
        private FollowCamera camera;
        private Player player;
        private TileMapManager mapManager;
        private TiledMap map;
        private Texture2D tilesetTexture;
        private Dictionary<int, TiledTileset> tilesets;

        public GameScene(ContentManager contentManager, SceneManager sceneManager, GraphicsDeviceManager graphics)
        {
            this.Content = contentManager;
            this.sceneManager = sceneManager;
            _graphics = graphics;
            sprites = new List<Sprite>();
            camera = new(Vector2.Zero);
            player = new Player(Content.Load<Texture2D>("Player/knight_m_idle_anim_f0"), new Vector2(300, 300), sprites);
        }

        public void Load()
        {
            map = new TiledMap(Content.RootDirectory + "/Map/map.tmx");
            tilesets = map.GetTiledTilesets(Content.RootDirectory + "/Map/");
            tilesetTexture = Content.Load<Texture2D>("dungeontileset-extended");
            mapManager = new TileMapManager(map, tilesets,tilesetTexture);
            var list = mapManager.GetTileList();
            foreach (var tile in list)
            {
                sprites.Add(tile);
            }
            sprites.Add(new Sprite(Content.Load<Texture2D>("Map/floor_1"), new Vector2(100, 100)));
            sprites.Add(new Sprite(Content.Load<Texture2D>("Map/wall_mid"), new Vector2(200, 200)));
            sprites.Add(player);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var sprite in sprites)
            {
                sprite.Update(gameTime);
            }
            camera.Follow(player.rectangle, new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            camera.Draw(spriteBatch, sprites);
        }
    }
}
