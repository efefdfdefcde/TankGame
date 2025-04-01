using Assets.Scripts.New.Shop;
using Assets.Scripts.New.Shop.PartsSO.Shells;
using Assets.Scripts.New.Shop.UI.NationSelect;
using Assets.Scripts.New.Shop.Upgrades;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.New.Arhitecture.SaveSistem
{
    [Serializable]
    public class ShopExitParams 
    {
        public int _money;
        public int _gold;
        public int _level;
        public int _levelExperience;
        public Dictionary<string, UpgradeStatusDictonary> _upgradeStatus = new();
        public Dictionary<NationName, NationStorageSave> _nationDictonary;
        public NationName _currentNation;
        public Dictionary<string,bool> _partsStatus = new();
        public Dictionary<(int,NationName),BuildSave> _buildDictionary = new();
        public Dictionary<NationName, int> _selectedCells;
        public Dictionary<NationName, Dictionary<ShellType, bool>> _awailableShells;
    }
}