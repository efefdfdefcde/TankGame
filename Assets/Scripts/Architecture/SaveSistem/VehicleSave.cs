using Assets.Scripts.Shop.ResearchTree;
using Assets.Scripts.Shop.Shells;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Architecture.SaveSistem
{
    [Serializable]
    public class VehicleSave 
    {
        public bool _isAwailable;
        public string _name;
        public int _health;
        public float _armor;
        public float _speed;
        public float _enginePower;
        public float _turretRotationSpeed;
        public float _reloadSpeed;
        public int _researchPoints;
        public int _shellStorageCapasity;
        public Dictionary<ShellType, ShellSave> _shellInfo = new();

        public VehicleSave(VehicleData data)
        {
            _isAwailable = data._isAwailable;
            _name = data._name;
            _health = data._health;
            _armor = data._armor;
            _speed = data._speed;
            _enginePower = data._enginePower;
            _turretRotationSpeed = data._turretRotationSpeed;
            _reloadSpeed = data._reloadSpeed;
            _researchPoints = data._researchPoints;
            _shellStorageCapasity = data._shellStorageCapasity;
            foreach(var shellData in data._structs)
            {
                _shellInfo.Add(shellData._shellType,new ShellSave(shellData._data));
               
            }
        }

        public void SetParams(VehicleData data)
        {
            _isAwailable = data._isAwailable;
            _name = data._name;
            _health = data._health;
            _armor = data._armor;
            _speed = data._speed;
            _enginePower = data._enginePower;
            _turretRotationSpeed = data._turretRotationSpeed;
            _reloadSpeed = data._reloadSpeed;
            _researchPoints = data._researchPoints;
            _shellStorageCapasity = data._shellStorageCapasity;
            foreach(var shellData in data._shellInfo)
            {
                var shellSave = _shellInfo[shellData.Key];
                shellSave.SetParams(shellData.Value);
            }
        }
    }
}