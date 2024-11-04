using System.Collections;
using UnityEngine;

namespace Assets
{
    public class EnemyHealthRoot : HealthRoot
    {
        [SerializeField] private EnemiesList _enemyType;

        protected override void OnDeath()
        {
            EventBus.Instance._enemyDeathAction?.Invoke(_enemyType);
            base.OnDeath();
        }
    }
}