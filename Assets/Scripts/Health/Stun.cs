using System.Collections;
using UnityEngine;

public class Stun : MonoBehaviour
{
    public bool IsStunned {  get; private set; } = false;

    public void ApplyStun(float stunDuration)
    {
        StartCoroutine(StunRoutine(stunDuration));
    }

    private IEnumerator StunRoutine(float stunDuration)
    {
        IsStunned = true;

        yield return new WaitForSeconds(stunDuration);

        IsStunned = false;
    }
}
