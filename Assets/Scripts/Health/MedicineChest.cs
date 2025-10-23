using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class MedicineChest : MonoBehaviour
{
    [SerializeField] private int _value = 1;

    public bool IsCollected { get; private set; } = false;
    public int Value => _value;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void Destroy()
    {
        IsCollected = true;
        gameObject.SetActive(false);
    }
}