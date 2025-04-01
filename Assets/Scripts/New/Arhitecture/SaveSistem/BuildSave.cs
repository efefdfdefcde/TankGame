using Assets.Scripts.New.Shop.PartsSO.Shells;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.New.Arhitecture.SaveSistem
{
    [Serializable]
    public class BuildSave 
    {
        public string _corpus;
        public string _turret;
        public string _cannon;
        public string _tracks;
        public string _name;
        public byte[] _icon;
        public float _battleRating;
        public int _shellStorageCapasity;
        public Dictionary<ShellType, int> _shellsStorage;
    }
}