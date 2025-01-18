using Assets.Scripts.ShopUI.ResearchTree;
using R3;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Shop.ResearchTree
{
    public class ResearchInfoPopup : MonoBehaviour
    {
        public Subject<GameObject> _demonstrateVenicleEvent = new();
        public Subject<VehicleData> _researchVenicleEvent = new();

        [SerializeField] private ResearchInfoPopupView _view;
        [SerializeField] private ResearchInfoController _controller;
        [SerializeField] private GameObject _popup;

        private VehicleData _data;
        private CompositeDisposable _disposable = new();
        

        [Inject]
        private void Construct()
        {
            _controller._researchEvent.Subscribe(_ => ResearchVenicle()).AddTo(_disposable);
            _controller._demonstrateEvent.Subscribe(_ => DemonstrateVenicle()).AddTo(_disposable);
            _controller._closeEvent.Subscribe(_ => ClosePanel()).AddTo(_disposable);
            VenicleContainerModel._openInfoPanel.Subscribe(data => OpenPanel(data)).AddTo(_disposable);
            ResearchTreePopupModel._switchSignalEvent.Subscribe(_ => ClosePanel()).AddTo(_disposable);
            EventBus.Instance._panelCloseEvent.Subscribe(_ => ClosePanel()).AddTo(_disposable); 
        }

        private void OpenPanel(VehicleData data)
        {
            _popup.SetActive(true);
            _view.UpdateInfo(data);
            _data = data;
        }

        private void ResearchVenicle()
        {
            _researchVenicleEvent.OnNext(_data);
        }

        private void DemonstrateVenicle()
        {
            _demonstrateVenicleEvent?.OnNext(_data._viewPrefab);
        }

        private void ClosePanel()
        {
            _popup.SetActive(false);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }


    }
}