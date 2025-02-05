using Assets.Scripts.Shop.ResearchTree.NewUpgrade;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Architecture.SaveSistem
{
    [Serializable]
    public class ShopExitParams 
    {
        public int _money;
        public int _gold;
        public int _level;
        public int _levelExperience;
        public string _currentVehicleWay;
        public Dictionary<string, UpgradeStatusDictonary> _upgradeStatus;
        public Dictionary<string, VehicleSave> _datas;
 

        public ShopExitParams()
        {
            _upgradeStatus = new();
          
        }
    }
}