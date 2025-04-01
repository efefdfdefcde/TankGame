using R3;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.New.Shop.UI
{
    public class PopupSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private PanelCloser _panelCloser;
        [SerializeField] private GameObject _сamera;

        private GameObject _currentPanel;
        private CompositeDisposable _disposables = new();

        [Inject]
        private void Construct()
        {
            Arhitecture.EventBus.Instance._openPanel.Subscribe(panel => OpenPanel(panel)).AddTo(_disposables);
            New.Arhitecture.EventBus.Instance._panelClose.Subscribe(_ => Close()).AddTo(_disposables);
        }

        private void OpenPanel(GameObject panel)
        {
            if (_currentPanel)
            {
                _currentPanel.SetActive(false);
            }
            _menuPanel.SetActive(false);
            _currentPanel = panel;
            _currentPanel.SetActive(true);
            _сamera.SetActive(false);
        }

        private void Close()
        {
            if (_currentPanel)
            {
                _currentPanel.SetActive(false);
            }         
            _menuPanel.SetActive(true);
            _сamera.SetActive(true);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

    }
}