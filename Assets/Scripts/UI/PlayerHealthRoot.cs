using System;
using System.Collections;
using UnityEngine;

namespace Assets
{
    public class PlayerHealthRoot : HealthRoot
    {
        public event Action<float> _damageEvent;

        protected override void OnDamage()
        {
            _damageEvent?.Invoke(_currentHealth);
        }

    }
}