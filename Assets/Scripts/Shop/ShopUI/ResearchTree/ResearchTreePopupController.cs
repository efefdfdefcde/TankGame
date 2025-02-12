using R3;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.ShopUI
{
    public class ResearchTreePopupController : MonoBehaviour
    {
        public Subject<Unit> _switchPopupEvent = new();

        [SerializeField] private Button _button;

        [Inject]
        public void Construct()
        {
            _button.onClick.AddListener(SwitchPopup);
        }

        public void SwitchPopup()
        {
            _switchPopupEvent?.OnNext(Unit.Default);
        }

        public void OnDestroy()
        {
            _button.onClick.RemoveListener(SwitchPopup);
        }

    }
}