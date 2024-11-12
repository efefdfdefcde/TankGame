using System;
using UnityEngine;

public abstract class HealthRoot : MonoBehaviour
{
    public event Action<float,float> _healthChangedAction;

    [SerializeField] private float _maxHealth;


    protected float _currentHealth;
    private bool _dead => _currentHealth <= 0;
     
    

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        OnDamage();
        if (_dead)
        {
           OnDeath();
        }
    }

    protected virtual void OnDamage()
    {
        _healthChangedAction?.Invoke(_maxHealth, _currentHealth);
    }

    protected virtual void OnDeath()
    {
        
    }


    
}
