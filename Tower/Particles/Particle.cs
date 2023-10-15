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
	public class Particle
	{

		int x, y;
		int count;
		List<Node> nodes;

		public Particle(float x2, float y2)
		{
			nodes = new List<Node>();
			Random r = new Random();

			// add 36 particle with speed by angle direction 
			for (int i = 0; i < 360; i += 10)
			{
				float speedX = (float)(((r.NextInt64(5)) + 1) * Math.Cos(i));
				float speedY = (float)(((r.NextInt64(5)) + 1) * Math.Sin(i));
				nodes.Add(new Node(x2, y2, speedX, speedY, (float)r.NextDouble(), (int)r.NextInt64(10)));
			}
		}

		public void update()
		{
			for (int i = 0; i < nodes.Count; i++)
			{
                nodes[i].update();
				if (nodes[i].isDead())
				{
					nodes.RemoveAt(i);
					break;
				}
			}
		}

		public void draw()
		{
			foreach (Node n in nodes)
			{
				n.draw();
			}
		}

		public bool isDead()
		{
			foreach (Node n in nodes)
			{
				if (!n.isDead())
				{
					return false;
				}
			}
			return true;
		}


	}

}
