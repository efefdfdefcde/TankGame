using R3;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    public class PopupSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private GameObject freeLookCam;
        [SerializeField] private PopupCloser _closer;

        private GameObject _currentPanel;

        private void Awake()
        {
            ShopMenuPanel._panelShowEvent.Subscribe(panel => OpenUIPanel(panel)).AddTo(this);
            _closer._panelCloseEvent.Subscribe(_ => ToShop()).AddTo(this);
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
        
    }
}