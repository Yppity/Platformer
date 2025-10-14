using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public bool TryMove(float moveX, float speed, float runMultiplier, bool isRunKeyDown, bool isGrounded)
    {
        if (isGrounded == false || moveX == 0)
            return false;

        float currentSpeed = isRunKeyDown ? speed * runMultiplier : speed;

        _rigidbody2D.velocity = new Vector2(moveX * currentSpeed, _rigidbody2D.velocity.y);

        return true;
    }
}