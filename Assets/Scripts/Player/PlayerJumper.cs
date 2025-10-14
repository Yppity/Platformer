using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerJumper : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public bool TryJump(bool isGrounded, float jumpForce)
    {
        if (isGrounded == false)
            return false;

        _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        return true;
    }
}