using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Views;
using Assets.Scripts.Shop.ResearchTree.Upgrade.Upgraders;
using Assets.Scripts.Shop.ResearchTree.Upgraders;
using Assets.Scripts.Shop.Shells;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Models
{
    public class ShellUnlockModel : UpgradeModel
    {
        private Dictionary<ShellType, IUprade> _shellsUnlockList = new() 
        {
            {ShellType.ArmorPiercing, new  ArmorPiercingUpgrade() },
            {ShellType.HighExplosive, new HightExplosiveUpgrade() },
        };

        private ShellType _unlockType;

        public ShellUnlockModel(UpgradeStatusDictonary status, UpgradeView view, VehicleData data, ShellType unlockType) : base(status, view, data)
        {
            _unlockType = unlockType;
            if(view is ShellUnlockView)
            {
                ShellUnlockView shellUnlockView = (ShellUnlockView)view;
                var shell = _data._shellInfo[_unlockType];
                shellUnlockView.Init(shell);
            }
            else throw new InvalidCastException($"Cannot downcast {view.GetType().Name} to ShellUnlockView");
        }

        public override void Upgrade(int price)
        {
            base.Upgrade(price);
            var unlock = _shellsUnlockList[_unlockType];
            unlock.Upgrade(_data,0);
        }
    }
}