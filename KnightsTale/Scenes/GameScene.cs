using SharpDX.Direct2D1;
using TiledCS;

namespace KnightsTale.Scenes
{
    public class GameScene : IScene
    {
        private List<(TiledLayer, Sprite)> Sprites;
        private Player player;
        private TileMapManager mapManager;
        private TiledMap map;
        private Dictionary<int, TiledTileset> tilesets;
        private List<Rectangle> CollisionGroup;
        private List<Door> DoorGroup;
        private List<Tablet> Tablets;
        private Camera camera;
        private readonly List<Projectile> Projectiles;
        private readonly List<Monster> Monsters;
        private readonly List<Unit> Units;
        private UserInterface UI;
        private readonly SquareGrid Grid;
        private List<Key> Keys;


        public GameScene()
        {
            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassMonster = AddMonster;
            GameGlobals.IsPaused = false;
            GameGlobals.IsOver = false;
            Sprites = new();
            CollisionGroup = new();
            DoorGroup = new();
            Projectiles = new();
            Monsters = new();
            Units = new();
            Tablets = new();
            Keys = new();
            Grid = new SquareGrid(new Vector2(16, 16), new Vector2(-96, -96), new Vector2(Globals.ScreenWidth + 192, Globals.ScreenHeight + 192));
            Globals.SoundManager.ChangeBackGroundMusic(Globals.Content.Load<SoundEffect>("Audio/Music/field_theme_2"));
        }

        public void Load()
        {
            map = new TiledMap(Globals.Content.RootDirectory + "/Map/TmxMaps/TestMap.tmx");
            tilesets = map.GetTiledTilesets(Globals.Content.RootDirectory + "/Map/mapTileSets/");
            mapManager = new TileMapManager(map, tilesets, Grid);
            camera = new Camera(Globals.Graphics.GraphicsDevice.Viewport);
            Globals.GameCamera = camera;
            DoorGroup = mapManager.GetDoorObjects();
            Tablets = mapManager.GetTabletObjects();
            Keys = mapManager.GetKeyObjects();
            Sprites = mapManager.GetTileListByGroups();
            CollisionGroup = mapManager.GetCollisionsListByGroups();
            player = new Player(Globals.Content.Load<Texture2D>("Player/knight_m_idle_anim_f0"), mapManager.GetPlayerSpawnPoint(), CollisionGroup, DoorGroup, "Player");
            UI = new(player);
            foreach (var spawnPoint in mapManager.GetMonsterSpawnPoint())
                CreateMonster(Globals.Random.Next(7), spawnPoint);
            Units.Add(player);
        }

        public virtual void Update()
        {
            if (!GameGlobals.IsOver && !GameGlobals.IsPaused)
            {
                Mouse.SetCursor(Globals.CombatCursor);
                player.Update();
                camera.Update(player.Position, map);
                foreach (var door in DoorGroup) door.Update(player, Grid);
                foreach (var tablet in Tablets) tablet.Update(player);
                foreach (var key in Keys) key.Update(player);
                for (var i = 0; i < Projectiles.Count; i++)
                {
                    Projectiles[i].Update(Units, CollisionGroup, DoorGroup);

                    if (Projectiles[i].Done)
                    {
                        Projectiles.RemoveAt(i);
                        i--;
                    }
                }

                for (var i = 0; i < Monsters.Count; i++)
                {
                    Monsters[i].Update(player, Grid);

                    if (Monsters[i].Dead)
                    {
                        Monsters.RemoveAt(i);
                        i--;
                    }
                }
            }
            else
            {
                if (UI.PauseMenu.RestartFlag || UI.DeathMenu.RestartFlag)
                    Globals.SceneManager.SwithSceneTo(new GameScene());
            }
            if (Globals.MyKeyboard.GetSinglePress("Escape") && !GameGlobals.IsOver)
            {
                GameGlobals.IsPaused = !GameGlobals.IsPaused;
                Mouse.SetCursor(Globals.BaseCursor);
            }
            if (Globals.MyKeyboard.GetSinglePress("Y")) Grid.ShowGrid = !Grid.ShowGrid;
            Grid?.Update();
            UI.Update(player);

        }

        public void Draw()
        {
            Globals.SpriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, null, null, null, camera.Transform);
            foreach (var tile in Sprites)
            {
                tile.Item2.Draw();
            }
            Grid.DrawGrid();
            Globals.SpriteBatch.End();
            Globals.SpriteBatch.Begin(SpriteSortMode.FrontToBack,
                BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, null, null, null, camera.Transform);
            player.Draw();
            foreach (var tile in Sprites.Where(x => x.Item1.name == "Columns" || x.Item1.name == "WallBase" || x.Item1.name == "WallCorners" || x.Item1.name == "AllwaysFront"))
            {
                tile.Item2.Draw();
            }
            foreach (var door in DoorGroup) { door.Draw(); }
            foreach (var monster in Monsters) { monster.Draw(); }
            foreach (var projectile in Projectiles) { projectile.Draw(); }
            foreach (var key in Keys) { key.Draw(); }
            Globals.SpriteBatch.End();
            Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            UI.Draw();
            foreach (var tablet in Tablets) { tablet.Draw(); }
            Globals.SpriteBatch.End();
        }

        public void AddProjectile(object objInfo) => Projectiles.Add((Projectile)objInfo);
        public void AddMonster(object objInfo) { Monsters.Add((Monster)objInfo); Units.Add((Monster)objInfo); }

        public void CreateMonster(int number, Vector2 spawnPoint)
        {
            if (number == 0) GameGlobals.PassMonster(new Skelet(Globals.Content.Load<Texture2D>("Monsters/BaseMonster"), spawnPoint, CollisionGroup, DoorGroup, "Monster"));
            if (number == 1) GameGlobals.PassMonster(new Wogol(Globals.Content.Load<Texture2D>("Monsters/BaseMonster"), spawnPoint, CollisionGroup, DoorGroup, "Monster"));
            if (number == 2) GameGlobals.PassMonster(new Chort(Globals.Content.Load<Texture2D>("Monsters/BaseMonster"), spawnPoint, CollisionGroup, DoorGroup, "Monster"));
            if (number == 3) GameGlobals.PassMonster(new Imp(Globals.Content.Load<Texture2D>("Monsters/BaseMonster"), spawnPoint, CollisionGroup, DoorGroup, "Monster"));
            if (number == 4) GameGlobals.PassMonster(new Muddy(Globals.Content.Load<Texture2D>("Monsters/BaseMonster"), spawnPoint, CollisionGroup, DoorGroup, "Monster"));
            if (number == 5) GameGlobals.PassMonster(new Shaman(Globals.Content.Load<Texture2D>("Monsters/BaseMonster"), spawnPoint, CollisionGroup, DoorGroup, "Monster"));
            if (number == 6) GameGlobals.PassMonster(new Necromancer(Globals.Content.Load<Texture2D>("Monsters/BaseMonster"), spawnPoint, CollisionGroup, DoorGroup, "Monster"));
        }
    }
}