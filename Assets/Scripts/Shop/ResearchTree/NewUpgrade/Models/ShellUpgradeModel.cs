using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Upgraders.ShellUpgrade;
using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Views;
using Assets.Scripts.Shop.Shells;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Models
{
    public enum ShellCharacteristic
    {
        Damage,
        Piercing,
        FuseSensivity
    }

    [Serializable]
    public struct ShellUpgradeStruct
    {
        public ShellType _type;
        public ShellCharacteristic _characteristic;
        public int _upgradeValue;
    }
    public class ShellUpgradeModel : UpgradeModel
    {
        private Dictionary<ShellCharacteristic, IShellUpgrade> _upraders = new()
        {
            {ShellCharacteristic.Damage, new DamageUpgrade() },
            {ShellCharacteristic.Piercing, new PiercingUpgrade() },
            {ShellCharacteristic.FuseSensivity, new FuseSensivityUpgrade() },
        };

        private List<ShellUpgradeStruct> _upgradeList;

        public ShellUpgradeModel(UpgradeStatusDictonary status, UpgradeView view, VehicleData data, List<ShellUpgradeStruct> upgradeList) : base(status, view, data)
        {
            _upgradeList = upgradeList;
            if (view is ShellUpgradeView)
            {
                ShellUpgradeView shellUpgradeView = (ShellUpgradeView)view;
                shellUpgradeView.Init(_upgradeList);
            }
            else throw new InvalidCastException($"Cannot downcast {view.GetType().Name} to ShellUpgradeView");
        }


        public override void Upgrade(int price)
        {
            foreach (var upgrade in _upgradeList)
            {
                var upgrader = _upraders[upgrade._characteristic];
                upgrader.Upgrade(upgrade, _data);
            }
            base.Upgrade(price);      
        }
    }
}