using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Shop.ResearchTree.Upgrade
{
    public class UpgradeManagerController : MonoBehaviour
    {
        public Subject<Unit> _upgradeEvent = new();

        [SerializeField] private Button _button;
        [SerializeField] private Button _close;
        [SerializeField] private GameObject _popup;

        
        private void Start()
        {
            _button.onClick.AddListener(Upgrade);
            _close.onClick.AddListener(Close);
        }

        private void Close()
        {
            _popup.SetActive(false);
        }

        private void Upgrade()
        {
            _upgradeEvent.OnNext(Unit.Default);
        }

        public void TurnStatus(bool status)
        {
            _button.gameObject.SetActive(status);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Upgrade);
            _close.onClick.RemoveListener(Close);
        }
    }
}