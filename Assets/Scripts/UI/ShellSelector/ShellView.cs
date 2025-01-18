using Assets.Scripts.Architecture;
using Assets.Scripts.Shop.ResearchTree;
using Assets.Scripts.TankParts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Shop.ResearchTree.VehicleData;

namespace Assets.Scripts.UI.ShellSelector
{
    public class ShellView : MonoBehaviour
    {
        [SerializeField] private Image _shellImage;
        [SerializeField] private Image _frame;
        [SerializeField] private TextMeshProUGUI _shellCount;

        private Color _selectColor;

        public void Construct(ShellData shellData, int startCount)
        {
            _shellImage.sprite = shellData._shellImage;
            _selectColor = shellData._selectColor;
            _shellCount.text = startCount.ToString();
        }

        public void UpdateCount(int count)
        {
            _shellCount.text = count.ToString();    
        }

        public void Select()
        {
            _frame.color = _selectColor;
        }

        public void Unselect()
        {
            Color color = _frame.color;
            color.a = 0;
            _frame.color = color;
        }
    }
}