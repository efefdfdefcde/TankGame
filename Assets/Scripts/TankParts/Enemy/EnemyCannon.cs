using UnityEngine;

public class EnemyCannon : Cannon
{

    [SerializeField] private GameObject _bullet;

    public void Construct(float reloadTime)
    {
        _reloadTime = reloadTime;
    }

    private void Start()
    {
        _currentBullet = _bullet;  
    }

}
