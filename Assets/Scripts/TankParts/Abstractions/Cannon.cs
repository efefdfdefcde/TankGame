using System;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public event Action<float, float> _reloadAction;

    [SerializeField] private Turret _turret;
    [SerializeField] private Vector3 _boxSize;
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private float _shootPower;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private LayerMask _obtacleMask;

    
    [SerializeField] protected float _reloadTime;
    

    private float _reload;
    private bool _loaded;
    protected GameObject _currentBullet;

    private void Update()
    {
        Reload();
    }

    public virtual void EnemyScan(Collider closestEnemy)//Owerride
    {
        RaycastHit hit;
        Physics.BoxCast(_shootPoint.position,_boxSize,transform.forward,out hit,Quaternion.identity,_distance , _enemyMask);
        if (hit.collider == closestEnemy) 
        {
            Vector3 directionToEnemy = hit.transform.position - _shootPoint.position;
            if(!Physics.BoxCast(_shootPoint.position,_boxSize, directionToEnemy, Quaternion.identity,directionToEnemy.magnitude, _obtacleMask))
            {
                Shoot(hit); 
            }
        }
    }

    private void Shoot(RaycastHit hit)
    {
        if(_loaded)
        {
            GameObject bullet = Instantiate(_currentBullet, _shootPoint.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            Vector3 enemyVector = (hit.transform.position - _shootPoint.position).normalized;
            rb.AddForce(enemyVector * _shootPower);
            _loaded = false;
            _reload = 0;
            _reloadAction?.Invoke(_reload,_reloadTime);
        }
    }

    private void Reload()
    {
        if (!_loaded)
        {
            _reload += Time.deltaTime;
            _reloadAction?.Invoke(_reload,_reloadTime);
            if(_reload > _reloadTime)_loaded = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Matrix4x4 oldMatrix = Gizmos.matrix;

        
        Gizmos.matrix = transform.localToWorldMatrix;

       
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(_boxSize.x, _boxSize.y, _distance));

        Gizmos.matrix = oldMatrix;
    }

  
}
