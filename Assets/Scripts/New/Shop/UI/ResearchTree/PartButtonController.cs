using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.New.Shop.UI.ResearchTree
{
    public class PartButtonController 
    {
        public Subject<Unit> _selectPart = new();

        private Button _button;

        public PartButtonController(Button button)
        {
            _button = button;
            _button.onClick.AddListener(SelectPart);
        }

        private void SelectPart()
        {
            _selectPart.OnNext(Unit.Default);
        }

        public void OnDestroy()
        {
            _button.onClick.RemoveListener(SelectPart);
        }

    }
}