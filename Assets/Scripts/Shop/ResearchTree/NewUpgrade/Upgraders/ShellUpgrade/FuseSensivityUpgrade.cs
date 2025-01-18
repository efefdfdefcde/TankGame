using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Upgraders.ShellUpgrade
{
    public class FuseSensivityUpgrade : IShellUpgrade
    {
        public void Upgrade(ShellUpgradeStruct shell, VehicleData data)
        {
            var shellUp = data._shellInfo[shell._type];
            shellUp._fuseSensivity += shell._upgradeValue;
        }
    }
}