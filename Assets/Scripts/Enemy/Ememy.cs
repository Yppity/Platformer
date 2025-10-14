using UnityEngine;

[RequireComponent(typeof(Patroller))]

public class Ememy : MonoBehaviour
{
    [SerializeField] private Transform _firstPoinPatrolt;
    [SerializeField] private Transform _secondPointPatrol;
    [SerializeField] private float _speed = 2;

    private Patroller _patroller;

    private void Awake()
    {
        _patroller = GetComponent<Patroller>();
        _patroller.SetCurrentTarget(_firstPoinPatrolt);
    }

    private void FixedUpdate()
    {
        _patroller.Patrol(_firstPoinPatrolt, _secondPointPatrol, _speed);
    }
}
