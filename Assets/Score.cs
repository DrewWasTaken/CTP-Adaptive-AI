using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
public static int scoreCount = 0;
    Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
    }


    void Update()
    {
        scoreText.text = "Score: " + scoreCount;
    }
}
