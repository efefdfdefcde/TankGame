using Assets.Scripts.Architecture;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade.Popups
{
    public class VehicleUnlockPopup : UpgradePopup
    {

        [SerializeField] private TextMeshProUGUI _hp;
        [SerializeField] private TextMeshProUGUI _armor;
        [SerializeField] private TextMeshProUGUI _speed;
        [SerializeField] private TextMeshProUGUI _engine;
        [SerializeField] private TextMeshProUGUI _turretRotation;
        [SerializeField] private TextMeshProUGUI _reload;

        public override void Init(VehicleData data, Sprite icon, string name)
        {
            base.Init(data ,icon,name);
            _hp.text = data._health.ToString();
            _armor.text = data._armor.ToString();
            _speed.text = data._speed.ToString();
            _engine.text = data._enginePower.ToString();
            _turretRotation.text = data._turretRotationSpeed.ToString();
            _reload.text = data._reloadSpeed.ToString();
        }
    }
}