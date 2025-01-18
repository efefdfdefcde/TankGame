using Assets.Scripts.Shop.ResearchTree.Upgraders;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.Upgrade.Upgraders
{
    public class EngineUpgrade : IUprade
    {
        public void Upgrade(VehicleData data, int upgradeCount)
        {
            data._enginePower += upgradeCount;
        }
    }
}