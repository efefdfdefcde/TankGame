using Assets.Scripts.Shop.ResearchTree;
using R3;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    public class PopupSwitcher : MonoBehaviour
    {

        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private GameObject freeLookCam;
        [SerializeField] private PopupCloser _closer;
        [SerializeField] private UpgradePopupSpawner _researchPopup;
        [SerializeField] private VenicleSpawner _venicleDemonstrator;

        private GameObject _currentPanel;
        private CompositeDisposable _disposables = new();

        private void Awake()
        {
            EventBus.Instance._hidePopup.Subscribe(_ => HidePopup()).AddTo(_disposables);
            EventBus.Instance._showPopup.Subscribe(_ => ShowPopup()).AddTo(_disposables);
            ShopMenuPanel._panelShowEvent.Subscribe(panel => OpenUIPanel(panel)).AddTo(_disposables);
            EventBus.Instance._panelCloseEvent.Subscribe(_ => ToShop()).AddTo(_disposables);
        }


        private void OpenUIPanel(GameObject panel)
        {
            _menuPanel.SetActive(false);
            freeLookCam.SetActive(false);
            _currentPanel = panel;
            _currentPanel.SetActive(true);
        }

        private void ToShop()
        {
            if(_currentPanel)_currentPanel.SetActive(false);
            freeLookCam.SetActive(true);
            _menuPanel.SetActive(true);
            _currentPanel = null;
        }

        private void HidePopup()
        {
            if(_currentPanel)_currentPanel.SetActive(false);
            else _menuPanel.SetActive(false);
            freeLookCam.SetActive(false);
        }

        private void ShowPopup()
        {
            if(_currentPanel)_currentPanel.SetActive(true);
            else _menuPanel.SetActive(true);
            freeLookCam.SetActive(true);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }



    }
}