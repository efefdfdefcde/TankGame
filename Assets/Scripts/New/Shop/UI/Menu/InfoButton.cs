using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.New.Shop.UI.Menu
{
    public class InfoButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _popup;

        [Inject]
        private void Construct()
        {
            _button.onClick.AddListener(SendPopup);
        }

        private void SendPopup()
        {
            New.Arhitecture.EventBus.Instance._menuInfo.OnNext(_popup);
        }

        private void OnDestroy()
        {
            _button?.onClick.RemoveListener(SendPopup);
        }
    }
}