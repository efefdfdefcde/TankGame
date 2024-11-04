using System.Collections;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = "EnemyRewardData", menuName = "ScriptableObjects/EnemyRewardData", order = 1)]
    public class EnemyRewardData : ScriptableObject
    {
        public EnemiesList _enemyName;
        public int _money;
        public int _reserchPoints;
       
    }
}