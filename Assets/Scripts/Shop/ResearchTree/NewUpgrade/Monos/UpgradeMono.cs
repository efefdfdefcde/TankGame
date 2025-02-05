using Assets.Scripts.Architecture;
using R3;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Monos
{
    public class UpgradeMono : MonoBehaviour
    {
        public Subject<R3.Unit> _upgradeBought = new();

        protected UpgradePresenter _presenter;
        [SerializeField] protected UpgradeMono _previousUpgrade;

        [SerializeField] protected int _researchPrice;
        [SerializeField] protected int _moneyPrice;
        [SerializeField] protected VehicleData _vehicleData;

        [SerializeField] protected UpgradeStatusDictonary _status;

        [SerializeField] protected string _saveKey;
        private CompositeDisposable _disposable = new();

        [Inject]
        protected DiContainer _container;
        
        protected virtual void Start()
        {
            if (string.IsNullOrEmpty(_saveKey))
            {
                throw new InvalidOperationException("Key has not been initialized.");
            }
            _presenter.Init(_previousUpgrade);
            _presenter._upgradeBought.Subscribe(_ => _upgradeBought?.OnNext(R3.Unit.Default)).AddTo(_disposable);

        }

        [ContextMenu("GenerateKey")]
        private void GenerateKey()
        {
            _saveKey = Guid.NewGuid().ToString();
        }

        private void OnDestroy()
        {
            _presenter.OnDestroy();
        }
    }
}