using R3;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New.Shop.UI.ResearchTree
{
    public class PartButtonModel 
    {
        private PartsNames _name;
        private PartButtonController _controller;
        private PartButtonView _view;

        private CompositeDisposable _disposable = new();

        public PartButtonModel(PartButtonController controller, PartsNames name, PartButtonView view)
        {
            _name = name;
            _controller = controller;
            _controller._selectPart.Subscribe(_ => SelectPart()).AddTo(_disposable);
            _view = view;
            New.Arhitecture.EventBus.Instance._selectPart.Subscribe(partName => Unselect(partName)).AddTo(_disposable);
            New.Arhitecture.EventBus.Instance._panelClose.Subscribe(_ => _view.Unselect()).AddTo(_disposable);
        }

        private void SelectPart()
        {
            New.Arhitecture.EventBus.Instance._selectPart.OnNext(_name);
            _view.Select();
        }

        private void Unselect(PartsNames partName)
        {
            if(partName != _name)
            {
                _view.Unselect();
            }
        }

        public void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}