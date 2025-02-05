using Assets.Scripts.Shop.ResearchTree.Upgrade.Upgraders;
using Assets.Scripts.Shop.ResearchTree.Upgrade;
using Assets.Scripts.Shop.ResearchTree.Upgraders;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Views;
using Assets.Scripts.Architecture;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Models
{ 
    [Serializable]
    public struct StructUpgrade
    {
        public int _upgradeCount;
        public UpgradeDictonary _upgradeType;
    }
    [Serializable]
    public class VehicleModel : UpgradeModel
    {
        private Dictionary<UpgradeDictonary, IUprade> _upgraders = new()
        {
            {UpgradeDictonary.HP, new HPUpgrade() },
            {UpgradeDictonary.Armor, new ArmorUpgrade() },
            {UpgradeDictonary.Reload, new ReloadUpgrade() },
            {UpgradeDictonary.Speed, new SpeedUpgrade() },
            {UpgradeDictonary.Engine, new EngineUpgrade() },
            {UpgradeDictonary.Turret, new TurretUpgrade() },
        };

        

        private List<StructUpgrade> _upgradeList;
        private VehicleView _vehicleView;

        public VehicleModel(UpgradeStatusDictonary status, UpgradeView view, VehicleData data, string key, List<StructUpgrade> upgradeList) : base(status, view, data, key)
        {
            _upgradeList = upgradeList;
            if (view is VehicleView)
            {
                _vehicleView = (VehicleView)view;
                _vehicleView.Init(_upgradeList);
            }
            else throw new InvalidCastException($"Cannot downcast {view.GetType().Name} to VehicleView");
        }

        public override void Upgrade(int price)
        {
            foreach (var upgrade in _upgradeList)
            {
                var upgrader = _upgraders[upgrade._upgradeType];
                upgrader.Upgrade(_data, upgrade._upgradeCount);
            }
            base.Upgrade(price);       
        }
    }
}