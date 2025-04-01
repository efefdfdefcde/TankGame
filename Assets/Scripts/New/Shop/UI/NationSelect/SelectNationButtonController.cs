using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.New.Shop.UI
{
    public class SelectNationButtonController 
    {
        public Subject<Unit> _selectEvent = new();

        private Button _select;

        
        public SelectNationButtonController(Button select)
        {
            _select = select;
            _select.onClick.AddListener(Select);
        }

        private void Select()
        {
            _selectEvent.OnNext(Unit.Default);
        }

        public void OnDestroy()
        {
            _select.onClick.RemoveAllListeners();
        }
    }
}