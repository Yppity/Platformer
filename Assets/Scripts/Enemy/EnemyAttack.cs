using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class EnemyAttack : MonoBehaviour
{
    private int _atta�kDamage;
    private float _knockbackVerticalOffset;
    private float _knockbackForce;

    public void Initialize(int atta�kDamage, float knockbackVerticalOffset, float knockbackForce)
    {
        _atta�kDamage = atta�kDamage;
        _knockbackVerticalOffset = knockbackVerticalOffset;
        _knockbackForce = knockbackForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Vector2 knockbackDirection = (player.transform.position - transform.position).normalized + (Vector3.up * _knockbackVerticalOffset);

            player.TakeDamage(_atta�kDamage, knockbackDirection, _knockbackForce);
        }
    }
}
