using Assets.Scripts.ShopUI.ResearchTree;
using R3;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.ShopUI
{
    public class ReserchTreeSwitcher : MonoBehaviour
    {
        [SerializeField] private ResearchTreePopupModel[] _treePopups;

        private ResearchTreePopupModel _currentPopup;

        [Inject]
        private void Construct()
        {
            for(int i = 0 ; i < _treePopups.Length; i++)
            {
                if(i == 0) SwitchPopup(_treePopups[i]);
                _treePopups[i]._switchSignalEvent.Subscribe(popup => SwitchPopup(popup)).AddTo(this);
            }
        }

        private void SwitchPopup(ResearchTreePopupModel popup)
        {
            if(_currentPopup != popup)
            {
                if (_currentPopup != null)_currentPopup.DeactivatePopup();
                _currentPopup = popup;
                _currentPopup.ActivatePopup();
            }
        }

    }
}