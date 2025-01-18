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

        protected override void Start()
        {
            _model = new(_status,_view,_vehicleData);
            _presenter = new(_model, _view, _researchPrice, _moneyPrice);
            base.Start();
        }

    }
}