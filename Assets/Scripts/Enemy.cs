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
   
    [Header("Enemy Health")]
    public float health = 50f;
    
    [Header("Enemy Movement")]
    public Transform Player;
    
    public float  MoveSpeed = 4f;
    public int MaxDist = 10;
    public float MinDist = 5f;
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
   }

    void Update()
    {
        //transform.LookAt(Player); //Enemy Targets Player
        var dist = Vector3.Distance(transform.position, Player.position); //Distance To Player

        if (dist >= MinDist) // Player Not In Range
        {
            agent.SetDestination(Player.position);

            //Play Walking Blend Tree -1 Backward, 0, Idle, 1 Forward
            anim.Play("MoveSpeed", 1);
            MoveSpeed = 4f;
            anim.ResetTrigger("Attack");
        }

        else //Player In Range ENEMIES HIT THIS ONE AND IDK WHY
        {
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

    public void Die()
    {
        if(!isDying)
        {
            isDying = true;
            rb.constraints = RigidbodyConstraints.FreezeAll; 
            scoreCounter.EnemyKilled(myType);
            damagePlayer = 0;
            MoveSpeed = 0;
            anim.Play("zombie_death_standing") ;
            Destroy(gameObject,3f);
        }
    }
}