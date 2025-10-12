using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class Coin : MonoBehaviour
{
    public event Action<Coin> PlayerCollisionCoin;

    private void Awake()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Collector collector))
        {
            collector.CoinCollected();

            PlayerCollisionCoin?.Invoke(this);
        }
    }
}
