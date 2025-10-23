using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class PlayerDetectionZone : MonoBehaviour
{
    public event Action<Player> PlayerEntered;
    public event Action PlayerExited;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            PlayerEntered?.Invoke(player);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
            PlayerExited?.Invoke();
    }
}
