using KnightsTale.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

        public TileMapManager(TiledMap map, Dictionary<int, TiledTileset> tilesets, ContentManager content)
        {
            this.map = map;
            this.tilesets = tilesets;
            this.content = content;
        }

        public List<Tile> GetTileList()
        {
            var list = new List<Tile>();
            var tileLayers = map.Layers.Where(layer => layer.type == TiledLayerType.TileLayer);
            foreach (var layer in tileLayers)
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

                        list.Add(new Tile(content.Load<Texture2D>("map/"+tileset.Name), position, map.TileWidth, map.TileHeight, source));
                    }
                }
            }
            return list;
        }
        public List<Rectangle> GetObjectList()
        {
            var list = new List<Rectangle>();
            var ObjectLayers = map.Layers.Where(layer => layer.type == TiledLayerType.ObjectLayer);
            var Collisions = ObjectLayers.First(layer => layer.name == ("Collisions"));
            foreach (var obj in Collisions.objects) { list.Add(new Rectangle((int)obj.x, (int)obj.y, (int)obj.width, (int)obj.height)); }
            return list;
        }
    }
}
