using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.New.Shop.UI.BuildPopup
{
    public class BuildPopupView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _battleRating;
        [SerializeField] private Color _selectedColor;
        [SerializeField]private Color _unselectedColor;

        public void Select()
        {
            _icon.color = _selectedColor;
        }

        public void Unselect()
        {
            _icon.color = _unselectedColor;
        }

        public void UpdateBuildView(string name,Sprite icon,float battleRating)
        {
            _icon.sprite = icon;
            _name.text = name;
            _battleRating.text = battleRating.ToString();
        }
    }
}