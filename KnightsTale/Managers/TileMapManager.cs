using KnightsTale.Objects.Flasks;
using System.Security.Policy;
using System.Windows.Forms;
using TiledCS;

namespace KnightsTale.Managers
{
    public class TileMapManager
    {
        private readonly TiledMap Map;
        private readonly Dictionary<int, TiledTileset> Tilesets;
        private readonly SquareGrid Grid;

        public TileMapManager(TiledMap map, Dictionary<int, TiledTileset> tilesets, SquareGrid grid)
        {
            Map = map;
            Tilesets = tilesets;
            Grid = grid;
        }

        public List<(TiledLayer, Sprite)> GetTileListByGroups()
        {
            var list = new List<(TiledLayer, Sprite)>();
            foreach (var group in Map.Groups)
            {
                var tileLayers = group.layers.Where(layer => layer.type == TiledLayerType.TileLayer);
                foreach (var layer in tileLayers)
                {
                    GetTIleInfo(layer, list);
                }
            }
            return list;
        }

        public List<(TiledLayer, Sprite)> GetTileListByLayers()
        {
            var list = new List<(TiledLayer, Sprite)>();
            var tileLayers = Map.Layers.Where(layer => layer.type == TiledLayerType.TileLayer);
            foreach (var layer in tileLayers)
            {
                GetTIleInfo(layer, list);
            }
            return list;
        }

        public List<Rectangle> GetCollisionsListByLayers()
        {
            var list = new List<Rectangle>();
            var ObjectLayers = Map.Layers.Where(layer => layer.type == TiledLayerType.ObjectLayer);
            var Collisions = ObjectLayers.First(layer => layer.name == ("Collisions"));
            foreach (var obj in Collisions.objects) { list.Add(new Rectangle((int)obj.x, (int)obj.y, (int)obj.width, (int)obj.height)); }
            return list;
        }

        public List<Rectangle> GetCollisionsListByGroups()
        {
            List<Rectangle> list = new();
            var Collisions = Map.Groups.SelectMany(group => group.layers).First(layer => layer.name == "Collisions").objects;
            foreach (var obj in Collisions) { list.Add(new Rectangle((int)obj.x, (int)obj.y, (int)obj.width, (int)obj.height)); }
            return list;
        }

        public List<Door> GetDoorObjects()
        {
            List<Door> list = new();
            var Doors = Map.Groups.SelectMany(group => group.layers).First(layer => layer.name == "Doors").objects;
            foreach (var obj in Doors)
            {
                list.Add(new Door(new Rectangle((int)(obj.x + obj.width / 2), (int)(obj.y - obj.height / 2), (int)obj.width, (int)obj.height),obj.properties.First(x => x.name == "Key").value));
            }

            return list;
        }

        public List<Tablet> GetTabletObjects()
        {
            List<Tablet> list = new();

            var Objects = Map.Groups.SelectMany(group => group.layers).First(layer => layer.name == "Objects").objects;

            foreach (var obj in Objects)
            {
                if (obj.type == "Tablet")
                { list.Add(new Tablet(new Rectangle((int)obj.x, (int)obj.y, 16, 16), obj.properties.First(x => x.name == "Text").value)); }
            }
            
            return list;
        }

        public List<Key> GetKeyObjects()
        {
            List<Key> list = new();
            var Objects = Map.Groups.SelectMany(group => group.layers).First(layer => layer.name == "Objects").objects;
            foreach (var obj in Objects)
            {
                if (obj.type == "Keys")
                { list.Add(new Key(new Rectangle((int)obj.x, (int)obj.y, 16, 16), obj.properties.First(x => x.name == "Name").value)); }
            }
            return list;
        }

