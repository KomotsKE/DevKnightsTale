using KnightsTale.Grids;
using KnightsTale.Objects;
using KnightsTale.Sprites;
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
            var Collisions = Map.Groups.SelectMany(group => group.layers)
                .Where(layer => layer.type == TiledLayerType.ObjectLayer).First(layer => layer.name == "Collisions").objects;
            foreach (var obj in Collisions) { list.Add(new Rectangle((int)obj.x, (int)obj.y, (int)obj.width, (int)obj.height)); }
            return list;
        }

        public List<Door> GetDoorObjects()
        {
            List<Door> list = new();
            var Doors = Map.Groups.SelectMany(group => group.layers)
                .Where(layer => layer.type == TiledLayerType.ObjectLayer).First(layer => layer.name == "Doors").objects;
            foreach (var obj in Doors)
            {
                list.Add(new Door(new Rectangle((int)(obj.x + obj.width / 2), (int)(obj.y - obj.height / 2), (int)obj.width, (int)obj.height),Grid));
            }

            return list;
        }

        public Vector2 GetPlayerSpawnPoint()
        {
            var layer = Map.Groups.SelectMany(group => group.layers)
                .Where(layer => layer.name == "Objects")
                .First();
            foreach (var obj in layer.objects)
            {
                if (obj.name == "playerSpawnPoint") { return new Vector2(obj.x, obj.y); }
            }
            return Vector2.Zero;
        }

        public List<Vector2> GetMonsterSpawnPoint()
        {
            var result = new List<Vector2>();
            var layer = Map.Groups.SelectMany(group => group.layers)
                .Where(layer => layer.name == "Objects")
                .First();
            foreach (var obj in layer.objects)
            {
                if (obj.name == "monsterSpawnPoint") { result.Add(new Vector2(obj.x, obj.y)); }
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
                    if (layer.name == "Columns") { offset = 48f; Grid.slots[(int)positionOnGrid.X][(int)positionOnGrid.Y].SetToFilled(true); position.Y -= 32; }
                    else if (layer.name == "WallBase") { offset = 0; Grid.slots[(int)positionOnGrid.X][(int)positionOnGrid.Y].SetToFilled(true); }
                    else if (layer.name == "WallCorners") offset = -16f;
                    else offset = 0;
                    var depth = (destination.Bottom - offset) * Globals.DeepCoef;
                    if (layer.name == "AllwaysFront") { depth = 0; }

                    list.Add((layer, new Tile(Globals.Content.Load<Texture2D>("map/SpriteSheets/" + tileset.Name), position, tileset.TileWidth, tileset.TileHeight, source, depth)));
                }
            }
        }
    }
}
