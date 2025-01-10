using Assets.Scripts.Shop.ResearchTree.Upgraders;
using Assets.Scripts.Shop.Shells;
using R3;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.Upgrade.Upgraders
{
    public class HightExplosiveUpgrade : IUprade
    {
        public void Upgrade(VenicleData data, int upgradeCount = 0)
        {
            var shell = data._shellInfo[Shells.ShellType.HighExplosive];
            shell._data._isAllowed = true;
            EventBus.Instance._shellUpdate?.OnNext(Unit.Default);

        }
    }
}