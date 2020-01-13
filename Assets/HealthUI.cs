using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HealthUI : MonoBehaviour
{
    
    private Text HealthText;
    // Start is called before the first frame update
    void Awake()
    {
        HealthText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = "HEALTH: " + PlayerStats._playerHealth.ToString();
    }
}
