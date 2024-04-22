using KnightsTale.Managers;
using KnightsTale.Models;
using KnightsTale.Sprites;
using System.Collections.Generic;
using System.Linq;
using TiledCS;

namespace KnightsTale.Scenes
{
    public class GameScene : IScene
    {
        private List<(TiledLayer, Sprite)> sprites;
        private GraphicsDeviceManager graphics;
        private ContentManager Content;
        private SceneManager sceneManager;
        private Player player;
        private TileMapManager mapManager;
        private TiledMap map;
        private Dictionary<int, TiledTileset> tilesets;
        private List<Rectangle> CollisionGroup;
        private Camera camera;

        public GameScene(ContentManager contentManager, SceneManager sceneManager, GraphicsDeviceManager graphics)
        {
            this.Content = contentManager;
            this.sceneManager = sceneManager;
            this.graphics = graphics;
            sprites = new();
            CollisionGroup = new();
            player = new Player(Content.Load<Texture2D>("Player/knight_m_idle_anim_f0"), new Vector2(300, 300), CollisionGroup);
        }
        
        public void Load()
        {
            map = new TiledMap(Content.RootDirectory + "/Map/TmxMaps/TestMap.tmx");
            tilesets = map.GetTiledTilesets(Content.RootDirectory + "/Map/mapTileSets/");
            mapManager = new TileMapManager(map, tilesets,Content);
            camera = new Camera(graphics.GraphicsDevice.Viewport);
            player.Load();
            foreach (var tile in mapManager.GetTileListByGroups())
            {
                sprites.Add(tile);
            }
            foreach (var Collision in mapManager.GetObjectList())
            {
                CollisionGroup.Add(Collision);
            }
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            camera.Update(player.position);
        }

        public void Draw()
        {
            Globals.SpriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, null, null, null, camera.Transform);
            foreach (var tile in sprites.Where(x => x.Item1.name != "Walls"))
            {
                tile.Item2.Draw();
            }
            Globals.SpriteBatch.End();
            Globals.SpriteBatch.Begin(SpriteSortMode.FrontToBack,
                BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, null, null, null, camera.Transform);
            foreach (var tile in sprites.Where(x => x.Item1.name == "Walls"))
            {
                tile.Item2.Draw();
            }
            player.Draw();
            Globals.SpriteBatch.End();
        }
    }
}