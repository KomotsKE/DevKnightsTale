using KnightsTale.Objects;
using KnightsTale.Sprites;
using TiledCS;

namespace KnightsTale.Managers
{
    public class TileMapManager
    {
        private readonly TiledMap map;
        private readonly Dictionary<int, TiledTileset> tilesets;
        private readonly ContentManager content;
        
        public TileMapManager(TiledMap map, Dictionary<int, TiledTileset> tilesets, ContentManager content)
        {
            this.map = map;
            this.tilesets = tilesets;
            this.content = content;
        }

        public List<(TiledLayer,Sprite)> GetTileListByGroups()
        {
            var list = new List<(TiledLayer, Sprite)>();
            foreach (var group in map.Groups)
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
            var tileLayers = map.Layers.Where(layer => layer.type == TiledLayerType.TileLayer);
            foreach (var layer in tileLayers)
            {
                GetTIleInfo(layer, list);
            }
            return list;
        }

        public List<Rectangle> GetCollisionsListByLayers()
        {
            var list = new List<Rectangle>();
            var ObjectLayers = map.Layers.Where(layer => layer.type == TiledLayerType.ObjectLayer);
            var Collisions = ObjectLayers.First(layer => layer.name == ("Collisions"));
            foreach (var obj in Collisions.objects) { list.Add(new Rectangle((int)obj.x, (int)obj.y, (int)obj.width, (int)obj.height)); }
            return list;
        }

        public List<Rectangle> GetCollisionsListByGroups()
        {
            List<Rectangle> list = new();
            var Collisions = map.Groups.SelectMany(group => group.layers)
                .Where(layer => layer.type == TiledLayerType.ObjectLayer).First(layer => layer.name == "Collisions").objects;
            foreach (var obj in Collisions) { list.Add(new Rectangle((int)obj.x, (int)obj.y, (int)obj.width, (int)obj.height)); }
            return list;
        }

        public List<Door> GetDoorObjects()
        {
            List<Door> list = new();
            var Doors = map.Groups.SelectMany(group => group.layers)
                .Where(layer => layer.type == TiledLayerType.ObjectLayer).First(layer => layer.name == "Doors").objects;
            foreach (var obj in Doors)
            {
                list.Add(new Door(new Rectangle((int)(obj.x + obj.width/2) , (int)(obj.y - obj.height/2), (int)obj.width, (int)obj.height)));
            }

            return list;
        }

        public Vector2 GetPlayerSpawnPoint()
        {
            var objectLayers = map.Groups.SelectMany(group => group.layers)
            .Where(layer => layer.type == TiledLayerType.ObjectLayer);
            foreach (var layer in objectLayers)
            {
                foreach (var obj in layer.objects)
                {
                    if (obj.name == "spawnPoint") { return new Vector2(obj.x,obj.y); }
                }
            }
            return Vector2.Zero;
        }

        public List<TileObject> GetObjects()
        {
            List<TileObject> list = new();
            var objectLayers = map.Groups.SelectMany(group => group.layers)
                .Where(layer => layer.type == TiledLayerType.ObjectLayer).Where(layer => layer.name != "Collisions");
            foreach (var layer in objectLayers)
            {
                if (layer.name == "Doors")
                {
                    foreach (var obj in layer.objects)
                    {
                        list.Add(new Door(new Rectangle((int)(obj.x + obj.width / 2), (int)(obj.y - obj.height / 2), (int)obj.width, (int)obj.height)));
                    }
                }
                else if (layer.name == "Objects")
                {

                }
            }
            return list;

        }
        public void GetTIleInfo(TiledLayer layer, List<(TiledLayer, Sprite)> list)
        {
            for (var y = 0; y < layer.height; y++)
            {
                for (var x = 0; x < layer.width; x++)
                {
                    var index = (y * layer.width) + x;
                    var gid = layer.data[index];
                    var tileX = x * map.TileWidth;
                    var tileY = y * map.TileHeight;
                    var position = new Vector2(tileX, tileY);

                    if (gid == 0) { continue; }
                    var mapTileSet = map.GetTiledMapTileset(gid);
                    var tileset = tilesets[mapTileSet.firstgid];
                    var rect = map.GetSourceRect(mapTileSet, tileset, gid);

                    var source = new Rectangle(rect.x, rect.y, rect.width, rect.height);
                    var destination = new Rectangle(tileX, tileY, tileset.TileWidth, tileset.TileHeight);
                    float offset;
                    if (layer.name == "Columns") offset = 16f;
                    else if (layer.name == "WallCorners") offset = -16f;
                    else offset = 0f;
                    var depth = (destination.Bottom - offset) * Globals.DeepCoef;

                    list.Add((layer, new Tile(content.Load<Texture2D>("map/SpriteSheets/" + tileset.Name), position, tileset.TileWidth, tileset.TileHeight, source,depth)));
                }
            }
        }
    }
}
