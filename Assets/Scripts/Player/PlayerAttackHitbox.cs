using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class PlayerAttackHitbox : MonoBehaviour
{
    public event Action<IDamageable, Transform> OnHit;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable iDamageable))
            OnHit?.Invoke(iDamageable, collision.transform);
    }
}