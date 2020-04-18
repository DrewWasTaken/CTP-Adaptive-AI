using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class EnemySpawner : MonoBehaviour
{
    private static EnemySpawner _instance;
    public static EnemySpawner instance { get => _instance; }

    [SerializeField] private List<EnemySpawnPoint> _spawnPoints;
    [SerializeField] private List<Wave> _waves;

    private int _currentWave = -1;
    private int _destroyedEnemies = 0;
    public bool _gameOver = false;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject victoryUI;
    [SerializeField] private GameObject UIAfterDeath;
    [SerializeField] private GameObject Gun;
    private Player player;    

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        if (!player) player = GameObject.FindObjectOfType<Player>();
    }

    public void OnEnemyDeath(Vector3 deathPosition)
    {
        float smallestDistance = Mathf.Infinity;
        EnemySpawnPoint closestSpawnPoint = null;

        foreach (EnemySpawnPoint spawnPoint in _spawnPoints)
        {
            float dist = Vector3.Distance(deathPosition, spawnPoint.transform.position);
            if(smallestDistance > dist)
            {
                closestSpawnPoint = spawnPoint;
                smallestDistance = dist;
            }

            spawnPoint.IncreaseProbability();
        }

        closestSpawnPoint.DecreaseProbability();
        closestSpawnPoint.DecreaseProbability();

        _destroyedEnemies++;
        DisplayHandler.instance.UpdateRemainingEnemies(_destroyedEnemies, _waves[_currentWave].enemies.Count);
    }

    public void Start()
    {
        ProceedToNextWave();
    }

    public void Update()
    {
        if (!_gameOver && _destroyedEnemies >= _waves[_currentWave].enemies.Count)
        {
            ProceedToNextWave();
        }

        if (player._health <= 0)
        {
            //Loser Screen
            EndGame();
        }
    }

    public void EndGame()
    {
        //Debug.Log("Game Over");
        _gameOver = true;
        player.GetComponent<RigidbodyFirstPersonController>().enabled = false;
        Gun.GetComponent<Gun>().enabled = false;
        UIAfterDeath.GetComponent<Canvas>().enabled = false;
        //Cursor.visible = true;

        if (player._health <= 0)
        {
            //Loser Screen
            gameOverUI.SetActive(true);
        }
        else
        {
            //Victory Screen
            victoryUI.SetActive(true);
        }
    }

    private void ProceedToNextWave()
    {
        _destroyedEnemies = 0;
        if (++_currentWave > _waves.Count - 1)
        {
            EndGame();
        }
        else
        {
            StartCoroutine(StartWave(_waves[_currentWave].enemies, _waves[_currentWave].spawnRateInSeconds));
            DisplayHandler.instance.UpdateWave(_currentWave, _waves.Count);
            DisplayHandler.instance.UpdateRemainingEnemies(_destroyedEnemies, _waves[_currentWave].enemies.Count);
        }
    }

    private EnemySpawnPoint CalculateSpawnPoint()
    {
        SortedDictionary<float, EnemySpawnPoint> dictionary = new SortedDictionary<float, EnemySpawnPoint>();
        float accumulativeProbability = 0f;
        _spawnPoints.ForEach(spawnPoint =>                        // Example of spawnpoint prob (0.3|0.5|0.7)                  Random Num 0.8
        {                                                                                                    
            accumulativeProbability += spawnPoint.probability;    // Accumulates All SpawnPoint Probabilities (0.3|0.8|1.5)    0.5% [0.5, SP1]
            dictionary.Add(accumulativeProbability, spawnPoint);  // Assigns SpawnPoint Per Accumulative Probability           0.7% [1.2, SP2] <-- Ceiling 
                                                                  // Key = Accumulative Probability (0.3|0.8|1.5)              0.3% [1.5, SP3]
        });
        float result = Random.Range(0f, accumulativeProbability); // Result = Random Number (0 - Final AP) Example (0 - 1.5)
        return dictionary.CeilingEntry(result).Value;             // Returns Spawnpoint Probability
                                                                  // (CeilingEntry Rounds Up To Next Entry) 
    }

    private IEnumerator StartWave(List<Enemy> enemies, float spawnRateInSeconds)
    {
        foreach(Enemy enemy in enemies)
        {
            yield return new WaitForSeconds(spawnRateInSeconds);
            CalculateSpawnPoint().SpawnEnemy(enemy);
        };
    }
}