using Assets.Scripts.Shop.Shells;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree
{
    [CreateAssetMenu(fileName = "VenicleData", menuName = "ScriptableObjects/VenicleData", order = 3)]
    public class VenicleData : ScriptableObject
    {
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


        public Dictionary<ShellType,(ShellDataShop _data, Architecture.ShellData _gameplayData)> _shellInfo = new();

        [Serializable]
        public class ShellDataShop
        {
            public bool _isAllowed;
            public string _name;
            public int _damage;
            public int _shellPenetration;
            public int _fuseSensivity;
            public int _count;
        }

        #region
        public StructForDictonary[] _structs;

        [Serializable]
        public struct StructForDictonary
        {
           
            public ShellType _shellType;
            public ShellDataShop _data;
            public Architecture.ShellData _gameplayData;
        }

        public void FillDictonary()
        {
            foreach(var structD in _structs)
            {
                _shellInfo.Add(structD._shellType,(structD._data,structD._gameplayData));
            }
        }
        #endregion
        //DictonaryFill
    }
}