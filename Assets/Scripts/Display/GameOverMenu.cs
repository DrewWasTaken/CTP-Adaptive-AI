using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("APPLICATION QUIT");
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    } 

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}