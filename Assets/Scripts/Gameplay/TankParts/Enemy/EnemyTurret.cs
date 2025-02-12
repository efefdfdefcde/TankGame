using System;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{

   
    [SerializeField] private LayerMask _playerMask;

    [SerializeField] private float _rotationSpeed;

    private Quaternion _startPosition = Quaternion.Euler(0f, 0f, 0f);

    public void Construct(float rotationSpeed)
    {
        _rotationSpeed = rotationSpeed;
    }

    public void TurretRotation(Collider _player)
    {
        Vector3 directionToTarget = _player.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    public void TurretReturn()
    {
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _startPosition, _rotationSpeed * Time.deltaTime);
    }
}
