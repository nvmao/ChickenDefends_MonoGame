using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tower.Levels;

namespace Tower.Objs
{
	public class MTower : GameObject
	{
		protected int range;
		protected float attSpeed;

		protected float countAttTime = 0;

		public MTower(Vector2 pos)
			:base(pos)
		{
			
		}

		override
		public void update()
		{
			// TODO Auto-generated method stub
			base.update();
		}

		override public Rectangle getRect()
		{
			return new Rectangle((int)pos.X , (int)pos.Y , rect.Width , rect.Height);
		}

		public virtual void setTarget(GameObject target)
        {

        }

		public virtual Projectile attack()
        {
			return null;
        }

	}

}
