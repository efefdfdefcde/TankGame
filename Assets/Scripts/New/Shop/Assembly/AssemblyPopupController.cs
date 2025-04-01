using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.New.Shop.Assembly
{
    public class AssemblyPopupController 
    {
        public Subject<Unit> _select = new();

        private Button _button;

        public AssemblyPopupController(Button button)
        {
            _button = button;
            _button.onClick.AddListener(SelectPart);
        }
        

        private void SelectPart()
        {
            _select.OnNext(Unit.Default);
        }

        public void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}