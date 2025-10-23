using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]

public class KnockbackHandler : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D> ();
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.AddForce(direction * force, ForceMode2D.Impulse);
    }
}