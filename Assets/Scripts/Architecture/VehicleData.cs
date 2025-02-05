using Assets.Scripts.Shop.ResearchTree;
using Assets.Scripts.Shop.Shells;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Architecture
{
    [CreateAssetMenu(fileName = "VenicleData", menuName = "ScriptableObjects/VenicleData", order = 3)]
    public class VehicleData : ScriptableObject
    {
        public bool _isAwailable;
        public string _name;
        public Sprite _Icon;
        public int _health;
        public float _armor;
        public float _speed;
        public float _enginePower;
        public float _turretRotationSpeed;
        public float _reloadSpeed;
        public int _researchPoints;
        public int _shellStorageCapasity;
        public GameObject _viewPrefab;
        public GameObject _researchPrefab;
        public GameObject _gameplayPrefab;


        public Dictionary<ShellType,ShellData> _shellInfo = new();

        #region
        public StructForDictonary[] _structs;

        [Serializable]
        public struct StructForDictonary
        {
           
            public ShellType _shellType;
            public ShellData _data;
        }

        public void FillDictonary()
        {
            foreach(var structD in _structs)
            {
                _shellInfo.Add(structD._shellType,structD._data);
            }
        }
        #endregion
        //DictonaryFill
    }
}