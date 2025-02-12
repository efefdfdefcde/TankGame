using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Shells
{
    public class PlayerShell : Shell
    {
        protected override void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Enemy"))
            {
                collision.collider.GetComponent<EnemyHealthRoot>().TakeDamage(_damage);
            }
            base.OnCollisionEnter(collision);
        }

        public void SetCharacteristic(float damage, float shellPenetration, float fuseSensitivity)
        {
            _damage = damage;
            _shellPenetration = shellPenetration;
            _fuseSensitivity = fuseSensitivity;
        }
    }
}