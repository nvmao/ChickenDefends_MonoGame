using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tower.Objs
{

	public class SpriteSheet
	{
		Texture2D texture;

		int rows, cols;

		// array of texture
		public Rectangle[] frames;

		float countTime = 0;
		int from;
		int to;
		int current;
		float time;
		bool loop;

		int width, height;
		bool flipX = false;

		public SpriteSheet(Texture2D texture, int rows, int cols)
		{

			this.texture = texture;
			this.rows = rows;
			this.cols = cols;

			frames = new Rectangle[rows * cols];

			int sizeCols = texture.Width / cols;
			int sizeRows = texture.Height / rows;
			width  = sizeCols;
			height = sizeRows;

			int ide = 0;
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					frames[ide] = new Rectangle(j * sizeCols, i * sizeRows, sizeCols, sizeRows);
					ide++;
				}
			}
		}

		public void flip(bool flipX)
		{
			this.flipX = flipX;
		}

		public void setPlay(int from, int to, float time, bool loop)
		{
			this.from = from;
			this.to = to;
			this.time = time;
			this.loop = loop;
			current = from;
		}

		public int getCurrentIndex()
        {
			return current;
        }

		public void draw(Vector2 pos)
        {
			Vector2 dPos = new Vector2(pos.X - Utils.get().camera.X, pos.Y - Utils.get().camera.Y);
			if (flipX)
            {
				//Vector2 origin = new Vector2(pos.X + width/2, pos.Y + height/2);
				Vector2 origin = new Vector2(0, 0);
				Utils.get().batch.Draw(texture, dPos, frames[current], Color.White, 0,origin,new Vector2(1,1), SpriteEffects.FlipHorizontally,1);
			}
            else
            {
				Utils.get().batch.Draw(texture, dPos, frames[current], Color.White);
			}
		}

		public Rectangle getCurrent()
        {
			return frames[current];
        }

		// play fromFrame to toFrame each delay by 'time'
		public void play()
		{
			if (from == to)
			{
				return;
			}

			countTime += Utils.get().deltaTime;
			
			if (countTime >= time)
			{
				countTime = 0;
				current++;
				if (current > to && loop)
				{
					current = from;
				}
				else if (current > to && !loop)
				{
					current = to;
				}
			}

		}
		public bool isDone()
        {
			return current >= to;
        }

		public Texture2D getTexture()
        {
			return texture;
        }
		public float getWidth()
		{
			return width;
		}
		public float getHeight()
		{
			return height;
		}
		public void dispose()
		{
			//texture.dispose();
		}
	}

}
