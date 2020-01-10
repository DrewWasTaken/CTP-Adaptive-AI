using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounters : MonoBehaviour
{
    
    int enemyType1Counter = 0;
    int enemyType2Counter = 0;
    int enemyType3Counter = 0;
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
        case Enemies.Fire:
            // Add to fire  enemy total
            break;
        case Enemies.Ice:
            // Add to Ice   enemy total
            break;
        case Enemies.Type4:
            // Add to Type4 enemy total
            break;
        case Enemies.Type5:
            // Add to Type5 enemy total
            break;
        case Enemies.Type6:
            // Add to Type6 enemy total
            break;
        case Enemies.Normal:
        default:
            // Add to normie enemy total
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
