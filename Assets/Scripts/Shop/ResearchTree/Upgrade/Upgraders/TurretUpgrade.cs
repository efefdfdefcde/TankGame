using Assets.Scripts.Shop.ResearchTree.Upgraders;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.Upgrade.Upgraders
{
    public class TurretUpgrade : IUprade
    {
        public void Upgrade(VenicleData data, int upgradeCount)
        {
            data._turretRotationSpeed += upgradeCount;
        }
    }
}