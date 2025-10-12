using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputReader))]

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private GroundDetector _landedCheck;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _runMultiplier = 2f;
    [SerializeField] private float _jumpForce = 20f;

    private Rigidbody2D _rigidbody2D;
    private InputReader _inputReader;
    private Vector3 _leftScale = new Vector3(1, 1, 1);
    private Vector3 _rightScale = new Vector3(-1, 1, 1);

    public bool IsMoved { get; private set; }
    public bool IsRunning { get; private set; }

    public Action Jumped;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _inputReader = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _inputReader.JumpKeyPressed += Jump;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnDisable()
    {
        _inputReader.JumpKeyPressed -= Jump;
    }

    private void Jump()
    {
        if (_landedCheck.IsGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            Jumped?.Invoke();
        }
    }

    private void Move()
    {
        float moveX = _inputReader.MoveX;
        float currentSpeed = _inputReader.IsRunKeyDown ? _speed * _runMultiplier : _speed;

        if (_landedCheck.IsGrounded)
            _rigidbody2D.velocity = new Vector2(moveX * currentSpeed, _rigidbody2D.velocity.y);

        IsMoved = moveX != 0;
        IsRunning = _inputReader.IsRunKeyDown;

        Turn(moveX);
    }

    private void Turn(float moveX)
    {
        if (moveX < 0)
            transform.localScale = _leftScale;
        else if (moveX > 0)
            transform.localScale = _rightScale;
    }
}
