using Assets.Scripts.Architecture;
using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Models;
using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Views;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Monos
{
    public class VehicleUnlockMono : UpgradeMono
    {
        private VehicleUnlockModel _model;
        [SerializeField] private VehicleUnlockView _view;
        [SerializeField] private VehicleData _unlockData;

        protected override void Start()
        {
            if(_unlockData._isAwailable)_status = UpgradeStatusDictonary.Bought;
            _model = new(_status,_view,_vehicleData, _saveKey,_unlockData);
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