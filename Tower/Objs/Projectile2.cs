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
	public class Projectile2 : Projectile
	{
		SpriteSheet sheet;

		public Projectile2(Vector2 pos,GameObject target)
			:base(pos,target)
		{
			this.speed = 0.2f;
			sheet = new SpriteSheet(Utils.get().content.Load<Texture2D>("pro2"), 1, 4);
			sheet.setPlay(0, 3, 0.05f, true);

			texture = sheet.getTexture();
			rect = sheet.frames[0];
			damage = 1;

			Msound.get().play("jump");

		}

		bool moveTo(Vector2 to)
		{
			float disc = Vector2.Distance(pos,to);

			float normalizedX = (to.X - pos.X) / disc;
			float normalizedY = (to.Y - pos.Y) / disc;

			pos.X += normalizedX * speed;
			pos.Y += normalizedY * speed;

			return true;
		}

		override
		public void update()
		{
			// TODO Auto-generated method stub
			base.update();

			moveTo(target.getCenter());

			sheet.play();
			rect = sheet.getCurrent();
		}


	}

}
