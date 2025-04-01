using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.New.Shop.UI
{
    public class ShopMenuButton : MonoBehaviour
    {

        [SerializeField] private Button _openPanel;

        public Subject<Unit> _openPanelEvent = new();

        [Inject]
        private void Construct()
        {
            _openPanel.onClick.AddListener(OpenPanel);
        }

        private void OpenPanel()
        {
            _openPanelEvent.OnNext(Unit.Default);
          
        }

        private void OnDestroy()
        {
            _openPanel.onClick.RemoveAllListeners();
        }
    }
}