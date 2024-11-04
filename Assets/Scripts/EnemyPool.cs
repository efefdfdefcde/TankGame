using Assets.Scripts.TankParts.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private List<EnemyData> _enemies; 

        public EnemyData GetFreeEnemy(EnemiesList type)
        {
            foreach(EnemyData enemy in _enemies)
            {
                if(!enemy.gameObject.activeSelf && enemy.GetEnemyType() == type)
                {
                    return enemy;   
                }
            }
            return null;
        }
    }
}