using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TankParts.Enemy.Movement
{
    public class StuckTrigger : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;

        private bool _isBack;

        public event Action<int> _stuckAction;

        private void Update()
        {
            Debug.Log(_rb.linearVelocity.magnitude);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_rb.linearVelocity.magnitude < 1.3 && !_isBack)
            {
                _stuckAction?.Invoke(-1);
                _isBack = true;
            }          
        }

        private void OnTriggerExit(Collider other)
        {
            _stuckAction?.Invoke(-1);
            _isBack = false;
        }
    }
}