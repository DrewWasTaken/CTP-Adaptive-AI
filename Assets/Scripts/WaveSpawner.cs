using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;

        public int enemyCount;
        public float spawnRate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    public int xPos;
    public int zPos;

    void Start()
    {
        waveCountdown = timeBetweenWaves;

    }

    void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if(!EnemyIsAlive())
            {
                Debug.Log("Wave Completed!");
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave ( waves[nextWave] ) );
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    bool EnemyIsAlive()
    {
    searchCountdown -= Time.deltaTime;
    if(searchCountdown <= 0f)
    {
        searchCountdown = 1f;
        if(GameObject.FindGameObjectWithTag ("Enemy") == null)
        {
            return false;
        }
    }
        return true;
    }


    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.enemyCount; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }

        state = SpawnState.WAITING;
        yield break;
    }



    void SpawnEnemy (Transform enemy)
    {
        xPos = Random.Range(1, 50);
        zPos = Random.Range(1, 31);

        Debug.Log("Spawning Enemy" + enemy.name);
        Instantiate(enemy, new Vector3(xPos, 1, zPos), Quaternion.identity);
    }
}