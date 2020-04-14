using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    [SerializeField] [Tooltip("Time (In Seconds) Between Waves.")] private float _lengthInSeconds;
    [SerializeField] [Tooltip("Seconds Between Each Enemy Spawn.")] private float _spawnRateInSeconds;
    [SerializeField] private List<Enemy> _enemies;

    public float lengthInSeconds { get => _lengthInSeconds; }
    public float spawnRateInSeconds { get => _spawnRateInSeconds; }
    public List<Enemy> enemies { get => _enemies; }
}