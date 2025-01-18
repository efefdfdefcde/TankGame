using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Popups;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Views
{
    public class ShellUnlockView : UpgradeView
    {
        private ShellData _shellData;

        public void Init(ShellData shell)
        {
            _shellData = shell;
        }

        protected override void PopupInit()
        {
            base.PopupInit();
            if (_popup is ShellUnlockPopup)
            {
                ShellUnlockPopup popup = (ShellUnlockPopup)_popup;
                popup.Init(_shellData);
            }
            else throw new InvalidCastException($"Cannot downcast {_popup.GetType().Name} to ShellUnlockPopup");
        }
    }
}