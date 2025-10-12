using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMover))]

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerMover _playerMover;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _playerMover.Jumped += HandleJumped;
    }

    private void Update()
    {
        _animator.SetBool("IsMoved", _playerMover.IsMoved);
        _animator.SetBool("IsRunning", _playerMover.IsRunning);
    }

    private void OnDisable()
    {
        _playerMover.Jumped -= HandleJumped;
    }

    private void HandleJumped()
    {
        _animator.SetTrigger("Jump");
    }
}
