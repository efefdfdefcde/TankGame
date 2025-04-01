using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.New.Shop.UI.BuildPopup
{
    public class BuildPopupController 
    {
        public Subject<Unit> _selectBuild = new();

        private Button _button;
        
        public BuildPopupController(Button button)
        {
            _button = button;
            _button.onClick.AddListener(SelectBuild);
        }

        private void SelectBuild()
        {
            _selectBuild.OnNext(Unit.Default);
        }

        public void OnDestroy()
        {
            _button.onClick.RemoveListener(SelectBuild);
        }
    }
}