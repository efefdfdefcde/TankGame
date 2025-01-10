using R3;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.ShopUI.ResearchTree
{
    public class ResearchTreePopupModel : MonoBehaviour
    {
        public static Subject<ResearchTreePopupModel> _switchSignalEvent = new();

        [SerializeField] private ResearchTreePopupController _controller;
        [SerializeField] private ResearchTreePopupView _view;

        private CompositeDisposable _disposable = new();

        [Inject]
        private void Construct()
        {
            _controller._switchPopupEvent.Subscribe(_ => SwitchSignal()).AddTo(_disposable);
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

        private void OnDestroy()
        {
            _disposable.Dispose();  
        }
    }
}