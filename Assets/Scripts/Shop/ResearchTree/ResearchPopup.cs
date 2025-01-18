using Assets.Scripts.ShopUI.ResearchTree;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Shop.ResearchTree
{
    public class ResearchPopup : MonoBehaviour
    {
       

        [SerializeField] private ResearchInfoPopup _infoPopup;
        [SerializeField] private PopupSwitcher _switcher;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Transform _canvas;
        [SerializeField] private LocalResearchButton _localButton;
        

        private GameObject _researchPopup;
        private CompositeDisposable _disposable = new();

        [Inject]
        private DiContainer _container;

        [Inject]
        private void Construct()
        {
            _localButton._researchVenicleEvent.Subscribe(researchPrefab => SpawnResearchPopup(researchPrefab)).AddTo(_disposable);
            EventBus.Instance._panelCloseEvent.Subscribe(_ => DestroyPopup()).AddTo(_disposable);
            _infoPopup._researchVenicleEvent.Subscribe(researchPrefab => SpawnResearchPopup(researchPrefab)).AddTo(_disposable);
            _closeButton.onClick.AddListener(DestroyPopup);
        }

        private void SpawnResearchPopup(VehicleData researchPrefab)
        {
            _closeButton.gameObject.SetActive(true);
            EventBus.Instance._hidePopup.OnNext(Unit.Default);
            EventBus.Instance._showResearchP.OnNext(researchPrefab);
            _researchPopup = _container.InstantiatePrefab(researchPrefab._researchPrefab,_canvas);
        }

        private void DestroyPopup()
        {
            _closeButton.gameObject.SetActive(false);
            EventBus.Instance._showPopup.OnNext(Unit.Default);
            EventBus.Instance._hideResearchP.OnNext(Unit.Default);
            Destroy(_researchPopup);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

    }
}