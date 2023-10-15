//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;

//namespace Tower.Objs
//{

//	public class Door : GameObject
//	{
//		bool open = false;
//		SpriteSheet sheet ;
//		public Door(Vector2 pos)
//			:base(pos)
//		{

//			sheet = new SpriteSheet(Utils.get().content.Load<Texture2D>("door"), 1, 2);
//			sheet.setPlay(0, 1, 0.1f, true);

//			texture = sheet.getTexture();

//			rect = sheet.frames[0];
//		}

//		override
//		public void update()
//		{

//            //if (open)
//            //{
//            //    rect = sheet.frames[1];
//            //}
//            //else
//            //{
//            //    rect = sheet.frames[0];
//            //}

//			sheet.play();
//			rect = sheet.getCurrent();
//		}

//		public void setOpen()
//        {
//			open = true;
//        }
//		public bool isOpen()
//        {
//			return open;
//        }

//	}

//}
