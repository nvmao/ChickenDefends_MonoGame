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
	public class GameObject
	{
		protected Texture2D texture;
		protected Vector2 pos = new Vector2(0, 0); //position
		protected Vector2 vel = new Vector2(0, 0); // velocity
		protected Vector2 acc = new Vector2(0, 0); // acceleration
		protected bool flipX = false;
		protected Rectangle rect;
		public bool destroyFlag = false;

		public GameObject(Vector2 pos,Texture2D texture,Rectangle rect)
		{
			this.pos = pos;
			this.rect = rect;
			this.texture = texture;
		}

		public GameObject(Vector2 pos) { 
			this.pos = pos;
		}

		public void applyForce(Vector2 f)
		{ // add force 
			acc = new Vector2(acc.X + f.X, acc.Y + f.Y);
		}

		virtual public void update()
		{
			vel = new Vector2(vel.X + acc.X, vel.Y + acc.Y);
			pos = new Vector2(pos.X + vel.X, pos.Y + vel.Y);
			acc = new Vector2(0, 0);
		}

		virtual public void draw() {
			Vector2 dPos = new Vector2(pos.X - Utils.get().camera.X, pos.Y - Utils.get().camera.Y);
			if (flipX)
			{
				//Vector2 origin = new Vector2(pos.X + width/2, pos.Y + height/2);
				Vector2 origin = new Vector2(0, 0);
				Utils.get().batch.Draw(texture, dPos, rect, Color.White, 0, origin, new Vector2(1, 1), SpriteEffects.FlipHorizontally, 1);
			}
			else
			{
				Utils.get().batch.Draw(texture, dPos, rect, Color.White);
			}
		}


		virtual public Rectangle getRect()
		{
			return new Rectangle((int)pos.X, (int)pos.Y, rect.Width, rect.Height);
		}

		public Vector2 getPos()
		{
			return pos;
		}

		public void setPos(Vector2 pos)
		{
			this.pos = pos;
		}

		public Vector2 getVel()
		{
			return vel;
		}

		public void setVel(Vector2 vel)
		{
			this.vel = vel;
		}

		public Vector2 getAcc()
		{
			return acc;
		}

		public void setAcc(Vector2 acc)
		{
			this.acc = acc;
		}

		public Vector2 getCenter()
        {
			Vector2 center = new Vector2(pos.X + rect.Width / 2, pos.Y + rect.Height / 2);
			return center;
		}


	}
}
