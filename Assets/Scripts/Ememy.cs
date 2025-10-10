using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Ememy : MonoBehaviour
{
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;
    [SerializeField] private float _speed = 2;

    private Rigidbody2D _rigidbody2D;
    private Transform _currentTarget;
    private float _targetReachThreshold = 0.1f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _currentTarget = _firstPoint;
    }

    private void FixedUpdate()
    {
        Patrol();
    }

    private void Patrol()
    {
        Vector2 direction = new Vector2(_currentTarget.position.x - transform.position.x, 0f).normalized;

        _rigidbody2D.velocity = direction * _speed;

        if (Mathf.Abs(transform.position.x - _currentTarget.position.x) <= _targetReachThreshold)
            SwitchTarget();
    }

    private void SwitchTarget()
    {
        _currentTarget = _currentTarget == _firstPoint ? _secondPoint : _firstPoint;
    }
}
