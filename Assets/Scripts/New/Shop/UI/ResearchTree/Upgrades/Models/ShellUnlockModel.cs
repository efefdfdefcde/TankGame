using Assets.Scripts.New.Shop.PartsSO.Shells;
using Assets.Scripts.New.Shop.UI.NationSelect;
using Assets.Scripts.New.Shop.Upgrades;
using R3;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New.Shop.UI.ResearchTree.Upgrades.Models
{
    public class ShellUnlockModel : UpgradeModel
    {
        private ShellType _shell;
        private NationName _nation;

        public ShellUnlockModel(UpgradeStatusDictonary status, UpgradeView view, NationStorage data, string key, NationName nation,ShellType shell) : base(status, view, data, key)
        {
            _shell = shell;
        }

        public override void Upgrade(int price)
        {
            base.Upgrade(price);
            New.Arhitecture.EventBus.Instance._shellAwailable.OnNext((_nation,_shell));
            New.Arhitecture.EventBus.Instance._shellsRespawn.OnNext(Unit.Default);
        }
    }
}