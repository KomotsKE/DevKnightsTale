using KnightsTale.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledCS;

namespace KnightsTale.Managers
{
    public class TileMapManager
    {
        private TiledMap map;
        private Dictionary<int, TiledTileset> tilesets;
        private Texture2D tilesetTexture;

        public TileMapManager(TiledMap map, Dictionary<int, TiledTileset> tilesets, Texture2D tilesetTexture)
        {
            this.map = map;
            this.tilesets = tilesets;
            this.tilesetTexture = tilesetTexture;
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

                        list.Add(new Tile(tilesetTexture, position, map.TileWidth, map.TileHeight, source));
                    }
                }
            }
            return list;
        }
    }
}
