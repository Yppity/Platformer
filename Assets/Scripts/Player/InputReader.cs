using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";

    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _runKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _attackKey = KeyCode.F;

    public float MoveX { get; private set; }
    public bool IsRunKeyDown { get; private set; }

    public event Action JumpKeyPressed;
    public event Action AttackKeyPressed;

    private void Update()
    {
        MoveX = Input.GetAxisRaw(HorizontalAxis);
        IsRunKeyDown = Input.GetKey(_runKey);

        if (Input.GetKeyDown(_jumpKey))
            JumpKeyPressed?.Invoke();

        if (Input.GetKeyDown(_attackKey))
            AttackKeyPressed?.Invoke();
    }
}