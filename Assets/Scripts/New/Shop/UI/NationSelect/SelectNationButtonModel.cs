using R3;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New.Shop.UI.NationSelect
{
    public class SelectNationButtonModel 
    {
    
        private NationName _nationName;
        private SelectNationButtonController _controller;
        private SelectNationButtonView _view;

        private CompositeDisposable _disposables = new();

        public SelectNationButtonModel(SelectNationButtonController controller, NationName nationName,SelectNationButtonView view)
        {
            _view = view;
            _nationName = nationName;
            _controller = controller;
            _controller._selectEvent.Subscribe(_ => SetNation()).AddTo(_disposables);
            New.Arhitecture.EventBus.Instance._selectNation.Subscribe(name => UnSelect(name)).AddTo(_disposables);
            New.Arhitecture.EventBus.Instance._selectNation.Subscribe(name => Select(name)).AddTo(_disposables);
        }

        private void SetNation()
        {
            New.Arhitecture.EventBus.Instance._selectNation.OnNext(_nationName);
        }

        private void Select(NationName nationName)
        {
            if (nationName == _nationName)
            {
                _view.Select();
            }
        }

        private void UnSelect(NationName nationName)
        {
            if(nationName != _nationName)
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