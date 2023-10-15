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
    public class MainGame : MGame
    {
        List<GameObject> gameobjects;
        List<Level> levels;

        float countTime = 0;
        int startScore = 0;
        int score = 0;
        float time = 0;
        Menu menu;

        WaveSpawner waveSpawner;

        private KeyboardState keyboardState;

        Tower1 tower1 = null;
        Tower2 tower2 = null;
        int wave = 1;
        int gold;

        enum GAMESTATE
        {
            addBuilding1,
            addBuilding2,
            playing,
            waitForNextWave
        }
        GAMESTATE gameState;
        float countWaitnextState = 0;

        public MainGame()
        {
            //Utils.get().currentLevel = 3;
            levels = new List<Level>();
            levels.Add(new Level1());
            gameobjects = levels[Utils.get().currentLevel].GetGameObjects();
            //menu = new Menu();
            startScore = 0;

            tower1 = new Tower1(new Vector2(0, 0));
            tower2 = new Tower2(new Vector2(0, 0));
            gold = 100;
            gameState = GAMESTATE.playing;

            menu = new Menu();
            menu.btns.Clear();
            menu.btns.Add(new LevelBtn(0, new Rectangle(1000, 5, 110, 40), "tower1 (40)", Color.White));
            menu.btns.Add(new LevelBtn(1, new Rectangle(1150, 5, 110, 40), "tower2 (60)", Color.White));

            waveSpawner = new WaveSpawner(1);
        }


        void updateEvent()
        {
            keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();
            float screenX = mouseState.X;
            float screenY = mouseState.Y;
            Vector2 mousePos = new Vector2(screenX, screenY);
            Utils.get().mousePos = mousePos;

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Utils.get().currentGame = new MenuScreen(true);
            }
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                switch (gameState)
                {
                    case GAMESTATE.addBuilding1:
                        MTower newTower1 = levels[Utils.get().currentLevel].getTower1(tower1.getRect(),gameobjects,1);
                        if(newTower1 != null)
                        {
                            gameobjects.Add(newTower1);
                            gameState = GAMESTATE.playing;
                        }
                        break;
                    case GAMESTATE.addBuilding2:
                        MTower newTower2 = levels[Utils.get().currentLevel].getTower1(tower2.getRect(), gameobjects,2);
                        if (newTower2 != null)
                        {
                            gameobjects.Add(newTower2);
                            gameState = GAMESTATE.playing;
                        }
                        break;
                    case GAMESTATE.playing:
                        foreach (LevelBtn btn in menu.getBtns()) // click button
                        {
                            btn.color = Color.White;
                            if (screenX >= btn.getRect().X &&
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
                                            if(gold >= 40)
                                            {
                                                gold -= 40;
                                                gameState = GAMESTATE.addBuilding1;
                                            }
                                            break;
                                        case 1:
                                            if (gold >= 60)
                                            {
                                                gold -= 60;
                                                gameState = GAMESTATE.addBuilding2;

                                            }
                                            break;
                                    }

                                }
                            }
                        }
                        break;
                }
            }
           
        }


        override
        public void update()
        {
            updateEvent();
            time += Utils.get().deltaTime;
            if (Utils.get().gameOver)
            {
                Utils.get().saveScore(score);
                Msound.get().play("die");
                Utils.get().currentGame = new MenuScreen(true);
            }

            if(gameState == GAMESTATE.playing)
            {
                Enemy e = waveSpawner.spawn();
                if (e != null)
                {
                    gameobjects.Add(e);
                }
            }
            if (gameState == GAMESTATE.waitForNextWave)
            {
                countWaitnextState += Utils.get().deltaTime;
                if(countWaitnextState >= 5.0f)
                {
                    countWaitnextState = 0;
                    gameState = GAMESTATE.playing;
                }
            }


            if (waveSpawner.isDone() && wave < 5)
            {
                wave++;
                Msound.get().play("nextLevel");

                gameState = GAMESTATE.waitForNextWave;
                waveSpawner = new WaveSpawner(wave);
            }

            if (wave == 5 && countEnemy() == 0)
            {
                Utils.get().currentGame = new Win();
            }

        }

        public int countEnemy()
        {
            int c = 0;
            foreach(GameObject g in gameobjects)
            {
                if(g is Enemy)
                {
                    c++;
                }
            }
            return c;
        }

        override
        public void draw()
        {
            levels[Utils.get().currentLevel].draw();
            //Utils.get().camera.X = player.getPos().X - 300;
            //Utils.get().camera.Y = 50;

            List<GameObject> newProjectiles = new List<GameObject>();
           
            foreach (GameObject obj in gameobjects)
            {
                obj.update();
                obj.draw();

                if(obj is MTower)
                {
                    Projectile p = ((MTower)obj).attack();
                    if(p != null)
                    {
                        newProjectiles.Add(p);
                    }
                }
               

                foreach (GameObject otherObj in gameobjects)
                {
                    if(obj == otherObj)
                    {
                        continue;
                    }

                    if(obj is MTower && otherObj is Enemy)
                    {
                        ((MTower)obj).setTarget(otherObj);
                    }

                    if (obj.getRect().Intersects(otherObj.getRect()))
                    {

                        if (obj is Enemy && otherObj is Projectile)
                        {
                            ((Enemy)obj).damage((Projectile)otherObj);
                            Msound.get().play("hit");
                        }
                    }
                }
               
            }
            gameobjects.AddRange(newProjectiles);

            for (int i = 0; i < gameobjects.Count; i++)
            {
                if (gameobjects[i].destroyFlag)
                {
                    if (gameobjects[i] is Enemy)
                    {
                        score += 2;
                        gold += 2;
                    }
                    gameobjects.Remove(gameobjects[i]);

                }
            }

            levels[Utils.get().currentLevel].drawUI();

            Utils.get().drawText(20, 10, "Time: " + time.ToString("0.00") + " s", Color.White, 2);
            Utils.get().drawText(20, 30, "Score: " + ((int)(score)) , Color.White, 2);
            Utils.get().drawText(140, 10, "Wave: " + (wave), Color.Red, 2);
            Utils.get().drawText(840, 10, "Gold: " + (gold), Color.Red, 2);

            foreach (LevelBtn btn in menu.getBtns())
            {
                btn.draw();
            }

            Vector2 mousePos = Utils.get().mousePos;
            switch (gameState)
            {
                case GAMESTATE.addBuilding1:
                    tower1.setPos(new Vector2(mousePos.X - tower1.getRect().Width / 2, mousePos.Y - tower1.getRect().Height / 2));
                    tower1.draw();
                    break;
                case GAMESTATE.addBuilding2:
                    tower2.setPos(new Vector2(mousePos.X - tower1.getRect().Width / 2, mousePos.Y - tower1.getRect().Height / 2));
                    tower2.draw();
                    break;
            }

        }
    }
}
