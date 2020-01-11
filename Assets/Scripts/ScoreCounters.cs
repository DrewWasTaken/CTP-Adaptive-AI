using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveSpawner;
public class ScoreCounters : MonoBehaviour
{
    public Enemies enemyType;

    int enemyTypeNormalCounter = 0;
    int enemyTypeFireCounter = 0;
    int enemyTypeIceCounter = 0;
    int enemyType4Counter = 0;
    int enemyType5Counter = 0;
    int enemyType6Counter = 0;

    void Update()
    {
        
    }
    
    public void EnemyKilled(Enemies enemyType)
    {
        switch (enemyType)
    {
        case Enemies.FireEnemy:
            enemyTypeFireCounter++;            
            break;
        case Enemies.IceEnemy:
            enemyTypeIceCounter++;
            break;
        case Enemies.enemyType4:
            enemyType4Counter++;
            break;
        case Enemies.enemyType5:
            enemyType5Counter++;
            break;
        case Enemies.enemyType6:
            enemyType6Counter++;
            break;
        case Enemies.NormalEnemy:
        default:
            enemyTypeNormalCounter++;
            break;
    }
}

/*

public class NormalEnemy : Enemy
{
  private int key = 1; //index of this enemy type. The key.

  //You now inherit from your enemy class.

   //...........
//Make sure pass by ref...
public void incrementCount(int[] enemyCounter){
  enemyCounter[key]++;
  }
}
}
//In you update, in wavespawner you only need....:
foreach(Enemy enemy in EnemyList){
  if(enemy.die())
    enemy.incrementCount(enemyCounter)



    }

    /*        
            if (enemyType1Counter > enemyType2Counter)
            {
                Destroy gameobject.enemyType1;
                Instantiate
            }
        }
    */
}
