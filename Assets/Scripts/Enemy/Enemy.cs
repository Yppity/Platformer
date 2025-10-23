using UnityEngine;

public enum EnemyState
{
    Patrol,
    Chase
}

[RequireComponent(typeof(KnockbackHandler))]
[RequireComponent(typeof(EnemyPatroller))]
[RequireComponent(typeof(EnemyChaser))]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Stun))]

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerDetectionZone _playerDetectionZone;
    [SerializeField] private Transform _firstPatroltPoin;
    [SerializeField] private Transform _secondPatrolPoin;
    [SerializeField] private float _speed = 2;
    [SerializeField] private float _knockbackVerticalOffset = 0.3f;
    [SerializeField] private float _knockbackForce = 13f;
    [SerializeField] private float _stunDuration = 1f;
    [SerializeField] private int _attakDamage = 1;
    [SerializeField] private int _maxHealth = 3;


    private KnockbackHandler _knockbackHandler;
    private EnemyPatroller _enemyPatroller;
    private EnemyChaser _enemyChaser;
    private EnemyAttack _enemyAtakk;
    private Health _health;
    private Stun _stun;
    private EnemyState _currentState = EnemyState.Patrol;

    private void Awake()
    {
        _knockbackHandler = GetComponent<KnockbackHandler>();
        _enemyPatroller = GetComponent<EnemyPatroller>();
        _enemyChaser = GetComponent<EnemyChaser>();
        _enemyAtakk = GetComponent<EnemyAttack>();
        _health = GetComponent<Health>();
        _stun = GetComponent<Stun>();
        _enemyPatroller.SetCurrentTarget(_firstPatroltPoin);
        _enemyAtakk.Initialize(_attakDamage, _knockbackVerticalOffset, _knockbackForce);
        _health.Initialize(_maxHealth);
    }

    private void OnEnable()
    {
        _playerDetectionZone.PlayerEntered += StartChase;
        _playerDetectionZone.PlayerExited += StartPatrol;
        _health.Died += Died;
    }

    private void FixedUpdate()
    {
        if (_stun.IsStunned == false)
        {
            switch (_currentState)
            {
                case EnemyState.Patrol:
                    Patrol();
                    break;

                case EnemyState.Chase:
                    Chase();
                    break;
            }
        }
    }
    private void OnDisable()
    {
        _playerDetectionZone.PlayerEntered -= StartChase;
        _playerDetectionZone.PlayerExited -= StartPatrol;
        _health.Died -= Died;
    }

    public void TakeDamage(int damage, Vector2 knockbackDirection, float knockbackForce)
    {
        _stun.ApplyStun(_stunDuration);
        _knockbackHandler.ApplyKnockback(knockbackDirection, knockbackForce);
        _health.TakeDamage(damage);
    }

    private void Patrol()
    {
        _enemyPatroller.Patrol(_firstPatroltPoin, _secondPatrolPoin, _speed);
    }

    private void Chase()
    {
        _enemyChaser.ChasePlayer(_speed);
    }

    private void StartPatrol()
    {
        _currentState = EnemyState.Patrol;
    }

    private void StartChase(Player player)
    {
        _enemyChaser.SetTarget(player);
        _currentState = EnemyState.Chase;
    }

    private void Died()
    {
        gameObject.SetActive(false);
    }
}