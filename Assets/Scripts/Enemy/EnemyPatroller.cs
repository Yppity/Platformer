using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyPatroller : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Transform _currentTarget;
    private float _targetReachThreshold = 0.1f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetCurrentTarget(Transform target)
    {
        _currentTarget = target;
    }

    public void Patrol(Transform firstPoint, Transform secondPoint, float speed)
    {
        if (_currentTarget == null)
            return;

        Vector2 direction = new Vector2(_currentTarget.position.x - transform.position.x, 0f).normalized;

        _rigidbody2D.velocity = direction * speed;

        if (Mathf.Abs(transform.position.x - _currentTarget.position.x) <= _targetReachThreshold)
            SwitchTarget(firstPoint, secondPoint);
    }

    private void SwitchTarget(Transform firstPoint, Transform secondPoint)
    {
        _currentTarget = _currentTarget == firstPoint ? secondPoint : firstPoint;
    }
}
