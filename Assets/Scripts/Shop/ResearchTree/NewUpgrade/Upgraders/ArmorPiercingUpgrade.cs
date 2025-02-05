using Assets.Scripts.Architecture;
using Assets.Scripts.Shop.ResearchTree.Upgraders;
using Assets.Scripts.Shop.Shells;
using R3;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.Upgrade.Upgraders
{
    public class ArmorPiercingUpgrade : IUprade
    {
        public void Upgrade(VehicleData data, int upgradeCount = 0)
        {
            var shell = data._shellInfo[ShellType.ArmorPiercing];
            shell._isAllowed = true;
            EventBus.Instance._shellUpdate?.OnNext(Unit.Default);
        }
    }
}