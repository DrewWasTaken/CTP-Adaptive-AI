using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
   [SerializeField] private float spawnprob = 0.5f;
    private float minprob = 0f;
    private float maxprob = 1f;

    //[SerializeField] private GameObject[] enemyTypes;

    public void Spawn(Enemy enemyType, List<SpawnPoint> spawnPoints)
    {
        
        if (Random.Range(minprob, maxprob) <= spawnprob)
        {
            Enemy enemy = Instantiate(enemyType, transform.position, transform.rotation);
            enemy.SetSpawnPoints(spawnPoints);
        }
    }

    public void IncreaseSpawnProb()
    {
        if (spawnprob + .1f <= maxprob)
        {
            spawnprob += .1f;
            print(this.name + " " + spawnprob);
        }
    }

    public void DecreaseSpawnProb()
    {
        if (spawnprob - .1f >= minprob)
        {
            spawnprob -= .1f;
            print(this.name + " " + spawnprob);
        }
    }
}
