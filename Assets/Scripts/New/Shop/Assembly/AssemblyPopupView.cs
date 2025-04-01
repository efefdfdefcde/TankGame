using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.New.Shop.Assembly
{
    public class AssemblyPopupView : MonoBehaviour
    {
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _battleRating;
        [SerializeField] private Color _selectedColor;

        private Color _unselectedColor = Color.white;//Override

        public void SetPart(TankPartSO part)
        {
            _icon.sprite = part._icon;
            _battleRating.text = part._battleRating.ToString();
        }

        public void Select()
        {
            _frame.color = _selectedColor;
        }

        public void Unselect()
        {
            _frame.color = _unselectedColor;
        }
       
    }
}