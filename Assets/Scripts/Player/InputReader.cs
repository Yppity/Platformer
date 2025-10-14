using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";

    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _runKey = KeyCode.LeftShift;

    public float MoveX { get; private set; }
    public bool IsRunKeyDown { get; private set; }

    public Action JumpKeyPressed;

    private void Update()
    {
        MoveX = Input.GetAxisRaw(HorizontalAxis);
        IsRunKeyDown = Input.GetKey(_runKey);

        if (Input.GetKeyDown(_jumpKey))
            JumpKeyPressed?.Invoke();
    }
}