using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Gameplay
{
    public class ToShopButton : MonoBehaviour
    {
        public Subject<Unit> _saveEvent = new();
       

        [SerializeField] private Button _button;

        [Inject]
        private void Construct()
        {
            _button.onClick.AddListener(ToShop);
        }

        private void ToShop()
        {
            _saveEvent.OnNext(Unit.Default);
            EventBus.Instance._toShopEvent.OnNext(Unit.Default);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

    }
}