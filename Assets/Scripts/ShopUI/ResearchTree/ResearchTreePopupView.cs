using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.ShopUI.ResearchTree
{
    public class ResearchTreePopupView : MonoBehaviour
    {

        [SerializeField] private Color _switchColor;
        [SerializeField] private Image _back;
        [SerializeField] private GameObject _treePanel;

        private Color _startColor;

        [Inject]
        private void Construct()
        {
            _startColor = _back.color;
        }

        public void ActivatePopup()
        {
            _back.color = _switchColor;
            _treePanel.SetActive(true);
        }

        public void DeactivatePopup()
        {
            _back.color = _startColor;
            _treePanel.SetActive(false);
        }
    }
}