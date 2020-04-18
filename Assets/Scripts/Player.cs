using UnityEngine;
public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player instance { get => _instance; }

    public float _maxHealth = 100f;
    public float _health;
    [SerializeField] private int _score;
    [SerializeField] private float _interactionRadius = 1.5f;

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
    }

    public void IncreaseScore(int addScore)
    {
        _score += addScore;
        DisplayHandler.instance.UpdateScore(_score);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        DisplayHandler.instance.UpdateHealth(_health, _maxHealth);
    }
}