using Assets.Scripts.New.Shop.Assembly.Parts;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New.Shop
{
    [CreateAssetMenu(fileName = "Turret", menuName = "ScriptableObjects/Turret", order = 6)]
    public class Turret : TankPartSO
    {
        public TurretPart _shopPrefab;
        public TurretPart _gameplayPrefab;
        public int _health;
        public int _armor;
        public float _turningSpeed;
        public int _weight;
    }
}