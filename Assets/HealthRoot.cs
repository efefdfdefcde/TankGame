using System;
using UnityEngine;

public class HealthRoot : MonoBehaviour
{
    [SerializeField] private float _maxHealth;


    private float _currentHealth;
    private bool _dead => _currentHealth <= 0;

    public static event Action _kill;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_dead)
        {
            _kill?.Invoke();
            Destroy(transform.parent.gameObject);
        }
    }
}
