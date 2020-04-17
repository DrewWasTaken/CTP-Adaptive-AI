using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    //public GameObject spawnerReference;
    //public GameObject playerReference;


    public void Quit()
    {
        Debug.Log("APPLICATION QUIT");
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    } 

    public void Retry()
    {
        //spawnerReference.GetComponent<EnemySpawner>()._gameOver = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(1);

        //playerReference.GetComponent<Player>()._health = _maxHealth;
    }
}