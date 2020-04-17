using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHandler : MonoBehaviour
{
    [SerializeField] Text _wave = null;
    [SerializeField] Text _remainingEnemies = null;
    [SerializeField] Text _health = null;
    [SerializeField] Text _score = null;

    private static DisplayHandler _instance;
    public static DisplayHandler instance { get => _instance; }

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
    }

    void Start()
    {
        if (!(_wave || _remainingEnemies || _health || _score))
        {
            Debug.LogError("UI elements are missing!");
            enabled = false;
        }
    }

    public void UpdateWave(int wave, int totalWaves)
    {
        _wave.text = (wave + 1).ToString() + " / " + totalWaves.ToString();
    }

    public void UpdateRemainingEnemies(int destroyedEnemies, int totalEnemies)
    {
        _remainingEnemies.text = (totalEnemies - destroyedEnemies).ToString() + " / " + totalEnemies;
    }

    public void UpdateHealth(float health, float maxHealth)
    {
        _health.text = health.ToString() + " / " + maxHealth.ToString();
    }

    public void UpdateScore(int score)
    {
        _score.text = score.ToString();
    }
}