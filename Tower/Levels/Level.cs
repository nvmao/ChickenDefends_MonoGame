using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Tower.Objs;

namespace Tower.Levels
{
	public class Level
	{
		
		protected List<GameObject> gameObjects = new List<GameObject>();

		public Level() { }

		public List<GameObject> GetGameObjects()
        {
			return gameObjects;
        }

		public virtual void draw() {
		}

		public virtual void drawUI()
		{
			
		}
		public virtual MTower getTower1(Rectangle rect,List<GameObject> gameObjects,int tower)
        {
			return null;
        }

	};

}
