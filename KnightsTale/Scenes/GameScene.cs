using KnightsTale.Managers;
using KnightsTale.Models;
using KnightsTale.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TiledSharp;

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
        private TileMapManager tileMapManager;
        private TmxMap map;
        private SpriteBatch SpriteBatch;
        private Texture2D TileSet;

        public GameScene(ContentManager contentManager, SceneManager sceneManager, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            Content = contentManager;
            SpriteBatch = spriteBatch;
            _graphics = graphics;
            this.sceneManager = sceneManager;
        }
        public void Load()
        {
            camera = new(Vector2.Zero);
            sprites = new List<Sprite>();
            player = new Player(Content.Load<Texture2D>("Player/knight_m_idle_anim_f0"), new Vector2(300, 300), sprites);
            TileSet = Content.Load<Texture2D>("atlas_floor-16x16");
            map = new TmxMap("Content/map.tmx");
            tileMapManager = new TileMapManager(SpriteBatch, map, TileSet);
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
            camera.Draw(spriteBatch, sprites, tileMapManager);
        }
    }
}
