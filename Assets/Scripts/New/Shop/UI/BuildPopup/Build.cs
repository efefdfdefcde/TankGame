using Assets.Scripts.New.Shop.PartsSO.Shells;
using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.New.Shop.Assembly
{
    public class Build 
    {
        public Corpus _corpus;
        public Turret _turret;
        public Cannon _cannon;
        public Tracks _tracks;
        public string _name;
        public Sprite _icon;
        public float _battleRating;
        public int _shellStorageCapasity;
        public Dictionary<ShellType, int> _shellsStorage;

        public void StorageUpdate()
        {
            _shellsStorage = new Dictionary<ShellType, int>();
            foreach(var shell in _cannon._shells)
            {
                _shellsStorage.Add(shell._type, 0);
            }
        }
    }
}