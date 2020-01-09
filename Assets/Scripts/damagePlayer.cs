using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagePlayer : MonoBehaviour
{
    public int playerHealth = 30;
    int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        //print(playerHealth);

    }

    void OnCollisionEnter(Collision _collision)
    {
        if(_collision.gameObject.tag== "Enemy")
        {
            playerHealth -= damage;
            print ("Enemy Damages Player" + playerHealth);
        }
    }
}
