using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Shells
{
    public class EnemyShell : Shell
    {

        protected override void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                PlayerHealthRoot._instance.TakeDamage(_damage);
            }
            base.OnCollisionEnter(collision);
        }
    }
}