using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tower.Objs;

namespace Tower.Levels
{
    public class WaveSpawner
    {
        int wave;

        float countSpawnTime = 0;
        float spawnTime = 0;
        int maxEnemyCount = 0;
        int enemyCount;

        public WaveSpawner(int wave)
        {
            this.wave = wave;

            enemyCount = 0;
            countSpawnTime = 0;
            switch (wave)
            {
                case 1:
                    spawnTime = 1.0f;
                    maxEnemyCount = 20;
                    break;
                case 2:
                    spawnTime = 0.8f;
                    maxEnemyCount = 30;
                    break;
                case 3:
                    spawnTime = 0.3f;
                    maxEnemyCount = 100;
                    break;
                case 4:
                    spawnTime = 0.2f;
                    maxEnemyCount = 120;
                    break;
                case 5:
                    spawnTime = 0.1f;
                    maxEnemyCount = 200;
                    break;
            }

        }

        public Enemy spawn()
        {
            countSpawnTime += Utils.get().deltaTime;
            if(countSpawnTime >= spawnTime && enemyCount < maxEnemyCount)
            {
                countSpawnTime = 0;
                enemyCount++;
                return new Enemy1(new Vector2(3 * 16, 3 * 16), Utils.get().path);
            }
            return null;
        }
        public bool isDone()
        {
            return enemyCount >= maxEnemyCount;
        }

    }
}
