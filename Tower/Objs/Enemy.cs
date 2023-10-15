using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tower.Objs
{
	public class Enemy : GameObject
	{

		protected int health = 3;

		public Enemy(Vector2 pos)
			:base(pos)
		{
		}


		override
		public void update()
		{
			// TODO Auto-generated method stub
			base.update();


		}

		public virtual void damage(Projectile projectile)
        {
			
		}


	}

}
