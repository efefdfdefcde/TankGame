using Assets.Scripts.Shop.ResearchTree.Upgrade.Upgraders;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop.ResearchTree.Upgrade
{
    public class UpgradeManagerView : MonoBehaviour
    {
        [SerializeField] private Dictionary<UpgradeDictonary, TextMeshProUGUI> _upgradesInfo = new();
        [SerializeField] private Image _upgradeImage;
        [SerializeField] private StructForDictonary[] _structsForDictonary;
        [SerializeField] private GameObject _popup;
        //data
        [SerializeField] private TextMeshProUGUI _upgradeName;
        [SerializeField] private TextMeshProUGUI _health;
        [SerializeField] private TextMeshProUGUI _armor;
        [SerializeField] private TextMeshProUGUI _speed;
        [SerializeField] private TextMeshProUGUI _engine;
        [SerializeField] private TextMeshProUGUI _turret;
        [SerializeField] private TextMeshProUGUI _reload;
        //Upgradebutton
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Color _buttonActiveColor;
        [SerializeField] private TextMeshProUGUI _buttonText;
        //Price
        [SerializeField] private Image _valueTypeImage;
        [SerializeField] private Sprite _cashSprite;
        [SerializeField] private Sprite _researchPointSprite;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private Color _cashC;
        [SerializeField] private Color _reserchPCol;

        [SerializeField] private GameObject _upgradeContainer;
        [SerializeField] private GameObject _shellContainer;

        private Color _buttonDisabledColor;
        private VenicleData _data;

        [Serializable]
        public struct StructForDictonary
        {
            public UpgradeDictonary _upgradeType;
            public TextMeshProUGUI _infoString;
        }

        public void Construct(VenicleData data)
        {
            _data = data;
            DataUpdate();
            _buttonDisabledColor = _buttonImage.color;
            foreach(var structs in _structsForDictonary)
            {
                _upgradesInfo.Add(structs._upgradeType, structs._infoString);
            }
        }

        public void DataUpdate()
        {
            _health.text = _data._health.ToString();
            _armor.text = _data._armor.ToString();
            _turret.text = _data._turretRotationSpeed.ToString();
            _engine.text = _data._enginePower.ToString();
            _speed.text = _data._speed.ToString();
            _reload.text = _data._reloadSpeed.ToString();
        }

        public void Init(List<StructUpgrade> info, string name, Sprite icon, bool _isShell)
        {
            ShowShellUpgrade(_isShell);
            _popup.SetActive(true);
            _upgradeImage.sprite = icon;
            ShowUpgradeInfo(info, name);
            ShowButtonStatus(false, "NotAvailable");
            ClearPrice();
        }

        public void Init((List<StructUpgrade> info, string name, Sprite icon, bool _isShell, int price) upgradeInfo, bool buttonStatus, bool _isMoney)
        {
            ShowShellUpgrade(upgradeInfo._isShell);
            if (!_isMoney)
            {
                ShowButtonStatus(buttonStatus, "Research");
                ShowPrice(upgradeInfo.price, false);
            }
            else
            {
                ShowButtonStatus(buttonStatus, "Buy");
                ShowPrice(upgradeInfo.price, true);
            }
            _popup.SetActive(true);
            _upgradeImage.sprite = upgradeInfo.icon;
            ShowUpgradeInfo(upgradeInfo.info, upgradeInfo.name);
        }

        public void Init(string name, Sprite icon, bool _isShell)
        {
            ShowShellUpgrade(_isShell);
            _popup.SetActive(true);
            ClearStrings();
            ShowButtonStatus(false, "Bought");
            _upgradeName.text = name;
            ClearPrice();
            _upgradeImage.sprite = icon;
        }

        private void ShowUpgradeInfo(List<StructUpgrade> infos, string name)
        {
            ClearStrings();
            foreach(var info in infos)
            {
                var upgradeString = _upgradesInfo[info._upgradeType];
                upgradeString.text = info._upgradeCount.ToString();
            }          
            _upgradeName.text = name;
        }

        private void ShowShellUpgrade(bool _isShell)
        {
           _upgradeContainer.SetActive(!_isShell);
           _shellContainer.SetActive(_isShell);

        }

        private void ShowPrice(int price,bool _isMoney)
        {
            _valueTypeImage.gameObject.SetActive(true);
            if(_isMoney)
            {
                _valueTypeImage.sprite = _cashSprite;
                _price.color = _cashC;
            }else
            {
                _valueTypeImage.sprite = _researchPointSprite;
                _price.color = _reserchPCol;
            }
            _price.text = price.ToString();
        }

        private void ClearPrice()
        {
            _valueTypeImage.gameObject.SetActive(false);
            _price.text = null;
        }

        private void ShowButtonStatus(bool buttonStatus, string buttonText)
        {
            if (buttonStatus) _buttonImage.color = _buttonActiveColor;
            else _buttonImage.color = _buttonDisabledColor;
            _buttonText.text = buttonText;
        }

        private void ClearStrings()
        {
            foreach(var strings in _upgradesInfo) strings.Value.text = string.Empty;
        }
    }
}