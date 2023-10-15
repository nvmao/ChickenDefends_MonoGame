using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Tower.Objs;
using TiledSharp;
using System.Diagnostics;

namespace Tower.Levels
{
	public class Level1 : Level
	{
		TmxMap map;
		Texture2D tileset;

		int tileWidth;
		int tileHeight;
		int tilesetTilesWide;
		int tilesetTilesHigh;

		int[,] grid;
		int ROWS = 45;
		int COLS = 80;

		List<Node> path;



		public Level1()
		{
			gameObjects.Clear();

			map = new TmxMap("./Content/map.tmx");
			tileset = Utils.get().content.Load<Texture2D>("tiles");

			tileWidth = map.Tilesets[0].TileWidth;
			tileHeight = map.Tilesets[0].TileHeight;

			tilesetTilesWide = tileset.Width / tileWidth;
			tilesetTilesHigh = tileset.Height / tileHeight;

			grid = new int[ROWS, COLS];


			//dirt = new GameObject(new Vector2(0,0), Utils.get().content.Load<Texture2D>("Background"),new Rectangle(0,0,800,600));

			for (int layer = 0; layer < map.Layers.Count; layer++)
            {
                if (map.Layers[layer].Name == "plant")
                {
                    for (var i = 0; i < map.Layers[layer].Tiles.Count; i++)
                    {

                        int gid = map.Layers[layer].Tiles[i].Gid;

                        // Empty tile, do nothing
                        if (gid != 0)
                        {
                            int tileFrame = gid - 1;
                            int column = tileFrame % tilesetTilesWide;
                            int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                            float x = (i % map.Width) * map.TileWidth;
                            float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                            Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);


							grid[(int)y/16, (int)x/16] = 2;

						}
                    }
                }

            }

			Random random = new Random();
			path = new PathGenerator(grid).getRandomPath();
			foreach(Node node in path)
            {
				grid[node.row, node.col] = 1;
            }

			Texture2D dirt = Utils.get().content.Load<Texture2D>("dirt");
			for (int row = 0; row < ROWS; row++)
            {
				for (int col = 0; col < COLS; col++)
                {
                    if (grid[row,col] == 1)
                    {
						gameObjects.Add(new GameObject(new Vector2(col*16, row * 16),dirt, new Rectangle(0, 0, 16, 16)));
					}
                }
            }

			Utils.get().path = path;


		}

		public override void draw()
        {
			for (int layer = 0; layer < map.Layers.Count; layer++)
            {
				for (var i = 0; i < map.Layers[layer].Tiles.Count; i++)
				{
					int gid = map.Layers[layer].Tiles[i].Gid;

					if (gid != 0)
					{
						int tileFrame = gid - 1;
						int column = tileFrame % tilesetTilesWide;
						int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

						float x = (i % map.Width) * map.TileWidth;
						float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

						Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

						Utils.get().batch.Draw(tileset, new Rectangle((int)(x - Utils.get().camera.X), (int)(y - Utils.get().camera.Y), tileWidth, tileHeight), tilesetRec, Color.White);

					}
				}
			}

		}


		public override void drawUI()
		{
			
		}

		public override MTower getTower1(Rectangle rect, List<GameObject> gameObjects,int tower)
		{
			if(rect.X < 16 * 3 || rect.X > (80-3) * 16 || rect.Y < 16 * 3 || rect.Y > (45-3) * 16)
            {
				return null;
            }

			foreach(Node n in path){
				Rectangle nPathRect = new Rectangle(n.col * 16, n.row * 16, 16, 16);
                if (nPathRect.Intersects(rect))
                {
					return null;
                }
            }
			foreach(GameObject g in gameObjects)
            {
                if (g.getRect().Intersects(rect))
                {
					return null;
                }
            }

			if(tower == 1)
            {
				return new Tower1(new Vector2(rect.X, rect.Y));
			}

			return new Tower2(new Vector2(rect.X, rect.Y));
        }

	}

}
