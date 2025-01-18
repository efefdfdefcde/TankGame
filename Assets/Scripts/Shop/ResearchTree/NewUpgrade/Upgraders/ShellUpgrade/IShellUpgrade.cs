using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Upgraders.ShellUpgrade
{
    public interface IShellUpgrade
    {
        public void Upgrade(ShellUpgradeStruct shell, VehicleData data);
    }
}
