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
	public class Apple : GameObject
	{
		SpriteSheet sheet;


		public Apple(Vector2 pos)
			:base(pos)
		{
		
			sheet = new SpriteSheet(Utils.get().content.Load<Texture2D>("MonedaR"), 1, 3);
			sheet.setPlay(0, 2, 0.1f, true);

			texture = sheet.getTexture();
			rect = sheet.frames[0];

		}


		override
		public void update()
		{
			// TODO Auto-generated method stub
			base.update();

			sheet.play();
			rect = sheet.getCurrent();

		}


	}

}
