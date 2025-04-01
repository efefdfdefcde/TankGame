using R3;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New.Shop.Assembly
{
    public class AssemblyPopupModel 
    {
        private AssemblyPopupController _controller;
        private AssemblyPopupView _view;
        private TankPartSO _part;

        private CompositeDisposable _disposables = new();
        
        public AssemblyPopupModel(AssemblyPopupController controller, AssemblyPopupView view, TankPartSO part)
        {
            _controller = controller;
            _view = view;
            _part = part;
            _controller._select.Subscribe(_ => Select()).AddTo(_disposables);
            New.Arhitecture.EventBus.Instance._setPartEvent.Subscribe(part => Unselect(part)).AddTo(_disposables);
        }

        private void Select()
        {
            New.Arhitecture.EventBus.Instance._setPartEvent?.OnNext(_part);
            _view.Select();
        }

        private void Unselect(TankPartSO part)
        {
            if(_part != part)
            {
                _view.Unselect();
            }
        }

        public void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}