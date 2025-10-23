using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class EnemyAttack : MonoBehaviour
{
    private int _attañkDamage;
    private float _knockbackVerticalOffset;
    private float _knockbackForce;

    public void Initialize(int attañkDamage, float knockbackVerticalOffset, float knockbackForce)
    {
        _attañkDamage = attañkDamage;
        _knockbackVerticalOffset = knockbackVerticalOffset;
        _knockbackForce = knockbackForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Vector2 knockbackDirection = (player.transform.position - transform.position).normalized + (Vector3.up * _knockbackVerticalOffset);

            player.TakeDamage(_attañkDamage, knockbackDirection, _knockbackForce);
        }
    }
}
