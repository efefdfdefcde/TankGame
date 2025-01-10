using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.Upgrade
{
    public enum UpgradeStatusDictonary
    {
        NotAvailable,
        Available,
        Researched,
        Bought
    }

    [Serializable]
    public struct StructUpgrade
    {
        public int _upgradeCount;
        public UpgradeDictonary _upgradeType;
    }

    public class Upgrade : MonoBehaviour
    {
        public Subject<(List<StructUpgrade>, string,Sprite,bool)> _upgradeNotAvailable = new();
        public Subject<(List<StructUpgrade>, string,Sprite,bool, int, Upgrade)> _upgradeAvailable = new();
        public Subject<(List<StructUpgrade>, string,Sprite, bool,int, Upgrade)> _upgradeResearched = new();
        public Subject<(string,Sprite,bool)> _upgradeBought = new();


        [SerializeField] private List<StructUpgrade> _upgradeTypes;
        [SerializeField] private Sprite _upgradeSprite;
        [SerializeField] private string _upgradeName;
        [SerializeField] private int _upgradeResearchPrice;
        [SerializeField] private int _upgradeMoneyPrice;
        [SerializeField] private UpgradeController _controller;
        [SerializeField] private UpgradeView _view;
        [SerializeField] private Upgrade _previousUpgrade;
        [SerializeField] private UpgradeStatusDictonary _upgradeStatus;
        [SerializeField] private bool _IsUpgrade;
        private CompositeDisposable _disposable = new();
        private IDisposable _upgradeBoughtSubscription;

        private void Start()
        {
            _controller._upgradeInfoEvent.Subscribe(_ => ShowUpgradeInfo()).AddTo(_disposable);
            if (_previousUpgrade) _upgradeBoughtSubscription = _previousUpgrade._upgradeBought.Subscribe(_ => UpgradeAvailable()).AddTo(_disposable);
        }

        private void ShowUpgradeInfo()
        {
            switch (_upgradeStatus)
            {
                case UpgradeStatusDictonary.Available:
                    _upgradeAvailable.OnNext((_upgradeTypes, _upgradeName ,_upgradeSprite,_IsUpgrade, _upgradeResearchPrice,this));
                    break;
                case UpgradeStatusDictonary.Researched:
                    _upgradeResearched.OnNext((_upgradeTypes, _upgradeName, _upgradeSprite, _IsUpgrade, _upgradeMoneyPrice, this));
                    break;
                case UpgradeStatusDictonary.Bought:
                    _upgradeBought.OnNext((_upgradeName,_upgradeSprite,_IsUpgrade));
                    break;
                default: _upgradeNotAvailable.OnNext((_upgradeTypes, _upgradeName, _upgradeSprite,_IsUpgrade));
                    break;
            }
        }

        private void UpgradeAvailable()
        {
            _upgradeStatus = UpgradeStatusDictonary.Available;
            _view.UpgradeAvailable();
            _upgradeBoughtSubscription?.Dispose();
        }

        public void UpgradeResearched()
        {
            _upgradeStatus = UpgradeStatusDictonary.Researched;
            _view.UpgradeResearched();
            _upgradeResearched.OnNext((_upgradeTypes, _upgradeName, _upgradeSprite, _IsUpgrade, _upgradeMoneyPrice, this));
        }

        public void UpgradeBought()
        {
            _upgradeStatus = UpgradeStatusDictonary.Bought;
            _view.UpgradeBought();
            _upgradeBought?.OnNext((_upgradeName, _upgradeSprite, _IsUpgrade));
            EventBus.Instance._getExpirience.OnNext(_upgradeResearchPrice);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();  
        }

       
    }
}