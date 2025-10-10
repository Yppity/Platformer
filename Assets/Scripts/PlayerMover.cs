using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMover : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";

    [SerializeField] private LandedCheck _landedCheck;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _runMultiplier = 2f;
    [SerializeField] private KeyCode _runKey = KeyCode.LeftShift;
    [SerializeField] private float _jumpForce = 20f;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    private Rigidbody2D _rigidbody2D;
    private float _currentSpeed;
    private Vector3 _leftScale = new Vector3(1, 1, 1);
    private Vector3 _rightScale = new Vector3(-1, 1, 1);
    private bool _isMoved;
    private bool _isRunning;
    private bool _jumpStarted;

    public bool IsMoved => _isMoved;
    public bool IsRunning => _isRunning;
    public bool JumpStarted => _jumpStarted;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Jump()
    {
        _jumpStarted = false;

        if (Input.GetKey(_jumpKey) && _landedCheck.IsLanded)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _jumpStarted = true;
        }
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw(HorizontalAxis);

        _isMoved = moveX != 0;
        _isRunning = Input.GetKey(_runKey);
        _currentSpeed = _isRunning ? _speed * _runMultiplier : _speed;

        if (_landedCheck.IsLanded)
            _rigidbody2D.velocity = new Vector2(moveX * _currentSpeed, _rigidbody2D.velocity.y);

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
