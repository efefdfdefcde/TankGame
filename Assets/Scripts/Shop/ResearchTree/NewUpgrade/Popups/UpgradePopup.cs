using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade
{
    public abstract class UpgradePopup : MonoBehaviour
    {
        public Subject<Unit> _clickEvent = new(); 
        public Subject<Unit> _closeEvent = new();

        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;

        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Color _buttonAllowed;
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private Button _closeButton;

        [SerializeField] private Image _cashImage;
        [SerializeField] private Sprite _cashSprite;
        [SerializeField] private Sprite _researchPSprite;
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private Color _cashColor;
        [SerializeField] private Color _researchColor;


        protected VehicleData _data;
        private Color _buttonNotAllowed;


        public virtual void Init(VehicleData data, Sprite icon, string name)
        {
            _icon.sprite = icon;
            _name.text = name;
            _button.onClick.AddListener(ButtonClick);
            _buttonNotAllowed = _buttonImage.color;
            _data = data;
            _closeButton.onClick.AddListener(Close);
        }

        private void ButtonClick() => _clickEvent.OnNext(Unit.Default);

        public void UpgradeNotAwailable()
        {
            ButtonSwitcher(false);
            _buttonText.text = "Not Awailable";
        }

        public void UpgradeAwailable(bool button, int price)
        {
            _cashImage.gameObject.SetActive(true);
            _cashImage.sprite = _researchPSprite;
            _valueText.color = _researchColor;
            _valueText.text = price.ToString();
            _buttonText.text = "Research";
            ButtonSwitcher(button);
        }

        public void UpgradeResearched(bool button, int price)
        {
            _cashImage.gameObject.SetActive(true);
            _cashImage.sprite = _cashSprite;
            _valueText.color = _cashColor;
            _valueText.text = price.ToString();
            _buttonText.text = "Buy";
            ButtonSwitcher(button);
        }

        public virtual void UpgradeBought()
        {
            _cashImage.gameObject.SetActive(false); 
            _valueText.text = null;
            _buttonText.text = "Bought";
            ButtonSwitcher(false);
        }

        private void ButtonSwitcher(bool status)
        {
            if (status)
            {
                _button.gameObject.SetActive(true);
                _buttonImage.color = _buttonAllowed;
            }
            else
            {
                _button.gameObject.SetActive(false);
                _buttonImage.color = _buttonNotAllowed;
            }
        }

        private void Close()
        {
            _closeEvent.OnNext(Unit.Default);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(Close);
            _button.onClick.RemoveListener(ButtonClick);
        }
    }
}