using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _maxHealth;
    private int _currentHealth;

    public event Action<int> HealthChanged;
    public event Action Died;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Initialize(int maxHealth)
    {
        _maxHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0)
            return;

        _currentHealth = Mathf.Max(_currentHealth - damage, 0);

        HealthChanged?.Invoke(_currentHealth);

        if (_currentHealth == 0)
            Died?.Invoke();
    }

    public void Heal(int value)
    {
        _currentHealth = Mathf.Min(_currentHealth + value, _maxHealth);

        HealthChanged?.Invoke(_currentHealth);
    }
}
