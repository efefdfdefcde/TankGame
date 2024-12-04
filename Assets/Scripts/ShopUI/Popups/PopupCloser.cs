using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop
{
    public class PopupCloser : MonoBehaviour
    {
        public Subject<Unit> _panelCloseEvent = new();

        [SerializeField] private Button _close;

        private void Awake() => _close.onClick.AddListener(Close);

        private void Close() => _panelCloseEvent?.OnNext(Unit.Default);

        private void OnDestroy() => _close.onClick.RemoveListener(Close);
        

    }
}