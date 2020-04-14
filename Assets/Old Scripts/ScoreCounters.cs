using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScoreCounters : MonoBehaviour
{
    public int enemyTypeNormalCounter = 0;
    public int enemyTypeFireCounter = 0;
    public int enemyTypeIceCounter = 0;
    
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
        case Enemies.NormalEnemy:
        default:
            enemyTypeNormalCounter++;
            break;
        }
    }
}
