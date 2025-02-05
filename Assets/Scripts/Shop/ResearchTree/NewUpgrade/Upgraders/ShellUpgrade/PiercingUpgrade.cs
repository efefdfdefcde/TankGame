using Assets.Scripts.Architecture;
using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Upgraders.ShellUpgrade
{
    public class PiercingUpgrade : IShellUpgrade
    {
        public void Upgrade(ShellUpgradeStruct shell, VehicleData data)
        {
            var shellUp = data._shellInfo[shell._type];
            shellUp._shellPenetration += shell._upgradeValue;
        }
    }
}