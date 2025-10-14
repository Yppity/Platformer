using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private bool _currentIsMoved = false;
    private bool _currentIsRunning = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Jump()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Jump);
    }

    public void SetIsMoved(bool isMoved)
    {
        if (_currentIsMoved == isMoved)
            return;

        _currentIsMoved = isMoved;
        _animator.SetBool(PlayerAnimatorData.Params.IsMoved, isMoved);
    }

    public void SetIsRunning(bool isRunning)
    {
        if (_currentIsRunning == isRunning)
            return;
         
        _currentIsRunning = isRunning;
        _animator.SetBool(PlayerAnimatorData.Params.IsRunning, isRunning);
    }
}
