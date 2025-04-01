using R3;
using UnityEngine;

namespace Assets.Scripts.New.Shop.PartsSO.Shells.ShellForm
{
    public class ShellFormModel 
    {
        public Subject<(ShellType, int)> _countChange = new();
        
        private ShellFormController _controller;
        private ShellFormView _view;
        private ShellMenuInfo _menuInfo;

        private int _currentCount;
        private ShellType _type;

        private CompositeDisposable _disposable = new();

        public ShellFormModel(ShellFormController controller,ShellFormView view,int count,ShellType type,ShellMenuInfo menuInfo)
        {
            _menuInfo = menuInfo;
            _controller = controller;
            _view = view;
            _currentCount = count;
            _type = type;
            _controller._valueChanged.Subscribe(count => ChangeCount((int)count)).AddTo(_disposable);
            _controller._buttonClick.Subscribe(clickValue => _countChange.OnNext((_type,clickValue))).AddTo(_disposable);
        }

        private void ChangeCount(int count)
        {
            int updatedCount = count - _currentCount;
            _countChange.OnNext((_type,updatedCount));
        }

        public void CountUpdate(int count)
        {
            _currentCount = count;
            _menuInfo.CountUpdate(_currentCount);
            _controller.CountUpdate(_currentCount);
            _view.CountUpdate(_currentCount);
        }

        public void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}