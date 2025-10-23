using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class Coin : MonoBehaviour
{
    public bool IsCollected { get; private set; } = false;

    public event Action<Coin> PlayerCollisionCoin;

    private void Awake()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    public void Reset()
    {
        IsCollected = false;
        gameObject.SetActive(true);
    }

    public void Destroy()
    {
        IsCollected = true;
        PlayerCollisionCoin?.Invoke(this);
    }
}
