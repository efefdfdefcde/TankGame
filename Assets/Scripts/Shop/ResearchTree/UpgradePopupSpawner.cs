using Assets.Scripts.Architecture;
using Assets.Scripts.Architecture.SaveSistem;
using Assets.Scripts.Shop.ResearchTree.NewUpgrade;
using Assets.Scripts.ShopUI.ResearchTree;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Shop.ResearchTree
{
    public class UpgradePopupSpawner : MonoBehaviour
    {
        public Subject<Unit> _upgradeInitialize = new();

        [SerializeField] private ResearchInfoPopup _infoPopup;
        [SerializeField] private PopupSwitcher _switcher;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Transform _canvas;
        [SerializeField] private LocalResearchButton _localButton;

        [SerializeField] private DataManagerShop _dataManager;
        

        private GameObject _researchPopup;
        private Dictionary<string, UpgradeStatusDictonary> _upgradeStatusDictonary;
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
            EventBus.Instance._nextPopupEvent.Subscribe(data => ChangePopup(data)).AddTo(_disposable);
            _dataManager._upgradeInitialize.Subscribe(upgrade => Init(upgrade)).AddTo(_disposable);
        }

        private void Start()
        {
            _upgradeInitialize.OnNext(Unit.Default);
            _upgradeInitialize.OnCompleted();
        }

        private void Init(Dictionary<string, UpgradeStatusDictonary> upgradeStatusDictonary)
        {
            _upgradeStatusDictonary = upgradeStatusDictonary;
            if (upgradeStatusDictonary != null)
            {
                _container.BindInstance(_upgradeStatusDictonary).AsSingle();
            }         
        }

        private void SpawnResearchPopup(VehicleData researchPrefab)
        {
            _closeButton.gameObject.SetActive(true);
            EventBus.Instance._hidePopup.OnNext(Unit.Default);
            EventBus.Instance._showResearchP.OnNext(researchPrefab);
            _researchPopup = _container.InstantiatePrefab(researchPrefab._researchPrefab,_canvas);
        }

        private void ChangePopup(VehicleData researchPrefab)
        {
            Destroy(_researchPopup);
            _researchPopup = _container.InstantiatePrefab(researchPrefab._researchPrefab, _canvas);
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