using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.New.Shop.PartsSO.Shells.ShellForm
{
    public class ShellFormController 
    {
        public Subject<float> _valueChanged = new();
        public Subject<int> _buttonClick = new();

        private Slider _slider;
        private Button _plus;
        private Button _minus;
        
        public ShellFormController(Slider slider,Button plus, Button minus,int count,int maxCount)
        {
            _slider = slider;
            _slider.wholeNumbers = true;
            _slider.minValue = 0;
            _slider.maxValue = maxCount;
            _slider.value = count;
            _slider.onValueChanged.AddListener(value => _valueChanged.OnNext(value));
            _plus = plus;
            _plus.onClick.AddListener(ButtonClickPlus);
            _minus = minus;
            _minus.onClick.AddListener(ButtonClickMinus);
        }

        private void ButtonClickPlus()
        {
            _buttonClick.OnNext(1);
        }

        private void ButtonClickMinus()
        {
            _buttonClick.OnNext(-1);
        }

        public void CountUpdate(int count)
        {
            _slider.value = count;
        }

        public void OnDestroy()
        {
            _slider.onValueChanged.RemoveAllListeners();
            _minus.onClick.RemoveAllListeners();
            _plus.onClick.RemoveAllListeners();
        }
    }
}