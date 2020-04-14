using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int _playerHealth = 100;
    public static int PlayerHealth
    {
        get {return _playerHealth;}
    }

    [SerializeField]
    private GameObject gameOverUI;

    void Start()
    {
        _playerHealth = 100;
    }


    void Update()
    {
        if (_playerHealth < 0) 
        {
            _playerHealth = 0;
            KillPlayer();
        }    
    }

    public void KillPlayer()
    {
        if(_playerHealth <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER");
        gameOverUI.SetActive(true);
        //Freeze player inputs, play death animation
    }



}
