using Assets.Scripts.Architecture;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop.ResearchTree
{
    public class ResearchInfoPopupView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _health;
        [SerializeField] private TextMeshProUGUI _speed;
        [SerializeField] private TextMeshProUGUI _enginePower;
        [SerializeField] private TextMeshProUGUI _turretRottationSpeed;
        [SerializeField] private TextMeshProUGUI _reloadSpeed;

        [SerializeField] private Button _selectButton;
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Color _awailableColor;
        [SerializeField] private Color _notAwailableColor;

        public void ButtonUpdate(bool status)
        {
            if (status)
            {
                try { _selectButton.enabled = true; }
                catch { NullReferenceException ex; }
               
                _buttonText.text = "Select";
                try { _buttonImage.color = _awailableColor; }
                catch { NullReferenceException ex; }
              
            }
            else
            {
                try { _selectButton.enabled = false; }
                catch { NullReferenceException ex; }
               
                _buttonText.text = "NotAwailable";
                try { _buttonImage.color = _notAwailableColor; }
                catch { NullReferenceException ex; }
               
            }
        }

        public void UpdateInfo(VehicleData data)
        {
            try { _icon.sprite = data._Icon; }
            catch { NullReferenceException ex; }         
            _name.text = data._name;
            _health.text = data._health.ToString();
            _speed.text = data._speed.ToString();
            _enginePower.text = data._enginePower.ToString();
            _turretRottationSpeed.text = data._turretRotationSpeed.ToString();
            _reloadSpeed.text = data._reloadSpeed.ToString();
        }

    }
}