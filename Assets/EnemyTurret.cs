using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    [SerializeField] private float _detectionRadius;
    [SerializeField] private LayerMask _playerMask;

    private void Update()
    {
        
    }

    private void PlayerScan()
    {
        var player = Physics.OverlapSphere(transform.position, _detectionRadius, _playerMask);

    }

    private void TurretRotation()
    {
        
    }
}
