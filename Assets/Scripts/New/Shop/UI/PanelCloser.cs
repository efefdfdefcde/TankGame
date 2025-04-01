using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.New.Shop.UI
{
    public class PanelCloser : MonoBehaviour
    {
    

        [SerializeField] private Button _close;

        [Inject]
        private void Construct()
        {
            _close.onClick.AddListener(Close);
        }

        private void Close()
        {
            New.Arhitecture.EventBus.Instance._panelClose.OnNext(Unit.Default);
        }

        private void OnDestroy()
        {
            _close.onClick.RemoveAllListeners();
        }

    }
}