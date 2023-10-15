using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Tower;
using Tower.Objs;
using Tower.Particles;
using Tower.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower.Levels
{
	public class LevelBtn : GameObject
	{

		string text;
		int id;
		public Color color;
		public LevelBtn(int id,Rectangle rect, string text,Color color)
			:base(new Vector2(0,0))
		{
			this.text = text;
			this.id = id;
			this.rect = rect;
			this.color = color;
		}

		override
		public void draw()
		{
			texture = new Texture2D(Utils.get()._graphics.GraphicsDevice, 1, 1);
			texture.SetData(new[] { Color.White });
			Utils.get().batch.Draw(texture, rect, color);
			Utils.get().drawText(rect.X + rect.Width/2 - 4*text.Length, rect.Y + rect.Height/2-8, text, Color.Black, 1);

		}

		public int getId()
        {
			return id;
        }
		override public Rectangle getRect()
		{
			return rect;
		}

	}

}
