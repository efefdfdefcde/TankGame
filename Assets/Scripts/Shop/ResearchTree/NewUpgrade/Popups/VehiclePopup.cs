using Assets.Scripts.Architecture;
using Assets.Scripts.Shop.ResearchTree.Upgrade;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Popups
{
    public class VehiclePopup : UpgradePopup
    {
        [SerializeField] private Dictionary<UpgradeDictonary, TextMeshProUGUI> _upgradesInfo = new();
        [SerializeField] private StructForDictonary[] _structsForDictonary;
        //data
        [SerializeField] private TextMeshProUGUI _health;
        [SerializeField] private TextMeshProUGUI _armor;
        [SerializeField] private TextMeshProUGUI _speed;
        [SerializeField] private TextMeshProUGUI _engine;
        [SerializeField] private TextMeshProUGUI _turret;
        [SerializeField] private TextMeshProUGUI _reload;

        [Serializable]
        public struct StructForDictonary
        {
            public UpgradeDictonary _upgradeType;
            public TextMeshProUGUI _infoString;
        }

        public override void Init(VehicleData data, Sprite icon, string name)
        {
            base.Init(data,icon,name);
            UpdateData();
            foreach(var Struct in _structsForDictonary)_upgradesInfo.Add(Struct._upgradeType, Struct._infoString);
        }

        public void Init(List<Models.StructUpgrade> upgradeList)
        {
            foreach(var upgrade in upgradeList)
            {
                var upgradeString = _upgradesInfo[upgrade._upgradeType];
                upgradeString.text = upgrade._upgradeCount.ToString();
            }
        }

        private void UpdateData()
        {
            _health.text = _data._health.ToString();
            _armor.text = _data._armor.ToString();
            _speed.text = _data._speed.ToString();
            _engine.text = _data._enginePower.ToString();
            _turret.text = _data._turretRotationSpeed.ToString();
            _reload.text = _data._reloadSpeed.ToString();
        }

        public override void UpgradeBought()
        {
            base.UpgradeBought();
            UpdateData();
            foreach (var str in _upgradesInfo)str.Value.text = null;
        }
    }
}