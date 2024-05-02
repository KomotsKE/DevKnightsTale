using KnightsTale.Managers;
using KnightsTale.Models;
using KnightsTale.Objects;
using KnightsTale.Sprites;
using System.Collections.Generic;
using System.Linq;
using TiledCS;

namespace KnightsTale.Scenes
{
    public class GameScene : IScene
    {
        private List<(TiledLayer, Sprite)> sprites;
        private readonly GraphicsDeviceManager graphics;
        private readonly ContentManager Content;
        private SceneManager sceneManager;
        private Player player;
        private TileMapManager mapManager;
        private TiledMap map;
        private Dictionary<int, TiledTileset> tilesets;
        private List<Rectangle> CollisionGroup;
        private Camera camera;
        private List<Door> objects;

        public GameScene(ContentManager contentManager, SceneManager sceneManager, GraphicsDeviceManager graphics)
        {
            this.Content = contentManager;
            this.sceneManager = sceneManager;
            this.graphics = graphics;
            sprites = new();
            CollisionGroup = new();
        }
        
        public void Load()
        {
            map = new TiledMap(Content.RootDirectory + "/Map/TmxMaps/TestMap.tmx");
            tilesets = map.GetTiledTilesets(Content.RootDirectory + "/Map/mapTileSets/");
            mapManager = new TileMapManager(map, tilesets,Content);
            camera = new Camera(graphics.GraphicsDevice.Viewport);
            Globals.GameCamera = camera;
            player = new Player(Content.Load<Texture2D>("Player/knight_m_idle_anim_f0"), new Vector2(300, 300), CollisionGroup, mapManager.GetPlayerSpawnPoint());
            player.Load();
            objects = mapManager.GetDoorObjects();
            foreach (var obj in objects)
            {
                obj.Load();
            }
            sprites = mapManager.GetTileListByGroups();
            CollisionGroup = mapManager.GetCollisionsListByGroups();
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            camera.Update(player.position);
            foreach (var obj in objects)
            {
                obj.Update(player.position);
            }
        }

        public void Draw()
        {
            Globals.SpriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, null, null, null, camera.Transform);
            foreach (var tile in sprites.Where(x => x.Item1.name != "Columns"))
            {
                tile.Item2.Draw();
            }
            Globals.SpriteBatch.End();
            Globals.SpriteBatch.Begin(SpriteSortMode.FrontToBack,
                BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, null, null, null, camera.Transform);
            player.Draw();
            foreach (var tile in sprites.Where(x => x.Item1.name == "Columns"))
            {
                tile.Item2.Draw();
            }
            foreach (var door in objects)
            {
                door.Draw();
            }
            Globals.SpriteBatch.End();
        }
    }
}