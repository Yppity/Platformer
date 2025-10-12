using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class GroundDetector : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
        IsGrounded = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
            IsGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
            IsGrounded = false;
    }
}
