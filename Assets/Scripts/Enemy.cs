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
    
    public int MoveSpeed = 4;
    public int MaxDist = 10;
    public float MinDist = 5f;

   void Awake()
    {
        if(!anim) { gameObject.GetComponent<Animator>(); }
        if(!rb) { gameObject.GetComponent<Rigidbody>(); }
        Player = GameObject.FindWithTag("Player").transform;
    }
    

    void Update()
    {
        transform.LookAt(Player);

        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            //Play Walking Blend Tree
            anim.Play("MoveSpeed", 1);
        }
            else
            {
                anim.Play("MoveSpeed", 0);
    
            }

            if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
            {
                //Enemy Attack + Anim Script
                anim.SetTrigger("Attack");
                anim.ResetTrigger("Attack");

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

    void Die()
    {
        anim.Play("Dead");
        Destroy(gameObject);
    }
}