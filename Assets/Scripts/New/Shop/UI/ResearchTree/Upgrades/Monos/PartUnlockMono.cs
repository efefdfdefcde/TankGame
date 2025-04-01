using Assets.Scripts.New.Shop.Upgrades.Models;
using Assets.Scripts.New.Shop.Upgrades.Views;
using UnityEngine;

namespace Assets.Scripts.New.Shop.Upgrades
{
    public class PartUnlockMono : UpgradeMono
    {
        private PartUnlockModel _model;
        [SerializeField] private PartUnlockView _view;
        [SerializeField] private TankPartSO _unlockData;

        protected override void Start()
        {
            //if(_unlockData._isAwailable)_status = UpgradeStatusDictonary.Bought;
            _model = new(_status,_view,_storage, _saveKey,_unlockData);
            _presenter = new(_model, _view, _researchPrice, _moneyPrice);
            base.Start();
         
            _container.Inject(_model);
        }

        private void OnDestroy()
        {
            _model.OnDestroy();
            _presenter.OnDestroy();
        }

    }
}