using Assets.Scripts.New.Shop.Assembly.Parts;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New.Shop
{
    [CreateAssetMenu(fileName = "Tracks", menuName = "ScriptableObjects/Tracks", order = 7)]
    public class Tracks : TankPartSO
    {
        public TracksPart _shopPrefab;
        public TracksPart _gameplayPrefab;
        public int _maxSpeed;
        public int _maxWeight;
        public int _turningSpeed;
        public int _enginePower;
    }
}