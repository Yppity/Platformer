using UnityEngine;

public class PlayerFlipper : MonoBehaviour
{
    private readonly Quaternion _rightRotation = Quaternion.Euler(0, 180, 0);
    private readonly Quaternion _leftRotation = Quaternion.Euler(0, 0, 0);

    private bool _facingRight = false;

    public void Flip(float moveX)
    {
        if (moveX < 0 && _facingRight)
            Rotate(_leftRotation);

        else if (moveX > 0 && _facingRight == false)
            Rotate(_rightRotation);
    }

    private void Rotate(Quaternion rotation)
    {
        transform.rotation = rotation;
        _facingRight = !_facingRight;
    }
}