using System;
using UnityEngine;

public abstract class HealthRoot : MonoBehaviour
{
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

    }

    protected virtual void OnDeath()
    {
        Destroy(transform.parent.gameObject);
    }


    
}
