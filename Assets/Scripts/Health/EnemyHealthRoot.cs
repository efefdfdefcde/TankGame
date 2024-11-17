using System.Collections;
using UnityEngine;

namespace Assets
{
    public class EnemyHealthRoot : HealthRoot
    {
        [SerializeField] private EnemiesList _enemyType;

        protected override void OnDamage()
        {
            base.OnDamage();
        }

        protected override void OnDeath()
        {
            EventBus.Instance._enemyDeathAction?.Invoke(_enemyType);
            transform.parent.gameObject.SetActive(false);
        }
    }
}