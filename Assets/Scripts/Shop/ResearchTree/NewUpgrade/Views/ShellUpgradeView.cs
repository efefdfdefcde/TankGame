using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Models;
using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Popups;
using Assets.Scripts.Shop.Shells;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Views
{
    public class ShellUpgradeView : UpgradeView
    {
        private List<ShellUpgradeStruct> _shellList;
        private Dictionary<ShellType, List<ShellUpgradeStruct>> _shells = new();

        public void Init(List<ShellUpgradeStruct> list)
        {
            _shellList = list;
            Sort();
        }

        private void Sort()
        {
            foreach (var shell in _data._shellInfo)
            {
                List<ShellUpgradeStruct> filteredList = _shellList
                    .Where(str => str._type == shell.Key)
                    .ToList();
                if(filteredList.Count > 0)
                {
                    _shells.Add(shell.Key, filteredList);
                }                
            }
        }

        protected override void PopupInit()
        {
            base.PopupInit();
            if (_popup is ShellUpgradePopup)
            {
                ShellUpgradePopup popup = (ShellUpgradePopup)_popup;
                popup.Init(_shells);
            }
            else throw new InvalidCastException($"Cannot downcast {_popup.GetType().Name} to ShellUpgradePopup");
        }

        
    }
}