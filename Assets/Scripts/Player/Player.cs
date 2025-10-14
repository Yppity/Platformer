using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerFlipper))]
[RequireComponent(typeof(PlayerJumper))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(InputReader))]

public class Player : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _runMultiplier = 2f;
    [SerializeField] private float _jumpForce = 23f;

    private PlayerAnimator _playerAnimator;
    private PlayerFlipper _playerFlipper;
    private PlayerJumper _playerJumper;
    private PlayerMover _playerMover;
    private InputReader _inputReader;

    private void Awake()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerFlipper = GetComponent<PlayerFlipper>();
        _playerJumper = GetComponent<PlayerJumper>();
        _playerMover = GetComponent<PlayerMover>();
        _inputReader = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _inputReader.JumpKeyPressed += Jump;
    }

    private void FixedUpdate()
    {
        Move();
        Flip();
    }

    private void OnDisable()
    {
        _inputReader.JumpKeyPressed -= Jump;
    }

    private void Jump()
    {
        if (_playerJumper.TryJump(_groundDetector.IsGrounded, _jumpForce))
            _playerAnimator.Jump();
    }

    private void Move()
    {
        bool isMoved = _playerMover.TryMove(_inputReader.MoveX, _speed, _runMultiplier, _inputReader.IsRunKeyDown, _groundDetector.IsGrounded);

        _playerAnimator.SetIsMoved(isMoved);
        _playerAnimator.SetIsRunning(_inputReader.IsRunKeyDown);
    }

    private void Flip()
    {
        _playerFlipper.Flip(_inputReader.MoveX);
    }
}