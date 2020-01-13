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
    }



}
