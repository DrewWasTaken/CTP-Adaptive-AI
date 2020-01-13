using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

   void Awake()
    {
        if(!anim) GetComponent<Animator>();
        if(!rb) GetComponent<Rigidbody>();
        Player = GameObject.FindWithTag("Player").transform;
        scoreCounter = GameObject.FindWithTag("GameController").GetComponent<ScoreCounters>();
    }
    

void Update()
{
    transform.LookAt(Player);
    var dist = Vector3.Distance(transform.position, Player.position);
    if ( dist >= MinDist)
    {
        transform.position += transform.forward * (MoveSpeed * Time.deltaTime);
        //Play Walking Blend Tree
        anim.Play("MoveSpeed", 1);
    }
    else
    {
        anim.Play("MoveSpeed", 0);
    }

    if (dist <= MaxDist)
    {
        //Enemy Attack + Anim Script
        anim.SetTrigger("Attack");
        anim.ResetTrigger("Attack");
    }
}

void OnCollisionEnter(Collision _collision)
    {
        if(_collision.gameObject.CompareTag("Player"))
        {
            _playerHealth -= damagePlayer;
            Debug.Log ($"Enemy Damages Player {_playerHealth}");
        }

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
            scoreCounter.EnemyKilled(myType);
            damagePlayer = 0;
            MoveSpeed = 0;
            anim.Play("zombie_death_standing") ;
            Destroy(gameObject,3f);
        }
    }
}