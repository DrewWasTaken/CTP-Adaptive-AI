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
    [SerializeField] public Enemy[] enemyTypes;    

    public Wave[] waves; //Waves Array
    private int nextWave = 0;

    private SpawnPoint[] spawnPoints; //Spawn Points Array

    public float timeBetweenWaves = 5f; //Wait Duration After All Enemies Killed
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    [SerializeField] private SpawnPoint spawnPointPrefab;
    [SerializeField] private Vector3[] spawnPointPositions;

    void Start()
    {
        InstantiateSpawnPoints(6);

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No Spawn Points Referenced");
        }
        waveCountdown = timeBetweenWaves;

    }

    private void InstantiateSpawnPoints(int amount)
    {
        spawnPoints = new SpawnPoint[6];
        for (int i = 0; i < amount; i++)
        {
            spawnPoints[i] = Instantiate(spawnPointPrefab, spawnPointPositions[i], Quaternion.identity);
            spawnPoints[i].gameObject.layer = LayerMask.NameToLayer("Spawners");
        }
    }

    void Update()
    {

        if(state == SpawnState.WAITING) // Game Playing | Waiting For 0 Enemies
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


    void WaveCompleted()
    {

        Debug.Log("Wave Completed!");
        
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE!");
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

    void SpawnEnemy(Enemies enemyType) //Spawns Enemy Varient Type
    {
        //Debug.Log($"Spawning Enemy {enemyType}");
        SpawnPoint _sp = spawnPoints[ Random.Range(0, spawnPoints.Length)];
        _sp.Spawn(enemyTypes[(int)enemyType], new List<SpawnPoint>(spawnPoints));
        //Transform _sp = spawnPoints[ Random.Range(0, spawnPoints.Length)];
        //Instantiate(enemyTypes[(int)enemyType], _sp.position, _sp.rotation);
    }
}