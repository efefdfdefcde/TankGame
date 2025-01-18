using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Models;
using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Popups;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Views
{
    public class VehicleView : UpgradeView
    {
        private List<StructUpgrade> _upgradeList;


        public void Init(List<StructUpgrade> upgradeList)
        {
            _upgradeList = upgradeList;
        }

        protected override void PopupInit()
        {
            base.PopupInit();
            if (_popup is VehiclePopup) 
            {
                VehiclePopup vehiclePopup = (VehiclePopup)_popup;
                vehiclePopup.Init(_upgradeList);
            }
            else throw new InvalidCastException($"Cannot downcast {_popup.GetType().Name} to VehiclePopup");
           
        }
    }
}