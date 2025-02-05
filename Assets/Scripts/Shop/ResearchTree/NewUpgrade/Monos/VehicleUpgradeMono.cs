using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Models;
using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Monos
{
    public class VehicleUpgradeMono : UpgradeMono
    {

        private VehicleModel _model;
        [SerializeField] private VehicleView _view;

        [SerializeField] private List<StructUpgrade> _upgradeList;



        protected override void Start()
        {
            _model = new(_status, _view,_vehicleData, _saveKey, _upgradeList);         
            _presenter = new(_model,_view,_researchPrice,_moneyPrice);
            base.Start();
            _container.Inject(_model);

        }

        private void OnDestroy()
        {
            _presenter.OnDestroy();
            _model.OnDestroy();
        }
    }
}