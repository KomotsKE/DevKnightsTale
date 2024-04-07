using KnightsTale.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace KnightsTale.Managers
{

    public class TileMapManager
    {
        public Input input = new Input() { Up = Keys.W, Down = Keys.S, Left = Keys.A, Right = Keys.D };
        private SpriteBatch SpriteBatch;
        private TmxMap Map;
        private Rectangle bounds; 
        private Texture2D TileSet;
        private int tileSetTilesWide; private int tileWidth; private int tileHeight;
        

        public TileMapManager(SpriteBatch spriteBatch, TmxMap map, Texture2D tileset)
        {
            SpriteBatch = spriteBatch; Map = map; TileSet = tileset;
            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;
            tileSetTilesWide = TileSet.Width / tileWidth;
            bounds = new Rectangle(0, 0, 0, 0);
        }

        public void Draw()
        {
            CameraMove();
            for (var i = 0; i < Map.Layers.Count; i++) 
            { 
                for (var j = 0; j < Map.Layers[i].Tiles.Count; j++)
                {
                    int gid = Map.Layers[i].Tiles[j].Gid;
                    if (gid == 0) { }
                    else
                    {
                        int tileFrame = gid - 1;
                        int column = tileFrame % tileSetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tileSetTilesWide);
                        float x = (j % Map.Width) * Map.TileWidth;
                        float y = (float)Math.Floor(j / (double)Map.Width) * Map.TileHeight;
                        Rectangle newView = new Rectangle((int)x - bounds.X, (int)y - bounds.Y, tileWidth, tileHeight);
                        Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);
                        SpriteBatch.Draw(TileSet, newView, tilesetRec, Color.White);
                    }
                }
            }
        }

        private void CameraMove()
        {
            int scrollx = 0, scrolly = 0;

            if (Keyboard.GetState().IsKeyDown(input.Left))
                scrollx = 10;
            if (Keyboard.GetState().IsKeyDown(input.Right))
                scrollx = -10;
            if (Keyboard.GetState().IsKeyDown(input.Up))
                scrolly = 10;
            if (Keyboard.GetState().IsKeyDown(input.Down))
                scrolly = -10;

            bounds.X += scrollx;
            bounds.Y += scrolly;
        }
    }
}
