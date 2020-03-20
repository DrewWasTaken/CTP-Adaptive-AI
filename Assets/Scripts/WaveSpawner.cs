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
        [Header("Enemies To Spawn")]
        public Enemies[] enemySpawnSequence;
        public float spawnRate;
    }

    //Enemy Hierarchy Dropdown
    [SerializeField] public GameObject[] enemyTypes;    

    public Wave[] waves; //Waves Array
    private int nextWave = 0;

    public Transform[] spawnPoints; //Spawn Points Array

    public float timeBetweenWaves = 5f; //Wait Duration After All Enemies Killed
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

        if(state == SpawnState.WAITING) // Game PLaying | Waiting For 0 Enemies
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
            WaveDisplay.waveNumber ++;
        }
    }

    bool EnemyIsAlive() //Searches For Alive Enemies 
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
        Debug.Log($"Spawning Wave: {_wave.name}");
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.enemySpawnSequence.Length; i++)
        {
            SpawnEnemy(_wave.enemySpawnSequence[i]);
            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }

        state = SpawnState.WAITING;
        yield break;
    }


    void SpawnEnemy (Enemies enemyType) //Spawns Enemy Varient Type
    {
        Debug.Log($"Spawning Enemy {enemyType}");
        Transform _sp = spawnPoints[ Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyTypes[(int)enemyType], _sp.position, _sp.rotation);
    }
}