using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerAttackHitbox _hitbox;

    private int _damage;
    private float _knockbackForce;

    private void OnEnable()
    {
        _hitbox.OnHit += InflictDamage;
    }

    private void OnDisable()
    {
        _hitbox.OnHit -= InflictDamage;
    }

    public void Attack(int damage, float knockbackForce, float attackDelay, float attackHitboxLifetime)
    {
        _damage = damage;
        _knockbackForce = knockbackForce;

        StartCoroutine(AttackRoutine(attackDelay, attackHitboxLifetime));
    }

    private void InflictDamage(IDamageable iDamageable, Transform targetTransform)
    {
        Vector2 knockbackDirection = (targetTransform.position - transform.position).normalized;

        iDamageable.TakeDamage(_damage, knockbackDirection, _knockbackForce);
    }

    private IEnumerator AttackRoutine(float attackDelay, float attackHitboxLifetime)
    {
        yield return new WaitForSeconds(attackDelay);

        _hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(attackHitboxLifetime);

        _hitbox.gameObject.SetActive(false);
    }
}
