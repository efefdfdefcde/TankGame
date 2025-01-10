using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Shop.ResearchTree
{
    public class VenicleDemonstrator : MonoBehaviour
    {

        [SerializeField] private ResearchInfoPopup _infoPopup;
        [SerializeField] private GameObject _playerView;
        [SerializeField] private GameObject _spawnPosition;
        [SerializeField] private Button _closeButton;
        [SerializeField] private PopupSwitcher _popupSwitcher;
        [SerializeField] private GameObject freeLookCam;

        private GameObject _venicleView;
        private CompositeDisposable _disposable = new();

        [Inject]
        private void Construct()
        {
            EventBus.Instance._panelCloseEvent.Subscribe(_ => CloseDemonstration()).AddTo(_disposable);
            _closeButton.onClick.AddListener(CloseDemonstration);
            _infoPopup._demonstrateVenicleEvent.Subscribe(viewPrefab  => Demonstrate(viewPrefab)).AddTo(_disposable);
        }

        private void Demonstrate(GameObject viewPrefab)
        {
            _closeButton.gameObject.SetActive(true);
            EventBus.Instance._hidePopup?.OnNext(Unit.Default);
            _venicleView = Instantiate(viewPrefab, _spawnPosition.transform.position, viewPrefab.transform.rotation);
            _playerView.SetActive(false);
            freeLookCam.SetActive(true);
        }

        private void CloseDemonstration()
        {
            _closeButton.gameObject.SetActive(false);
            EventBus.Instance._showPopup?.OnNext(Unit.Default);
            Destroy(_venicleView);
            _playerView.SetActive(true);
            freeLookCam.SetActive(false);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(CloseDemonstration);
            _disposable.Dispose();
        }

    }
}