using Assets.Scripts.Shop.ResearchTree.Upgraders;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.Upgrade.Upgraders
{
    public class SpeedUpgrade : IUprade
    {
        public void Upgrade(VehicleData data, int upgradeCount)
        {
            data._speed += upgradeCount;
        }
    }
}