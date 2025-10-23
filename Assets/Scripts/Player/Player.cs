using UnityEngine;
using UnityEngine.UIElements.Experimental;

[RequireComponent(typeof(KnockbackHandler))]
[RequireComponent(typeof(Invulnerability))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerFlipper))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerJumper))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Collector))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Stun))]

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _runMultiplier = 2f;
    [SerializeField] private float _jumpForce = 23f;
    [SerializeField] private float _invulnerabilityTime = 0.5f;
    [SerializeField] private float _knockbackForce = 4f;
    [SerializeField] private int _maxHealth = 5;
    [SerializeField] private int _attakDamage = 1;
    [SerializeField] private float _attackDelay = 0.4f;
    [SerializeField] private float _attackHitboxLifetime = 0.1f;
    [SerializeField] private float _attackStunDuration = 0.5f;
    [SerializeField] private float _knockbackStunDuration = 0.1f;

    private KnockbackHandler _knockbackHandler;
    private Invulnerability _invulnerability;
    private PlayerAnimator _playerAnimator;
    private PlayerFlipper _playerFlipper;
    private PlayerAttack _playerAttack;
    private PlayerJumper _playerJumper;
    private PlayerMover _playerMover;
    private InputReader _inputReader;
    private Collector _collector;
    private Health _health;
    private Stun _stun;

    private void Awake()
    {
        _knockbackHandler = GetComponent<KnockbackHandler>();
        _invulnerability = GetComponent<Invulnerability>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerFlipper = GetComponent<PlayerFlipper>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerJumper = GetComponent<PlayerJumper>();
        _playerMover = GetComponent<PlayerMover>();
        _inputReader = GetComponent<InputReader>();
        _collector = GetComponent<Collector>();
        _health = GetComponent<Health>();
        _stun = GetComponent<Stun>();
        _health.Initialize(_maxHealth);
    }

    private void OnEnable()
    {
        _inputReader.JumpKeyPressed += Jump;
        _inputReader.AttackKeyPressed += Attack;
        _health.HealthChanged += HandleHealthChanged;
        _collector.MedicineChestCollected += Heal;
    }

    private void FixedUpdate()
    {
        Move();
        Flip();
    }

    private void OnDisable()
    {
        _inputReader.JumpKeyPressed -= Jump;
        _inputReader.AttackKeyPressed -= Attack;
        _health.HealthChanged -= HandleHealthChanged;
        _collector.MedicineChestCollected -= Heal;
    }

    public void TakeDamage(int damage, Vector2 knockbackDirection, float knockbackForce)
    {
        if (_invulnerability.IsInvulnerable)
            return;

        _health.TakeDamage(damage);
        _invulnerability.StartInvulnerability(_invulnerabilityTime);
        _knockbackHandler.ApplyKnockback(knockbackDirection, knockbackForce);
        _stun.ApplyStun(_knockbackStunDuration);
    }

    private void Jump()
    {
        if (_stun.IsStunned)
            return;

        if (_playerJumper.TryJump(_groundDetector.IsGrounded, _jumpForce))
            _playerAnimator.Jump();
    }

    private void Move()
    {
        if (_stun.IsStunned) 
            return;

        bool isMoved = _playerMover.TryMove(_inputReader.MoveX, _speed, _runMultiplier, _inputReader.IsRunKeyDown, _groundDetector.IsGrounded);

        _playerAnimator.SetIsMoved(isMoved);
        _playerAnimator.SetIsRunning(_inputReader.IsRunKeyDown);
    }

    private void Flip()
    {
        _playerFlipper.Flip(_inputReader.MoveX);
    }

    private void Attack()
    {
        if (_stun.IsStunned || _groundDetector.IsGrounded == false)
            return;

        _playerAttack.Attack(_attakDamage, _knockbackForce, _attackDelay, _attackHitboxLifetime);
        _playerAnimator.Attack();
        _stun.ApplyStun(_attackStunDuration);
    }

    private void HandleHealthChanged(int heath)
    {
        Debug.Log(heath);
    }

    private void Heal(int value)
    {
        _health.Heal(value);
    }
}