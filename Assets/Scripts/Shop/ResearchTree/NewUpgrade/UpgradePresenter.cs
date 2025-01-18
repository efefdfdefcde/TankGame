using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Monos;
using R3;
using System;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade
{


    [Serializable]
    public class UpgradePresenter 
    {
        public Subject<Unit> _upgradeBought = new();

        private int _upgradeResearchPrice;
        private int _upgradeMoneyPrice;

        private UpgradeStatusDictonary _status; 
        private UpgradeModel _model;
        private UpgradeView _view;
        private UpgradeMono _mono;
        private int _researchP;
        private CompositeDisposable _disposables = new();

        public UpgradePresenter(UpgradeModel model, UpgradeView view, int researchPrice, int upgradePrice)
        {
            _model = model;
            _view = view;
            _model.ReseachP.Subscribe(reseachP => _researchP = reseachP).AddTo(_disposables);
            _model.Status.Subscribe(status => { _status = status; UpgradeCheck(); }).AddTo(_disposables);
            _view._buttonClickEvent.Subscribe(_ => UpgradeCheck()).AddTo(_disposables);
            _view._upgradeButtonEvent.Subscribe(_ => Upgrade()).AddTo(_disposables);   
            _upgradeResearchPrice = researchPrice;
            _upgradeMoneyPrice = upgradePrice;
        }

        public void Init(UpgradeMono mono)
        {
            if (mono)
            {
                _mono = mono;
                _mono._upgradeBought.Subscribe(_ => PreviousBought()).AddTo(_disposables);
            }
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
                    break;
                default:
                    _view.UpgradeNotAwailable();
                    break;
            }
        }

        private void PreviousBought()
        {
            _model.PreviousReseached();
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
                _upgradeBought.OnNext(Unit.Default);
                _upgradeBought.OnCompleted();
                EventBus.Instance._getExpirience.OnNext(_upgradeResearchPrice);
            }
            else _model.Research(_upgradeResearchPrice);
        }

        public void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}