using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveDisplay : MonoBehaviour
{

    public static int waveNumber = 1;
    Text waveText;

    void Start()
    {
        waveText = GetComponent<Text>();
    }


    void Update()
    {
        waveText.text = "Wave: " + waveNumber;
    }
}