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
        public Transform fireEnemy;

        public int enemyCount;
        public float spawnRate;
    }

    public int whichEnemy = 0;
    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        if(spawnPoints.Length == 0)
        {
            Debug.LogError("No Spawn Points Referenced");
        }
        
        waveCountdown = timeBetweenWaves;

    }

    void Update()
    {

        whichEnemy = Random.Range(1,3);

        if(state == SpawnState.WAITING)
        {
            if(!EnemyIsAlive())
            {
                WaveCompleted();
                
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

    void WaveCompleted()  //Add Difficulty Multiplier For More Waves Here
    {

        Debug.Log("Wave Completed!");
        
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! Looping...");
        }
        else
        {
            nextWave++;
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



    void SpawnEnemy (Transform _enemy)
    {
        Debug.Log("Spawning Enemy" + _enemy.name);

        if (whichEnemy==1)
        {
            Transform _sp = spawnPoints[ Random.Range(0, spawnPoints.Length)];
            Instantiate(_enemy, _sp.position, _sp.rotation);
        }
        else
        {
            Transform _sp = spawnPoints[ Random.Range(0, spawnPoints.Length)];
            Instantiate(_enemy, _sp.position, _sp.rotation);
        }


        
        
    }


        void EnemiesKilled()
        {
            if(enemyType1.iskilled)
            {
                enemyType1Counter++;
            }

            if(enemyType2.iskilled)
            {
                enemyType2Counter++;
            }

            if(enemyType3.iskilled)
            {
                enemyType3Counter++;
            }


            
        }



        void SpliceEnemies()
        {
            int enemyType1Counter = 0;
            int enemyType2Counter = 0;
            int enemyType3Counter = 0;
            
            if (enemyType1Counter > enemyType2Counter)
            {
                Destroy gameobject.enemyType1;
            }

        }


}