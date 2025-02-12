using Assets.Scripts.Controllers;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TankParts.Player
{
    public class EnemyScaner : MonoBehaviour
    {
        public event Action<Collider> _closestEnemyAction;

        [SerializeField] private float _attackRadius;
        [SerializeField] private float _unselectRadius;
        [SerializeField] private LayerMask _enemyMask;
        [SerializeField] private EnemySelector _selector;

        private Collider _closestEnemy;
        private bool _isEnemySelected;

        private void Start ()
        {
            _selector._selectedEnemy += SetSelectedEnemy;
        }

        private void Update()
        {
            EnemyScan();
            SelectedEnemyScan();
        }

        private void EnemyScan()
        {
            if (!_isEnemySelected)
            {
                var enemyes = Physics.OverlapSphere(transform.position, _attackRadius, _enemyMask);
                if (_closestEnemy && !_closestEnemy.gameObject.activeInHierarchy) _closestEnemy = null;
                if (enemyes.Length > 0)
                {

                    if (!_closestEnemy)
                    {
                        float _closestEnemyDistance = float.MaxValue;
                        foreach (var enemy in enemyes)
                        {
                            float enemyDistanse = (enemy.transform.position - transform.position).magnitude;
                            if (enemyDistanse < _closestEnemyDistance)
                            {
                                _closestEnemy = enemy;
                                _closestEnemyDistance = enemyDistanse;
                            }
                        }
                    }
                    else if ((_closestEnemy.transform.position - transform.position).magnitude > _attackRadius)
                    {
                        _closestEnemy = null;
                    }
                }
                else
                {
                    _closestEnemy = null;
                }
                _closestEnemyAction?.Invoke(_closestEnemy);
            }
        }

        private void SetSelectedEnemy(Collider selectedEnemy)
        {
            _isEnemySelected = true;
            _closestEnemy = selectedEnemy;
            _closestEnemyAction?.Invoke(_closestEnemy);
        }

        private void SelectedEnemyScan()
        {
            if (_isEnemySelected)
            {
                _closestEnemyAction?.Invoke(_closestEnemy);
                if (!_closestEnemy.gameObject.activeInHierarchy || (_closestEnemy.transform.position - transform.position).magnitude > _unselectRadius)
                {
                    _closestEnemy = null;
                    _isEnemySelected = false;
                }
            }
        }

        private void OnDestroy()
        {
            _selector._selectedEnemy -= SetSelectedEnemy;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _unselectRadius);
        }
    }
}