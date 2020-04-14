using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

using static PlayerStats;
public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private List<SpawnPoint> spawners;

    [Header("Enemy Health")]
    public float health = 50f;
    
    [Header("Enemy Movement")]
    public Transform Player;
    
    public float  MoveSpeed = 0f;
    public float MinDist = 1f;
    int damagePlayer = 10; 
    public Enemies myType;
    private ScoreCounters scoreCounter;
    bool isDying = false;
    [SerializeField] NavMeshAgent agent;

   void Awake()
   {
        if(!anim) GetComponent<Animator>();
        if(!rb) GetComponent<Rigidbody>();
        //if (!agent) GetComponent<NavMeshAgent>();
        Player = GameObject.FindWithTag("Player").transform;
        scoreCounter = GameObject.FindWithTag("GameController").GetComponent<ScoreCounters>();
        agent = GetComponent<NavMeshAgent>();
        //spawners = new List<SpawnPoint>();
        //foreach (GameObject go in GameObject.FindGameObjectsWithTag("SpawnPoint"))
        //{
        //    spawners.Add(go.GetComponent<SpawnPoint>());
        //}
        //print(spawners);
   }
    
    void Update()
    {
        var dist = Vector3.Distance(transform.position, Player.position); //Distance To Player

        if (dist >= MinDist) // Player Not In Range
        {
            agent.SetDestination(Player.position);
            agent.isStopped = false;
            //Play Walking Blend Tree -1 Backward, 0, Idle, 1 Forward
            anim.Play("MoveSpeed", 1);
            MoveSpeed = 4f;
            anim.ResetTrigger("Attack");
        }

        else //Player In Range
        {
            agent.isStopped = true;
            anim.Play("MoveSpeed", 0);
            MoveSpeed = 0f;
            anim.SetTrigger("Attack");
        }
    }


    void OnCollisionEnter(Collision _collision)
    {
        if(_collision.gameObject.CompareTag("Player"))
        {
            _playerHealth -= damagePlayer;
        }
            Debug.Log ($"Enemy Damages Player {_playerHealth}");
    }

    public void TakeDamage (float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Die();
        }
    }

    public void SetSpawnPoints(List<SpawnPoint> spawnPoints)
    {
        spawners = spawnPoints;
    }

    public void Die()
    {
        if(!isDying)
        {
            isDying = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            RaycastHit hit;
            float previousDistance = 0f;
            SpawnPoint closestSpawner = null;

            foreach (SpawnPoint spawner in spawners)
            {
                Physics.Raycast(transform.position, spawner.transform.position, out hit, Mathf.Infinity, ~LayerMask.GetMask("Spawners"));
                print("hello " + hit.distance);
                if (hit.distance < previousDistance)
                {
                    previousDistance = hit.distance;
                    closestSpawner = spawner;

                }
            }
            
            foreach (SpawnPoint spawner in spawners)
            {
                if (spawner.Equals(closestSpawner))
                {
                    spawner.DecreaseSpawnProb();
                }
                else
                {
                    spawner.IncreaseSpawnProb();
                }
            }

            scoreCounter.EnemyKilled(myType);
            damagePlayer = 0;
            MoveSpeed = 0;
            //Destroy(agent);
            anim.Play("zombie_death_standing") ;
            Destroy(gameObject,3f);
        }
    }
}