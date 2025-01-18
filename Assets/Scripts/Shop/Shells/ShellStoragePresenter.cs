using Assets.Scripts.Shop.ResearchTree;
using R3;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Shop.Shells
{
    public class ShellStoragePresenter : MonoBehaviour
    {
        public Subject<int> _shellsCountEvent = new();

        [SerializeField] private ShellStorageModel _model;
        [SerializeField] private ShellStorageView _view;

        private Dictionary<ShellType, ShellPopup> _popups;
        private VehicleData _data;
        private CompositeDisposable _disposable = new();



        private int _shellsCount;



        [Inject]
        private void Construct()
        {
            _model._setDataEvent.Subscribe(data => { _data = data; CalculateShells(); }).AddTo(_disposable);
            _view._listUpdate.Subscribe(popups => _popups = popups).AddTo(_disposable);
            EventBus.Instance._shellCountChanged.Subscribe(info => RecalculateShells(info)).AddTo(_disposable);
        }

        private void CalculateShells()
        {
            _shellsCount = 0;
            foreach (var shell in _data._shellInfo)_shellsCount += shell.Value._count;
            _shellsCountEvent.OnNext(_shellsCount);
        }

        private void RecalculateShells((int value, ShellType type) info)
        {
            if ((_shellsCount + info.value) > _data._shellStorageCapasity)
            {
                int count = (_shellsCount + info.value) - _data._shellStorageCapasity;
                count = info.value - count;
                var popup = _popups[info.type];
                var shell = _data._shellInfo[info.type];
                shell._count += count;
                popup.CountUpdate(shell._count);
            }
            else
            {
                var shell = _data._shellInfo[info.type];
                shell._count += info.value;
            }
            CalculateShells();
        }

       

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}