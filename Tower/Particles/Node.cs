using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tower.Particles
{

	// Particle node
	public class Node
	{
		float x, y;
		float speedX, speedY;
		int size;
		float deathTime;
		Texture2D texture;

		bool isAlive = true;

		public Node(float x, float y, float speedX2, float speedY2, float deathTime, int size)
		{
			this.x = x;
			this.y = y;
			this.speedX = speedX2;
			this.speedY = speedY2;
			this.deathTime = deathTime;
			this.size = size;
			texture = Utils.get().content.Load<Texture2D>("node");
		}

		public void update()
		{
			x += speedX;
			y += speedY;

			deathTime -= Utils.get().deltaTime;
			if (deathTime < 0)
			{
				isAlive = false;
			}
		}

		public bool isDead()
		{
			return isAlive == false;
		}

		public void draw()
		{
			Utils.get().batch.Draw(texture, new Vector2(x, y),Color.White);
		}
	}
}
