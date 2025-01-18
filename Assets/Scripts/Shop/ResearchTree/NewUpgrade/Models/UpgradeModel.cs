using R3;
using System;
using System.Collections;
using UnityEngine;

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

        public UpgradeModel(UpgradeStatusDictonary status,UpgradeView view,VehicleData data)
        {
            _data = data;
            _upgradeStatus = new(status);
            _researchP = new(_data._researchPoints);
            view.Init(_data);
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

    }
}