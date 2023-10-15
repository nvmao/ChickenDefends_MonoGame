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
	public class Projectile : GameObject
	{
		protected GameObject target;
		protected float speed;
		protected int damage;

		float countDeadTime = 0;

		public Projectile(Vector2 pos,GameObject target)
			:base(pos)
		{
			this.target = target;
		}


		override
		public void update()
		{
			// TODO Auto-generated method stub
			base.update();

			pos = Vector2.SmoothStep(pos, target.getPos(), speed);

			countDeadTime += Utils.get().deltaTime;
			if(countDeadTime >= 1.0f)
            {
				destroyFlag = true;
            }
		}

		public int getDamage()
		{
			return damage;
		}



	}

}
