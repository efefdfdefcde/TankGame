using R3;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.ShopUI.ResearchTree
{
    public class ResearchTreePopupModel : MonoBehaviour
    {
        public Subject<ResearchTreePopupModel> _switchSignalEvent = new();

        [SerializeField] private ResearchTreePopupController _controller;
        [SerializeField] private ResearchTreePopupView _view;

        [Inject]
        private void Construct()
        {
            _controller._switchPopupEvent.Subscribe(_ => SwitchSignal()).AddTo(this);
        }

        private void SwitchSignal()
        {
            _switchSignalEvent?.OnNext(this);
        }

        public void ActivatePopup()
        {
            _view.ActivatePopup();
        }

        public void DeactivatePopup()
        {
            _view.DeactivatePopup();
        }
    }
}