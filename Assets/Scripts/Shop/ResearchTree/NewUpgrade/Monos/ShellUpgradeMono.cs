using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Models;
using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Monos
{
    public class ShellUpgradeMono : UpgradeMono
    {
        private ShellUpgradeModel _model;
        [SerializeField] private ShellUpgradeView _view;

        [SerializeField] private List<ShellUpgradeStruct> _upgradeList;

        protected override void Start()
        {
            _model = new(_status,_view,_vehicleData,_upgradeList);
            _presenter = new(_model, _view, _researchPrice, _moneyPrice);
            base.Start();
        }
    }
}