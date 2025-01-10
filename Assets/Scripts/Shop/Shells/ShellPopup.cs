using R3;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Shop.Shells
{
    public class ShellPopup : MonoBehaviour
    {
        

        [SerializeField] private Image _icon;
        [SerializeField] private Slider _countSlider;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _damage;
        [SerializeField] private TextMeshProUGUI _armorPiercing;
        [SerializeField] private TextMeshProUGUI _fuseSensivity;


        private int _count;
        private ShellType _type;
        

     
        public void Init(Sprite icon, string name, int damage, int armorPiercing, int fuseSensivity,int count, int maxCapasity,ShellType type)
        {
            _icon.sprite = icon;
            _name.text = name;
            _damage.text = damage.ToString();
            _armorPiercing.text = armorPiercing.ToString();
            _fuseSensivity.text = fuseSensivity.ToString();
            _type = type;
            _countSlider.minValue = 0;
            _countSlider.maxValue = maxCapasity;
            _countSlider.value = count;
            _countSlider.wholeNumbers = true;
            _countSlider.onValueChanged.AddListener(CountUpdate);
            _count = count;
            _countText.text = count.ToString();
        }

        private void CountUpdate(float value)
        {
            int changed = (int)value - _count; 
            _count = (int)value;
            _countSlider.value = _count;
            TextUpdate(_count.ToString());
            EventBus.Instance._shellCountChanged?.OnNext((changed,_type));         
        }

        public void CountUpdate(int value)
        {
            _count = value;
            _countSlider.value = _count;
            TextUpdate(_count.ToString());
        }

        private void TextUpdate(string text)
        {
            _countText.text = text;
        }

        private void OnDestroy()
        {
            _countSlider.onValueChanged.RemoveListener(CountUpdate);
        }
    }
}