        public List<TileObject> GetTiledObjects() 
        {
            List<TileObject> list = new();
            var Objects = Map.Groups.SelectMany(group => group.layers).First(layer => layer.name == "Objects").objects;
            foreach (var obj in Objects)
            {
                if (obj.type == "Keys")
                    list.Add(new Key(new Rectangle((int)obj.x, (int)obj.y, 16, 16), obj.properties.First(x => x.name == "Name").value));
                if (obj.type == "Tablet")
                    list.Add(new Tablet(new Rectangle((int)obj.x, (int)obj.y, 16, 16), obj.properties.First(x => x.name == "Text").value)); 
                if (obj.type == "BigFlask")
                    list.Add(new BigHealFlask(new Rectangle((int)obj.x, (int)obj.y, 16, 16)));
                if (obj.type == "SmallFlask")
                    list.Add(new SmallFlask(new Rectangle((int)obj.x, (int)obj.y, 16, 16)));
                if (obj.type == "HealthFlask")
                    list.Add(new SmallFlask(new Rectangle((int)obj.x, (int)obj.y, 16, 16)));
                if (obj.type == "StaminaFlask")
                    list.Add(new StaminaFlask(new Rectangle((int)obj.x, (int)obj.y, 16, 16)));

            }
            return list;
        }

        public Vector2 GetPlayerSpawnPoint()
        {
            var SpawnPoints = Map.Groups.SelectMany(group => group.layers).First(layer => layer.name == "Objects").objects;

            foreach (var obj in SpawnPoints)
            {
                if (obj.name == "playerSpawnPoint") { return new Vector2(obj.x, obj.y); }
            }
            return Vector2.Zero;
        }

        public List<Vector2> GetMonsterSpawnPoint()
        {
            var result = new List<Vector2>();
            var layer = Map.Groups.SelectMany(group => group.layers)
                .First(layer => layer.name == "Objects");
            foreach (var obj in layer.objects)
            {
                if (obj.type == "MonsterSpawnPoint") { result.Add(new Vector2(obj.x, obj.y)); }
            }
            return result;
        }

        public void GetTIleInfo(TiledLayer layer, List<(TiledLayer, Sprite)> list)
        {
            for (var y = 0; y < layer.height; y++)
            {
                for (var x = 0; x < layer.width; x++)
                {
                    var index = (y * layer.width) + x;
                    var gid = layer.data[index];
                    var tileX = x * Map.TileWidth;
                    var tileY = y * Map.TileHeight;
                    var position = new Vector2(tileX, tileY);

                    if (gid == 0) { continue; }
                    var mapTileSet = Map.GetTiledMapTileset(gid);
                    var tileset = Tilesets[mapTileSet.firstgid];

                    var rect = Map.GetSourceRect(mapTileSet, tileset, gid);
                    var source = new Rectangle(rect.x, rect.y, rect.width, rect.height);
                    var destination = new Rectangle(tileX, tileY, tileset.TileWidth, tileset.TileHeight);

                    float offset;
                    var positionOnGrid = Grid.GetSlotFromPixel(position);
                    if (layer.name == "Columns") { offset = 48f; Grid.slots[(int)positionOnGrid.X][(int)positionOnGrid.Y].SwitchGridImassability(true); position.Y -= 32; }
                    else if (layer.name == "WallBase") { offset = 0; Grid.slots[(int)positionOnGrid.X][(int)positionOnGrid.Y].SwitchGridImassability(true); }
                    else if (layer.name == "WallCorners") offset = -16f;
                    else offset = 0;
                    var depth = (destination.Bottom - offset) * Globals.DeepCoef;

                    if (layer.name == "AllwaysFront") { depth = 1; }
                    if (layer.name == "InvisibleWallForMonster" || layer.name == "WallBase") 
                        { Grid.slots[(int)positionOnGrid.X][(int)positionOnGrid.Y].SwitchGridImassability(true); }
                  
                    list.Add((layer, new Tile(Globals.Content.Load<Texture2D>("map/SpriteSheets/" + tileset.Name), 
                        position, tileset.TileWidth, tileset.TileHeight, source, depth)));
                }
            }
        }
    }
}
