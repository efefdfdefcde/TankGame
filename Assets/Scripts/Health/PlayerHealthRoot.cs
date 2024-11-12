using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets
{
    public class PlayerHealthRoot : HealthRoot
    {

        public static PlayerHealthRoot _instance { get; private set; }


        [Inject]
        private void Construct()
        {
            _instance = this;
        }

        protected override void OnDamage()
        {
            base.OnDamage();
        }

        protected override void OnDeath()
        {
            transform.parent.gameObject.SetActive(false);
        }

    }
}