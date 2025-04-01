using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New.Shop.Upgrades.Models
{
    public class PartUnlockModel : UpgradeModel
    {

        private TankPartSO _unlockPart;

        public PartUnlockModel(UpgradeStatusDictonary status, UpgradeView view, NationStorage data, string key, TankPartSO unlockPart) : base(status, view, data, key)
        {
            _unlockPart = unlockPart;
        }

        public override void Upgrade(int price)
        {

            base.Upgrade(price);
            _unlockPart._isAwailable = true;
        }
    }
}