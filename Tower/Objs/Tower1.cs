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
	public class Tower1 : MTower
	{
		Texture2D circle;
		GameObject target;

		public Tower1(Vector2 pos)
			:base(pos)
		{
			texture = Utils.get().content.Load<Texture2D>("tower1");
			rect = new Rectangle(0, 0, 32, 64);
			range = 200;
			attSpeed = 0.5f;
			countAttTime = 0.4f;

			circle = Utils.get().GenerateCircleTexture(range,Color.White);
		}

		override
		public void update()
		{
			// TODO Auto-generated method stub
			base.update();

			Vector2 center = new Vector2(pos.X + rect.Width / 2, pos.Y + rect.Height / 2);
			if (target != null && Vector2.Distance(target.getPos(), center) > range/2)
            {
				target = null;
			}
			if(target != null && target.destroyFlag)
            {
				target = null;
            }
		}

		override
		public void draw()
        {
			base.draw();
            if (getRect().Contains(Utils.get().mousePos)){
				Vector2 center = new Vector2(pos.X + rect.Width / 2, pos.Y + rect.Height / 2);
				Vector2 circlePos = new Vector2(center.X - range / 2, center.Y - range / 2);
				Utils.get().batch.Draw(circle, circlePos, Color.White);
			}
			
        }

		override public Rectangle getRect()
		{
			return new Rectangle((int)pos.X , (int)pos.Y , rect.Width , rect.Height);
		}

		public override void setTarget(GameObject target)
        {
			if(this.target != null)
            {
				return;
            }
			Vector2 center = new Vector2(pos.X + rect.Width / 2, pos.Y + rect.Height / 2);
			if (Vector2.Distance(target.getPos(), center) < range/2)
            {
				this.target = target;
            }
            
		}

		public override Projectile attack()
		{
			if (target == null)
			{
				return null;
			}

			countAttTime += Utils.get().deltaTime;
			if(countAttTime >= attSpeed)
            {
				countAttTime = 0;
				return new Projectile1(new Vector2(pos.X + rect.Width / 2, pos.Y + 8), target);
			}

			return null;
		}

	}

}
