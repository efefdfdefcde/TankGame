using R3;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.New.Shop.Upgrades
{


    [Serializable]
    public class UpgradePresenter 
    {

        private int _upgradeResearchPrice;
        private int _upgradeMoneyPrice;

        private UpgradeStatusDictonary _status; 
        private UpgradeModel _model;
        private UpgradeView _view;
        private List<UpgradeMono> _previousUpgrades;
        private UpgradeMono _mono;
        private int _researchP;
        private CompositeDisposable _disposables = new();
        private IDisposable _disposed ;

        

        public UpgradePresenter(UpgradeModel model, UpgradeView view, int researchPrice, int upgradePrice)
        {
            _model = model;
            _view = view;
            _model.ReseachP.Subscribe(reseachP => _researchP = reseachP).AddTo(_disposables);
            _view._buttonClickEvent.Subscribe(_ => UpgradeCheck()).AddTo(_disposables);
            _view._upgradeButtonEvent.Subscribe(_ => Upgrade()).AddTo(_disposables);   
            _upgradeResearchPrice = researchPrice;
            _upgradeMoneyPrice = upgradePrice;
        }

        public void Init(List<UpgradeMono> previousUpgrade,UpgradeMono mono)
        {
            _mono = mono;
            if(previousUpgrade != null && previousUpgrade.Count > 0)
            {
                _previousUpgrades = previousUpgrade;
                _disposed = New.Arhitecture.EventBus.Instance._upgradeBought.Subscribe(upgradeMono => PreviousBought(upgradeMono));
            }     
            _model.Status.Subscribe(status => { _status = status; UpgradeCheck(); }).AddTo(_disposables);
        }

        private void UpgradeCheck()
        {
            switch (_status)
            {
                case UpgradeStatusDictonary.Available:
                    ResearchCheck();
                    break;
                case UpgradeStatusDictonary.Researched:
                    BuyCheck();
                    break;
                case UpgradeStatusDictonary.Bought:
                    _view.UpgradeBought();
                    New.Arhitecture.EventBus.Instance._upgradeBought.OnNext(_mono);
                    _disposed?.Dispose();
                    break;
                default:
                    _view.UpgradeNotAwailable();
                    break;
            }
        }

        private void PreviousBought(UpgradeMono mono)
        {
            for (int i = _previousUpgrades.Count - 1; i >= 0; i--)
            {
                if (_previousUpgrades[i] == mono)
                {
                    _previousUpgrades.RemoveAt(i);

                    if (_previousUpgrades.Count == 0)
                    {
                        _model.PreviousReseached();
                        _disposed?.Dispose();
                    }
                }
            }


        }

        private void ResearchCheck()
        {
            bool button;
            if(_upgradeResearchPrice <= _researchP)button = true;
            else button = false;
            _view.UpgradeAvailable(button,_upgradeResearchPrice);
        }

        private void BuyCheck()
        {
            bool button;
            if(_upgradeMoneyPrice <= Bank._money)button = true;
            else button = false;
            _view.UpgradeResearched(button,_upgradeMoneyPrice);
        }

        private void Upgrade()
        {
            if (_status == UpgradeStatusDictonary.Researched) 
            {
                _model.Upgrade(_upgradeMoneyPrice); 
                New.Arhitecture.EventBus.Instance._getExpirience.OnNext(_upgradeResearchPrice);
                New.Arhitecture.EventBus.Instance._upgradeBought.OnNext(_mono);
            }
            else _model.Research(_upgradeResearchPrice);
        }

        public void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}