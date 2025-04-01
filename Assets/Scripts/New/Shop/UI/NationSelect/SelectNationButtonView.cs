using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.New.Shop.UI.NationSelect
{
    public class SelectNationButtonView : MonoBehaviour
    {

        [SerializeField] private Image _buttonImage;
        [SerializeField] private Color _selectColor;

        private Color _unselectColor = Color.white;

        public void Select()
        {
            _buttonImage.color = _selectColor;
        }

        public void Unselect()
        {
            _buttonImage.color = _unselectColor;
        }
    }
}