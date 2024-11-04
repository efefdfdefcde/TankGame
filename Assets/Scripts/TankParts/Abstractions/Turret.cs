using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public event Action<Collider> _enemyAction;

    [SerializeField] private float _attackRadius;
    [SerializeField] private LayerMask _enemyMask;

    [SerializeField] protected float _rotationSpeed;//Construct

    private Collider _closestEnemy;
    private float _closestEnemyDistance = float.MaxValue;
    private Quaternion _startPosition;

    private void Start()
    {
        _startPosition = Quaternion.Euler(0f, 0f, 0f);
    }

    private void Update()
    {
        EnemyScan();
    }


    private void EnemyScan()
    {
        var enemyes = Physics.OverlapSphere(transform.position, _attackRadius, _enemyMask);
        if (enemyes.Length > 0)
        {
            if(_closestEnemy == null)
            {
                _closestEnemyDistance = float.MaxValue;
            }
            foreach (var enemy in enemyes)
            {
                float enemyDistanse = (enemy.transform.position - transform.position).magnitude;
                if (enemyDistanse < _closestEnemyDistance)
                {
                    _closestEnemy = enemy;
                    _closestEnemyDistance = enemyDistanse;
                }
            }
            TurretRotation();
            _enemyAction?.Invoke(_closestEnemy);
        }
        else TurretReturn();
    }

    private void TurretRotation()
    {
        Vector3 directionToTarget = _closestEnemy.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void TurretReturn()
    {
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _startPosition, _rotationSpeed * Time.deltaTime);
    }

 


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}
