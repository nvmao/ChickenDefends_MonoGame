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
using System.IO;
using System.Diagnostics;
using Tower.Levels;
namespace Tower
{
	public class Utils
	{
		private static Utils instance = null;

		public SpriteBatch batch;
		public SpriteFont font;
		public ContentManager content;

		public int width = 800;
		public int height = 600;

		public bool gameOver = false;
		public int currentLevel = 0;

		public float deltaTime;

		public Vector2 camera = new Vector2(0, 0);
		public Vector2 mousePos = new Vector2(0, 0);

		public MGame currentGame;
		public GraphicsDeviceManager _graphics;

		public List<Node> path;
		private Utils()
		{
			
		}

		public void setBatch(SpriteBatch batch)
        {
			this.batch = batch;
        }
		public void setFont(SpriteFont font)
		{
			this.font = font;
		}
		public void setContent(ContentManager content)
        {
			this.content = content;
        }

		public static Utils get() // singleton class
		{
			if (instance == null)
				instance = new Utils();
			return instance;
		}

		public void drawText(float x, float y, String text, Color color, int scale)
		{
			batch.DrawString(font, text, new Vector2(x, y), color);
        }

        public bool collide(GameObject a, GameObject b)
        {
            Rectangle rect = a.getRect();
			Rectangle r = b.getRect();
            if (r.X < rect.X + rect.Width &&
                r.X + r.Width > rect.X &&
                r.Y < rect.Y + rect.Height &&
                r.Height + r.Y > rect.Y)
            {
                return true;
            }
            return false;
        }


		public void saveScore(int score)
        {
			using (StreamWriter w = File.AppendText("score.txt"))
			{
				w.WriteLine(score);
			}
		}

		public List<int> loadTop10Score()
		{
			List<int> scores = new List<int>();

			using (TextReader reader = File.OpenText("score.txt"))
			{
				var lines = File.ReadLines("score.txt");
				foreach (var line in lines)
                {
					int s = int.Parse(line);
					scores.Add(s);
				}
			}
			scores.Sort();
			scores.OrderByDescending(o => o).Take(10);
			return scores;
		}

		public void saveGame(int level)
		{
			System.IO.File.WriteAllText("levels.txt", level.ToString());
		}

		public int loadUnlockLevel()
		{
			int levels = 1;
            try
            {
				using (TextReader reader = File.OpenText("levels.txt"))
				{
					levels = int.Parse(reader.ReadLine());
				}
			}
			catch(Exception e)
            {

            }
			
			return levels;
		}

		public Texture2D GenerateCircleTexture(int radius,Color color, bool fill = false)
		{
			Texture2D texture = new Texture2D(_graphics.GraphicsDevice, radius, radius);
			Color[] colorData = new Color[radius * radius];

			float diam = radius / 2f;
			float diamsq = diam * diam;

			for (int x = 0; x < radius; x++)
			{
				for (int y = 0; y < radius; y++)
				{
					int index = x * radius + y;
					Vector2 pos = new Vector2(x - diam, y - diam);
					if (pos.LengthSquared() <= diamsq)
					{
                        if (fill)
                        {
							colorData[index] = color;
						}
						if(pos.LengthSquared() > diamsq - radius)
                        {
							colorData[index] = color;
						}
					}
					else
					{
						colorData[index] = Color.Transparent;
					}
				}
			}

			texture.SetData(colorData);
			return texture;
		}
	}
}
