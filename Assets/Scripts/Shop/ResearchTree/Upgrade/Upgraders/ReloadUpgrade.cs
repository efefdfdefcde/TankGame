using Assets.Scripts.Shop.ResearchTree.Upgraders;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.Upgrade.Upgraders
{
    public class ReloadUpgrade : IUprade
    {
        public void Upgrade(VenicleData data, int upgradeCount)
        {
            data._reloadSpeed += upgradeCount;
        }
    }
}