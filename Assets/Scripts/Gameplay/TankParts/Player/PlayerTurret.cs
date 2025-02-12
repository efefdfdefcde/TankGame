using Assets.Scripts.TankParts.Player;
using System;
using UnityEngine;

public class PlayerTurret : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private EnemyScaner _scaner;

    private Collider _closestEnemy;
    private Quaternion _startPosition = Quaternion.Euler(0f, 0f, 0f);

    private void Start()
    {
        _scaner._closestEnemyAction += SetClosestEnemy;
    }

    private void Update()
    {
        TurretRotation();
    }

    private void SetClosestEnemy(Collider closestEnemy)
    {
        _closestEnemy = closestEnemy;
    }

    private void TurretRotation()
    {
        if(_closestEnemy)
        {
            Vector3 directionToTarget = _closestEnemy.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _startPosition, _rotationSpeed * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        _scaner._closestEnemyAction -= SetClosestEnemy;
    }
}
