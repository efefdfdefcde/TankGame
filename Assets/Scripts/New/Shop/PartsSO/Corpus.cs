using Assets.Scripts.New.Shop.Assembly.Parts;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New.Shop
{
    [CreateAssetMenu(fileName = "Corpus", menuName = "ScriptableObjects/Corpus", order = 4)]
    public class Corpus : TankPartSO
    {
        public int _health;
        public int _armor;
        public int _weight;
        public int _shellCapacity;
        public CorpusPart _shopPrefab;
        public CorpusPart _gameplayPrefab;
       
    }
}