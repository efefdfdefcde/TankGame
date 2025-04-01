using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.New.Shop.PartsSO.Shells
{
    public class ShellManagerPresenter 
    {
        public Subject<(ShellType, int)> _addShells = new();

        private int _storageCapasity;
        private int _curentCount;

        private ShellManagerModel _model;
        private ShellManagerView _view;
        private Dictionary<ShellType, ShellFormMono> _spawnedForms;

        private CompositeDisposable _disposables = new();
        private CompositeDisposable _shellFormDisposable;

        public ShellManagerPresenter(ShellManagerModel model,ShellManagerView view)
        {
            _view = view;
            _model = model;
            _spawnedForms = _model._spawnedForms;
            _model._spawned.Subscribe(_ => SpawnSubscribe()).AddTo(_disposables);
            _model._delete.Subscribe(_ => Unsubscribe()).AddTo(_disposables);
            _model.StorageCapacity.Subscribe(capasity => { _storageCapasity = capasity; _view.CapasityUpdate(_storageCapasity); }).AddTo(_disposables);
            _model.Count.Subscribe(count => { _curentCount = count; _view.CountUpdate(count); }).AddTo(_disposables);
        }

        private void SpawnSubscribe()
        {
            _shellFormDisposable = new();
            foreach(var shellForm  in _spawnedForms)
            {
                shellForm.Value._model._countChange.Subscribe(count => AddShells(count)).AddTo(_shellFormDisposable);
            }
        }

        private void Unsubscribe()
        {
            _shellFormDisposable?.Dispose();
        }

        private void AddShells((ShellType type, int count) countType)
        {
            if((countType.count + _curentCount) <= _storageCapasity)
            {
                _addShells.OnNext(countType);
            }
            else
            {
                int addCount = (countType.count + _curentCount) - _storageCapasity;
                addCount = countType.count - addCount;
                _addShells.OnNext((countType.type, addCount));
            }
        }
    }
}