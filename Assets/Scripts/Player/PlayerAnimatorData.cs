using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int Jump = Animator.StringToHash(nameof(Jump));
        public static readonly int IsMoved = Animator.StringToHash(nameof(IsMoved));
        public static readonly int IsRunning = Animator.StringToHash(nameof(IsRunning));
    }
}
