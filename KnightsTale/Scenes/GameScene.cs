using KnightsTale.Managers;
using KnightsTale.Models;
using KnightsTale.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
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
        private Dictionary<int, TiledTileset> tilesets;
        private List<Rectangle> CollisionGroup;
        private Matrix translation;

        public GameScene(ContentManager contentManager, SceneManager sceneManager, GraphicsDeviceManager graphics)
        {
            this.Content = contentManager;
            this.sceneManager = sceneManager;
            _graphics = graphics;
            sprites = new List<Sprite>();
            camera = new(Vector2.Zero);
            CollisionGroup = new List<Rectangle>();
            player = new Player(Content.Load<Texture2D>("Player/knight_m_idle_anim_f0"), new Vector2(300, 300), CollisionGroup);
        }

        public void Load()
        {
            map = new TiledMap(Content.RootDirectory + "/Map/map.tmx");
            tilesets = map.GetTiledTilesets(Content.RootDirectory + "/Map/");
            mapManager = new TileMapManager(map, tilesets,Content);
            foreach (var tile in mapManager.GetTileList())
            {
                sprites.Add(tile);
            }
            sprites.Add(player);
            foreach (var Collision in mapManager.GetObjectList())
            {
                CollisionGroup.Add(Collision);
            }
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
