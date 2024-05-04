using KnightsTale.Managers;
using KnightsTale.Models;
using KnightsTale.Objects;
using KnightsTale.Sprites;
using KnightsTale.Sprites.Units;
using SharpDX.Direct2D1.Effects;
using TiledCS;

namespace KnightsTale.Scenes
{
    public class GameScene : IScene
    {
        private List<(TiledLayer, Sprite)> Sprites;
        private readonly GraphicsDeviceManager graphics;
        private readonly ContentManager Content;
        private SceneManager sceneManager;
        private Player player;
        private TileMapManager mapManager;
        private TiledMap map;
        private Dictionary<int, TiledTileset> tilesets;
        private List<Rectangle> CollisionGroup;
        private List<Door> DoorCollisionGroup;
        private Camera camera;
        private List<Door> objects;
        private List<Projectile> Projectiles;

        public GameScene(ContentManager contentManager, SceneManager sceneManager, GraphicsDeviceManager graphics)
        {
            this.Content = contentManager;
            this.sceneManager = sceneManager;
            this.graphics = graphics;
            GameGlobals.PassProjectile = AddProjectile;
            Sprites = new();
            CollisionGroup = new();
            DoorCollisionGroup = new();
            Projectiles = new();
        }
        
        public void Load()
        {
            map = new TiledMap(Content.RootDirectory + "/Map/TmxMaps/TestMap.tmx");
            tilesets = map.GetTiledTilesets(Content.RootDirectory + "/Map/mapTileSets/");
            mapManager = new TileMapManager(map, tilesets,Content);
            camera = new Camera(graphics.GraphicsDevice.Viewport);
            Globals.GameCamera = camera;
            objects = mapManager.GetDoorObjects();
            foreach (var obj in objects)
            {
                obj.Load();
                DoorCollisionGroup.Add(obj);
            }
            Sprites = mapManager.GetTileListByGroups();
            CollisionGroup = mapManager.GetCollisionsListByGroups();
            player = new Player(Content.Load<Texture2D>("Player/knight_m_idle_anim_f0"), new Vector2(300, 300), CollisionGroup, DoorCollisionGroup, mapManager.GetPlayerSpawnPoint());
            player.Load();

        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            camera.Update(player.position);
            foreach (var obj in objects)
            {
                obj.Update(player.position);
            }
            for (var i = 0; i < Projectiles.Count; i++)
            {
                Projectiles[i].Update(null);

                if (Projectiles[i].done)
                {
                    Projectiles.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw()
        {
            Globals.SpriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, null, null, null, camera.Transform);
            foreach (var tile in Sprites.Where(x => x.Item1.name != "Columns"))
            {
                tile.Item2.Draw();
            }
            Globals.SpriteBatch.End();
            Globals.SpriteBatch.Begin(SpriteSortMode.FrontToBack,
                BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, null, null, null, camera.Transform);
            player.Draw();
            foreach (var tile in Sprites.Where(x => x.Item1.name == "Columns" || x.Item1.name == "WallBase" || x.Item1.name == "WallCorners"))
            {
                tile.Item2.Draw();
            }
            foreach (var door in objects)
            {
                door.Draw();
            }
            foreach (var projectile in Projectiles) { projectile.Draw(); }
            Globals.SpriteBatch.End();
        }

        public void AddProjectile(object objInfo) { Projectiles.Add((Projectile)objInfo); }
    }
}