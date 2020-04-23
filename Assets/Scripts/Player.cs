using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player instance { get => _instance; }

    public float _maxHealth = 100f;
    public float _health;
    [SerializeField] private int _score;
    [SerializeField] private float _interactionRadius = 0.75f;
    [SerializeField] private Image damageImage;
    private bool damaged = false;
    private float _fadingDelay = 0.5f;

    public float interactionRadius { get => _interactionRadius; }

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void Start()
    {
        DisplayHandler.instance.UpdateHealth(_health, _maxHealth);
        damageImage.enabled = false;
    }

    public void Update()
    {
        if(damaged == true)
        {
            StartCoroutine(DamageScreen());
        }
    }

    public void IncreaseScore(int addScore)
    {
        _score += addScore;
        DisplayHandler.instance.UpdateScore(_score);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        damaged = true;
        DisplayHandler.instance.UpdateHealth(_health, _maxHealth);
    }

    public IEnumerator<WaitForSeconds> DamageScreen()
    {
            damageImage.enabled = true;
            damaged = false;
            yield return new WaitForSeconds(_fadingDelay);
            damageImage.enabled = false;
    }
}