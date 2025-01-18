using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Models
{
    public class VehicleUnlockModel : UpgradeModel
    {
        public VehicleUnlockModel(UpgradeStatusDictonary status, UpgradeView view, VehicleData data) : base(status, view, data)
        {
        }

        public override void Upgrade(int price)
        {
            _data._isAwailable = true;
            base.Upgrade(price);          
        }
    }
}