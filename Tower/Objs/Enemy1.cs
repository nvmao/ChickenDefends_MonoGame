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
	public class Enemy1 : Enemy
	{

		bool onGround = false;
		float speed;
		float timeToflip;

		SpriteSheet walkSheet;
		SpriteSheet hitSheet;
		SpriteSheet currentSheet;
		List<Node> path;
		int currentTarget = 0;

		Texture2D circle = Utils.get().GenerateCircleTexture(4, Color.White,true);

		public Enemy1(Vector2 pos,List<Node> path)
			:base(pos)
		{
			speed = 0.2f;

			walkSheet = new SpriteSheet(Utils.get().content.Load<Texture2D>("enemy_walk"), 1, 14);
			walkSheet.setPlay(0, 13, 0.05f, true);
			hitSheet = new SpriteSheet(Utils.get().content.Load<Texture2D>("enemy_hit"), 1, 5);
			hitSheet.setPlay(0, 4, 0.05f, false);

			currentSheet = walkSheet;

			texture = currentSheet.getTexture();
			rect = currentSheet.frames[0];
			this.path = path;
		}


		override
		public void update()
		{
			// TODO Auto-generated method stub
			base.update();

			if(currentTarget > path.Count - 1)
            {
				Utils.get().gameOver = true;
				return;
            }

			Vector2 target = new Vector2(path[currentTarget].col * 16, path[currentTarget].row * 16);
			pos = Vector2.SmoothStep(pos, target, speed);

			if(target.X > pos.X)
            {
				flipX = false;
            }
			else if(target.X < pos.X)
            {
				flipX = true;
            }

			if(Vector2.Distance(pos,target) < 5)
            {
				currentTarget++;
            }

			if(currentSheet == hitSheet && currentSheet.getCurrentIndex() == 4)
            {
				currentSheet = walkSheet;
            }

			texture = currentSheet.getTexture();
			currentSheet.play();
			rect = currentSheet.getCurrent();
		}

        public override void draw()
        {
            base.draw();

			float x = pos.X;
			for(int i = 0; i < health; i++)
            {
				Vector2 circlePos = new Vector2(x,pos.Y - 5);
				Utils.get().batch.Draw(circle, circlePos, Color.Red);
				x += 5;
			}
			
		}

        public override void damage(Projectile projectile)
        {
			health -= projectile.getDamage();
			projectile.destroyFlag = true;

			currentSheet = hitSheet;
			hitSheet.setPlay(0, 4, 0.05f, false);

			if (health <= 0)
            {
				destroyFlag = true;
            }
		}

		override public Rectangle getRect()
		{
			return new Rectangle((int)pos.X , (int)pos.Y , rect.Width , rect.Height);
		}

	}

}
