
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.New.Shop.Upgrades
{
    public class UpgradeMono : MonoBehaviour
    {
        public Subject<Unit> _upgradeBought = new();

        protected UpgradePresenter _presenter;
        [SerializeField] protected List<UpgradeMono> _previousUpgrade;

        [SerializeField] protected int _researchPrice;
        [SerializeField] protected int _moneyPrice;
        [SerializeField] protected NationStorage _storage;

        [SerializeField] protected UpgradeStatusDictonary _status;

        [SerializeField] protected string _saveKey;

        [Inject]
        protected DiContainer _container;
        
        protected virtual void Start()
        {
            if (string.IsNullOrEmpty(_saveKey))
            {
                throw new InvalidOperationException("Key has not been initialized.");
            }
            _presenter.Init(_previousUpgrade,this);

        }

        [ContextMenu("GenerateKey")]
        private void GenerateKey()
        {
            _saveKey = Guid.NewGuid().ToString();
        }
    }
}