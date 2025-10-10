using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class LandedCheck : MonoBehaviour
{
    private bool _isLanded = false;

    public bool IsLanded => _isLanded;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
            _isLanded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
            _isLanded = false;
    }
}
