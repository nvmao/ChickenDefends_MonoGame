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
	public class Menu
	{

		public List<LevelBtn> btns;

		public Menu()
		{
			btns = new List<LevelBtn>();
			// add 3 button"1"
			btns.Add(new LevelBtn(0,new Rectangle(500, 300,200,40),  "Play",Color.Gray));
			btns.Add(new LevelBtn(1,new Rectangle(500, 400,200,40),  "High Scores",Color.Gray));
			btns.Add(new LevelBtn(2,new Rectangle(500, 500,200,40),  "Quit",Color.Gray));
		}

		public void draw()
		{
			foreach (LevelBtn btn in btns)
			{
				btn.draw();
			}
		}

		public List<LevelBtn> getBtns()
		{
			return btns;
		}

		public void setBtns(List<LevelBtn> btns)
		{
			this.btns = btns;
		}
	}

}
