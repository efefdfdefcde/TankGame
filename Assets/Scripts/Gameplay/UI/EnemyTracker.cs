using Assets.Scripts.TankParts.Player;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class EnemyTracker : MonoBehaviour
    {
        [SerializeField] private EnemyScaner _scaner;

        private Collider _enemy;

        [Inject]
        private void Construct()
        {
            _scaner._closestEnemyAction += SetEnemy;
        }

        private void Update()
        {
            TrackEnemy();
        }

        private void SetEnemy(Collider enemy)
        {
            _enemy = enemy;
            if (enemy)
            {
                TrackEnemy();
                if(!gameObject.activeSelf)gameObject.SetActive(true);
            }else gameObject.SetActive(false);
           
        }

        private void TrackEnemy()
        {
            transform.position = _enemy.transform.position; 
        }

        private void OnDestroy()
        {
            _scaner._closestEnemyAction -= SetEnemy;
        }
    }
}