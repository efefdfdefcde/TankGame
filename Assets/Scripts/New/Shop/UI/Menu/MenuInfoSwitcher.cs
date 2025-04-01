using R3;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.New.Shop.UI
{
    public class MenuInfoSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject _startPopup;

        private GameObject _currentPopup;

        private CompositeDisposable _disposables = new();

        [Inject]
        private void Construct()
        {
            _currentPopup = _startPopup;
            New.Arhitecture.EventBus.Instance._menuInfo.Subscribe(popup => SwitchPopup(popup)).AddTo(_disposables);
        }

        private void SwitchPopup(GameObject popup)
        {
            _currentPopup.SetActive(false);
            _currentPopup = popup;
            _currentPopup.SetActive(true);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

    }
}