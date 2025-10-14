using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 5f;
    [SerializeField] private Vector3 _offset;

    private void LateUpdate()
    {
        Vector3 targetPosition = _target.position + _offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
    }
}
