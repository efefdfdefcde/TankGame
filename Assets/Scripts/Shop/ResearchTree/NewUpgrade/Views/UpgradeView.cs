using Assets.Scripts.Architecture;
using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop.ResearchTree.NewUpgrade
{
    public  class UpgradeView : MonoBehaviour
    {
        public Subject<Unit> _buttonClickEvent = new();
        public Subject<Unit> _upgradeButtonEvent = new();

        protected VehicleData _data;
        [SerializeField] protected UpgradePopup _popupPrefab;
        [SerializeField] private Transform _parent;

        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private Image _frame;
        [SerializeField] private Color _allowedRes;
        [SerializeField] private Color _allowedBuy;
        [SerializeField] private Color _bought;
        [SerializeField] private Button _button;

        protected UpgradePopup _popup;
        private CompositeDisposable _disposables = new();
        private IDisposable _popupDisposble;

        private void Start()
        {
            _button.onClick.AddListener(ShowPopup);
        }

        public void Init(VehicleData data)
        {
            _data = data;
            EventBus.Instance._spawnPopup.Subscribe(_ => DestroyPop()).AddTo(_disposables);
        }

        private void DestroyPop()
        {
            if (_popup)
            {
                Destroy(_popup.gameObject);
                _popupDisposble?.Dispose();
            }       
        }

        private void ShowPopup()
        {
            EventBus.Instance._spawnPopup.OnNext(Unit.Default);
            _popup = Instantiate(_popupPrefab,_parent);
            PopupInit();
            _buttonClickEvent.OnNext(Unit.Default);         
        }

        protected virtual void PopupInit()
        {
            _popup.Init(_data,_icon,_name);
            _popupDisposble = _popup._clickEvent.Subscribe(_ => UpgradeClick());
            _popupDisposble = _popup._closeEvent.Subscribe(_ => DestroyPop());
        }

        private void UpgradeClick() =>_upgradeButtonEvent.OnNext(Unit.Default);

        public void UpgradeNotAwailable()
        {
            if (_popup)
            {
                _popup.UpgradeNotAwailable();
            }         
        }

        public void UpgradeAvailable(bool button,int price)
        {
            if (_popup)
            {
                _popup.UpgradeAwailable(button, price);
            }   
            _frame.color = _allowedRes;
        }

        public void UpgradeResearched(bool button,int price)
        {
            if (_popup)
            {
                _popup.UpgradeResearched(button, price);
            }
            _frame.color = _allowedBuy;
        }

        public void UpgradeBought()
        {
            if(_popup)
            {
                _popup.UpgradeBought();              
            }
            _frame.color = _bought;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ShowPopup);
            _disposables.Dispose();
        }
    }
}