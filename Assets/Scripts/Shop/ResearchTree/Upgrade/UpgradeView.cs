using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop.ResearchTree.Upgrade
{
    public class UpgradeView : MonoBehaviour
    {
        [SerializeField] private Image _frame;
        [SerializeField] private Color _availableColor;
        [SerializeField] private Color _researchedColor;
        [SerializeField] private Color _boughtColor;
        [SerializeField] private Color _selectedColor;


        public void UpgradeAvailable()
        {
            _frame.color = _availableColor;
        }

        public void UpgradeResearched()
        {
            _frame.color = _researchedColor;
        }

        public void UpgradeBought()
        {
            _frame.color = _boughtColor;
        }
    }
}