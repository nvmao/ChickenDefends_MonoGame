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

namespace Tower
{
    public class Win : MGame
    {
        Menu menu;

        private KeyboardState keyboardState;
        Vector2 mousePos;
        Texture2D bunny;
        GameObject bg;
        public Win()
        {
            menu = new Menu();
            menu.btns.Clear();
            menu.btns.Add(new LevelBtn(0, new Rectangle(540, 420, 100, 40), "GG!", Color.Gray));

            bg = new GameObject(new Vector2(0, 0), Utils.get().content.Load<Texture2D>("menuBG"), new Rectangle(0, 0, 1280, 720));
        }

        void updateEvent()
        {
            keyboardState = Keyboard.GetState();

            var mouseState = Mouse.GetState();
            float screenX = mouseState.X;
            float screenY = mouseState.Y;
            mousePos = new Vector2(screenX, screenY);
           
            foreach (LevelBtn btn in menu.getBtns()) // click button
            {
                btn.color = Color.White;
                if (    screenX >= btn.getRect().X &&
                        screenX <= btn.getRect().X + btn.getRect().Width &&
                        screenY >= btn.getRect().Y &&
                        screenY <= btn.getRect().Y + btn.getRect().Height)
                {
                    btn.color = Color.Tomato;
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {

                        switch (btn.getId())
                        {
                            case 0:
                                Utils.get().currentGame = new MenuScreen(false);
                                break;
                        }

                       
                    }
                }
            }

        }

        override
        public void update()
        {
            updateEvent();

        }

        override
        public void draw()
        {
            bg.draw();
            menu.draw();
            Utils.get().drawText(540, 310, "Thank you for playing!", Color.White, 2);

        }
    }
}
