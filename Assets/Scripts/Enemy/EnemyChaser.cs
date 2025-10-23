using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]

public class EnemyChaser : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Player _targePlayer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(Player player)
    {
        _targePlayer = player;
    }

    public void ChasePlayer(float speed)
    {
        Vector2 direction = new Vector2(_targePlayer.transform.position.x - transform.position.x, 0f).normalized;
        _rigidbody2D.velocity = direction * speed;
    }
}
