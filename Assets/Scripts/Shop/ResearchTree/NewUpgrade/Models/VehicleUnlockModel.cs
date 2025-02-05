using Assets.Scripts.Architecture;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Models
{
    public class VehicleUnlockModel : UpgradeModel
    {
        private VehicleData _unlockData;

        public VehicleUnlockModel(UpgradeStatusDictonary status, UpgradeView view, VehicleData data, string key, VehicleData unlockData) : base(status, view, data, key)
        {
            _unlockData = unlockData;
        }

        public override void Upgrade(int price)
        {
           
            base.Upgrade(price);
            _unlockData._isAwailable = true;
        }
    }
}