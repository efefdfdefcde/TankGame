using Assets.Scripts.New.Arhitecture;
using R3;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.New.Shop.UI
{
    public class ShopMenuPanel : MonoBehaviour
    {
        [SerializeField] private ShopMenuButton _button;

        private GameObject _panel;
        private CompositeDisposable _disposable = new();

        [Inject]
        private void Construct()
        {
            _panel = gameObject;
            _button._openPanelEvent.Subscribe(_ => Arhitecture.EventBus.Instance._openPanel.OnNext(_panel)).AddTo(_disposable);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}