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
    public class MenuScreen : MGame
    {
        Menu menu;

        private KeyboardState keyboardState;
        Vector2 mousePos;
        Texture2D bunny;
        bool showGameOver = false;

        GameObject bg;
        public MenuScreen(bool showGameOver)
        {
            this.showGameOver = showGameOver;
            menu = new Menu();
            bunny = Utils.get().content.Load<Texture2D>("bunny");

            bg = new GameObject(new Vector2(0, 0), Utils.get().content.Load<Texture2D>("menuBG"), new Rectangle(0, 0, 1280, 720));
            Msound.get().playMusic();

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
                        Msound.get().play("click");

                        switch (btn.getId())
                        {
                            case 0:
                                Utils.get().gameOver = false;
                                Utils.get().currentLevel = 0;
                                Utils.get().currentGame = new MainGame();
                                break;
                            case 1:
                                Utils.get().currentGame = new Dashboard();
                                break;
                            case 2:
                                Game1.self.Exit();
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
            Utils.get().batch.Draw(bunny, new Vector2(310,20), new Rectangle(0,0,594,259), Color.White);
            if (showGameOver)
            {
                Utils.get().drawText(200, 360, "Game Over!", Color.Red, 2);
            }
        }
    }
}
