using KnightsTale.Sprites;
using SharpDX.XAudio2;
using System.Collections.Generic;
using System.Linq;
using TiledCS;

namespace KnightsTale.Managers
{
    public class TileMapManager
    {
        private TiledMap map;
        private Dictionary<int, TiledTileset> tilesets;
        private ContentManager content;
        public float cons_depth_y { get { return 1f/map.Height; } }
        public float cons_depth_x { get { return 0.00001f / map.Width; } }
        
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

        public List<Rectangle> GetObjectList()
        {
            var list = new List<Rectangle>();
            var ObjectLayers = map.Layers.Where(layer => layer.type == TiledLayerType.ObjectLayer);
            var Collisions = ObjectLayers.First(layer => layer.name == ("Collisions"));
            foreach (var obj in Collisions.objects) { list.Add(new Rectangle((int)obj.x + 100, (int)obj.y + 100, (int)obj.width, (int)obj.height)); }
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
                    var destination = new Rectangle(tileX, tileY, map.TileWidth, map.TileHeight);

                    list.Add((layer, new Tile(content.Load<Texture2D>("map/SpriteSheets/" + tileset.Name), position + new Vector2(100, 100), tileset.TileWidth, tileset.TileHeight, source, (int)(destination.Y * Globals.deepthcof))));
                }
            }
        }
    }
}
