using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Models;
using Assets.Scripts.Shop.ResearchTree.NewUpgrade.Views;
using Assets.Scripts.Shop.Shells;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Monos
{
    public class ShellUnlockMono : UpgradeMono
    {
        private ShellUnlockModel _model;
        [SerializeField] private ShellUnlockView _view;

        [SerializeField] private ShellType _type;

        protected override void Start()
        {
            _model = new(_status, _view, _vehicleData, _type);
            _presenter = new(_model, _view, _researchPrice, _moneyPrice);
            base.Start();
        }

    }
}