using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.New.Shop.UI.ResearchTree
{
    public class PartButtonView : MonoBehaviour
    {
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Color _selectedColor;

        private Color _unselectedColor;

        private void Start()
        {
            _unselectedColor = _buttonImage.color;
        }

        public void Select()
        {
            _buttonImage.color = _selectedColor;
        }

        public void Unselect()
        {
            _buttonImage.color = _unselectedColor;
        }

    }
}