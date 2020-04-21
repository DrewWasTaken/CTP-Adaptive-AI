using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 50; // Enemy Max Health
    [SerializeField] private int _score = 10;       // Score Per Kill
    [SerializeField] private float _strength = 10f; // Damage Against Player
    [SerializeField] private float _speed = 5f;     // Enemy Speed
    private float _health;
    private float _destroyDelay = 3f;
    private float _attackDelay = 2f;
    
    private bool _dying = false;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    public Image healthBar;
    Player _collidingPlayer = null;

    void Start()
    {
        _health = _maxHealth;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _navMeshAgent.stoppingDistance = Player.instance.interactionRadius;
        StartCoroutine(Attack());
        _navMeshAgent.speed = _speed;
    }

    void FixedUpdate()
    {
        MoveToPlayer();
        Rotate();
    }

    private void MoveToPlayer()
    {
        if (!_dying) 
        {
            if (Player.instance)
            {
                Vector3 playerPosition = Player.instance.transform.position;
                _navMeshAgent.SetDestination(playerPosition);
                
            }
            _animator.SetFloat("Direction", _navMeshAgent.velocity.normalized.x != 0 || _navMeshAgent.velocity.normalized.z != 0 ? 1f : 0f); //1 = Forward | 0 = Idle
        }
    }

    private void ResetMovement()
    {
        _navMeshAgent.SetDestination(transform.position);
    }

    private void Rotate()
    {
        if (Player.instance && !_dying)
        {
            Vector3 rotationTarget = new Vector3(_navMeshAgent.pathEndPosition.x, transform.position.y, _navMeshAgent.pathEndPosition.z);
            transform.LookAt(rotationTarget);
        }
    }

    public void OnHit(float damage)
    {
        _health -= damage;
        healthBar.fillAmount = _health / _maxHealth;

        if (_health <= 0f && !_dying)
        {
            Die();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        collider.TryGetComponent<Player>(out _collidingPlayer);
    }

    void OnTriggerExit(Collider collider)
    {
        _collidingPlayer = null;
        ResetMovement();
    }

    private void Die()
    {
        if (!_dying)
        {
            _dying = true;
            ResetMovement();
            Player.instance.IncreaseScore(_score);
            _animator.SetTrigger("Die");
            EnemySpawner.instance.OnEnemyDeath(transform.position);
            this.GetComponentInChildren<Canvas>().enabled = false;
            this.GetComponent<CapsuleCollider>().enabled = false;
            Destroy(this.gameObject, _destroyDelay);
        }
    }

    private IEnumerator<WaitForSeconds> Attack()
    {
        while (true)
        {
            if (!_dying && _collidingPlayer)
            {
                _animator.SetTrigger("Attack");
                Player.instance.TakeDamage(_strength);
            }
            yield return new WaitForSeconds(_attackDelay);
        }
    }
}