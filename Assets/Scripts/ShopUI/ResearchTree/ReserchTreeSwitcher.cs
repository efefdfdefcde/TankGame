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
        [SerializeField] private ResearchTreePopupModel _startPopup;

        private ResearchTreePopupModel _currentPopup;

        private CompositeDisposable _disposables = new();

        [Inject]
        private void Construct()
        {
            SwitchPopup(_startPopup);
            ResearchTreePopupModel._switchSignalEvent.Subscribe(popup  => SwitchPopup(popup)).AddTo(_disposables);
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

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

    }
}