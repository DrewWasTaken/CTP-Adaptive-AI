using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [Header("Probability Properties")]
    [SerializeField] [Range(0f, 1f)] private float _probability = 0.5f;
    [SerializeField] [Range(0f, 1f)] private float _probabilityStep = 0.1f;
    private float _probabilityFloor = 0f;                                  
    private float _probabilityCeiling = 1f;

    public float probability { get => _probability; }
    
    public void SpawnEnemy(Enemy enemy)
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }

    public void IncreaseProbability()
    {
        if (_probability + _probabilityStep > _probabilityCeiling)
        {
            _probability = _probabilityCeiling;
        }
        else
        {
            _probability += _probabilityStep;
        }
    }

    public void DecreaseProbability()
    {
        if (_probability - _probabilityStep < _probabilityFloor)
        {
            _probability = _probabilityFloor;
        }
        else
        {
            _probability -= _probabilityStep;
        }
    }
}
