using Assets.Scripts.Architecture;
using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade
{
    public enum UpgradeStatusDictonary
    {
        NotAvailable,
        Available,
        Researched,
        Bought
    }

    [Serializable]
    public abstract class UpgradeModel 
    {

        public ReadOnlyReactiveProperty<UpgradeStatusDictonary> Status => _upgradeStatus;
        private readonly ReactiveProperty<UpgradeStatusDictonary> _upgradeStatus;

        [SerializeField]protected VehicleData _data;
        public ReadOnlyReactiveProperty<int> ReseachP => _researchP;
        private readonly ReactiveProperty<int> _researchP;

        private string _saveKey;
        private Dictionary<string, UpgradeStatusDictonary> _saveData;

        private CompositeDisposable _disposables = new();


        public UpgradeModel(UpgradeStatusDictonary status,UpgradeView view,VehicleData data, string key)
        {
            _data = data;
            _saveKey = key;
            _upgradeStatus = new(status);
            Status.Skip(1).Subscribe(status => Save(status)).AddTo(_disposables);
            _researchP = new(_data._researchPoints);
            view.Init(_data);
            EventBus.Instance._researchPUpdate.Subscribe(_ => _researchP.Value = _data._researchPoints).AddTo(_disposables);
        }

        [Inject]
        private void Construct(Dictionary<string, UpgradeStatusDictonary> saveData)
        {
            _saveData = saveData;
            if (_saveData.ContainsKey(_saveKey))
            {
                _upgradeStatus.Value = _saveData[_saveKey];
            }
        }

        public void PreviousReseached()
        {
            _upgradeStatus.Value = UpgradeStatusDictonary.Available;
        }

        public virtual void Upgrade(int price)
        {
            _upgradeStatus.Value = UpgradeStatusDictonary.Bought;
            EventBus.Instance._spendMoney.OnNext(price);
        }

        public void Research(int price)
        {
            _upgradeStatus.Value = UpgradeStatusDictonary.Researched;
            _data._researchPoints -= price;
            _researchP.Value = _data._researchPoints;
            EventBus.Instance._researchPUpdate.OnNext(Unit.Default);
        }

        private void Save(UpgradeStatusDictonary status)
        {

            if(_saveData.ContainsKey(_saveKey))
            {
                _saveData[_saveKey] = status;
            }
            else
            {
                _saveData.Add(_saveKey, status);
            }
        }

        public void OnDestroy()
        {
            _disposables.Dispose();
        }

    }
}