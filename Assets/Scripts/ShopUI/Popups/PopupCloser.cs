using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop
{
    public class PopupCloser : MonoBehaviour
    {
       

        [SerializeField] private Button _close;

        private void Awake() => _close.onClick.AddListener(Close);

        private void Close() => EventBus.Instance._panelCloseEvent?.OnNext(Unit.Default);

        private void OnDestroy() => _close.onClick.RemoveListener(Close);
        

    }
}