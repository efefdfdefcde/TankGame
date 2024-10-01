using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Turret _turret;
    [SerializeField] private Vector3 _boxSize;
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _reload;
    [SerializeField] private float _shootPower;
    [SerializeField] private Transform _shootPoint;

    private float _reloadTime;

    private void Start()
    {
        _turret._enemyAction += EnemyScan;
    }

    private void EnemyScan(Collider closestEnemy)
    {
        RaycastHit hit;
        Physics.BoxCast(transform.position,_boxSize,transform.forward,out hit,Quaternion.identity,_distance,_enemyMask);
        if (hit.collider == closestEnemy) Shoot(hit);
    }

    private void Shoot(RaycastHit hit)
    {
        if(Time.time > _reloadTime)
        {
            _reloadTime = Time.time + _reload;
            GameObject bullet = Instantiate(_bullet, _shootPoint.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            Vector3 enemyVector = (hit.transform.position - _shootPoint.position).normalized;
            rb.AddForce(enemyVector * _shootPower);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Matrix4x4 oldMatrix = Gizmos.matrix;

        // Устанавливаем матрицу Gizmos для текущего объекта
        Gizmos.matrix = transform.localToWorldMatrix;

        // Рисуем гизмо с учетом поворота объекта
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(_boxSize.x, _boxSize.y, _distance));

        // Восстанавливаем старую матрицу Gizmos
        Gizmos.matrix = oldMatrix;
    }

    private void OnDestroy()
    {
        _turret._enemyAction -= EnemyScan;
    }
}
