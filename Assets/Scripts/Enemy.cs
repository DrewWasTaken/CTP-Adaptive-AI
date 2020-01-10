using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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


    public int playerHealth = 30;
    int damagePlayer = 10;


   void Awake()
    {
        if(!anim) GetComponent<Animator>();
        if(!rb) GetComponent<Rigidbody>();
        Player = GameObject.FindWithTag("Player").transform;
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
        if(_collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth -= damagePlayer;
            Debug.Log ($"Enemy Damages Player {playerHealth}");
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
        WaveSpawner.Instance.EnemyKilled(enemyType);
        damagePlayer = 0;
        MoveSpeed = 0;
        anim.Play("zombie_death_standing") ;
        Destroy(gameObject,3f);
    }
}