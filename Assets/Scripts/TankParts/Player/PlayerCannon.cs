using Assets.Scripts.TankParts.Player;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TankParts
{
    public class PlayerCannon : Cannon
    {
        public event Action _shellSpend;

        [SerializeField] ShellManager _manager;
        [SerializeField] EnemyScaner _enemyScaner;

        private int _shellsCount;
        private bool _haveShells => _shellsCount > 0;

        private void Awake()
        {
            _manager._setShellsAction += SetShells;
            _enemyScaner._closestEnemyAction += EnemyScan;
        }//Zenject

        protected override void Update()
        {
            base.Update();

        }

        public override void EnemyScan(Collider closestEnemy)
        {
            if (closestEnemy)
            {
                base.EnemyScan(closestEnemy);
            }
            
        }

        protected override void Shoot(RaycastHit hit)
        {
            if (_loaded)
            {
                GameObject bullet = Instantiate(_currentShell, _shootPoint.position, Quaternion.identity);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                Vector3 enemyVector = (hit.transform.position - _shootPoint.position).normalized;
                rb.AddForce(enemyVector * _shootPower);
                _loaded = false;
                _reload = 0;
                _reloadAction?.Invoke(_reload, _reloadTime);
                _shellsCount--;
                Debug.Log(_shellsCount);
                _shellSpend?.Invoke();
            }
        }

        protected override void Reload()
        {
            if (_haveShells)
            {
               base.Reload();
            }   
        }

        private void SetShells(GameObject shell,int shellsCount)
        {
            _currentShell = shell;
            _shellsCount = shellsCount;
        }

        private void OnDestroy()
        {
            _manager._setShellsAction -= SetShells;
        }
    }
}