using Assets.Scripts.New.Shop.Assembly;
using Assets.Scripts.New.Shop.PartsSO.Shells.ShellForm;
using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.New.Shop.PartsSO.Shells
{
    public class ShellManagerModel 
    {
        public Subject<Unit> _spawned = new();
        public Subject<Unit> _delete = new();

        private ShellFormMono _form;
        private Transform _formParent;
        private ShellMenuInfo _menuForm;
        private Transform _menuParent;
        private ShellCatalog _catalog;
        private ShellManagerPresenter _presenter;

        private Build _currentBuild;
        private Dictionary<ShellType, (Shell, bool)> _nationShells;

        public ReadOnlyReactiveProperty<int> Count => _currentCount;
        private readonly ReactiveProperty<int> _currentCount = new();

        public ReadOnlyReactiveProperty<int> StorageCapacity => _shellStorageCapacity;
        private readonly ReactiveProperty<int> _shellStorageCapacity = new();

        public Dictionary<ShellType,ShellFormMono> _spawnedForms {get;private set;}
        private CompositeDisposable _disposables = new();

        public ShellManagerModel(ShellFormMono form, ShellMenuInfo menuForm,Transform parent, Transform menuParent, ShellCatalog catalog)
        {
            _menuForm = menuForm;
            _menuParent = menuParent;
            _form = form;
            _formParent = parent;
            _catalog = catalog;
            _spawnedForms = new();
            _catalog.NationShells.Subscribe(catalog => _nationShells = catalog).AddTo(_disposables);
            New.Arhitecture.EventBus.Instance._sendBuild.Subscribe(build => { _currentBuild = build._build; SpawnForms(); }).AddTo(_disposables);
            New.Arhitecture.EventBus.Instance._shellsRespawn.Subscribe(_ => SpawnForms()).AddTo(_disposables);
        }

        public void Init(ShellManagerPresenter presenter)
        {
            _presenter = presenter;
            _presenter._addShells.Subscribe(shells => AddShells(shells)).AddTo(_disposables);
        }

        private void SpawnForms()
        {
            DeleteForms();
            if(_currentBuild != null)
            {
                foreach (var shell in _currentBuild._cannon._shells)
                {
                    var shellT = _nationShells[shell._type];
                    if (shellT.Item2)
                    {
                        ShellFormMono form = UnityEngine.Object.Instantiate(_form, _formParent);
                        ShellMenuInfo menuform = UnityEngine.Object.Instantiate(_menuForm, _menuParent);
                        form.Init(shellT.Item1, shell, _currentBuild._shellsStorage[shell._type], _currentBuild._shellStorageCapasity, shell._type,menuform);
                        _spawnedForms.Add(shell._type, form);

                    }
                }
                _spawned.OnNext(Unit.Default);
                _shellStorageCapacity.Value = _currentBuild._shellStorageCapasity;
                CalculateShells();
            }
            else
            {
                _shellStorageCapacity.Value = 0;
                _currentCount.Value = 0;
            }
           
        }

        private void DeleteForms()
        {
            _delete.OnNext(Unit.Default);
            foreach (var form in _spawnedForms)
            {
                UnityEngine.Object.Destroy(form.Value._menuInfo.gameObject);
                UnityEngine.Object.Destroy(form.Value.gameObject);
            }
            _spawnedForms.Clear();
        }

        private void CalculateShells()
        {
            int count = 0;
            foreach(var shell in _currentBuild._shellsStorage)
            {
                count += shell.Value;
            }
            _currentCount.Value = count;
        }

        private void AddShells((ShellType type,int count) countType)
        {
            _currentBuild._shellsStorage[countType.type] += countType.count;
            _spawnedForms[countType.type]._model.CountUpdate(_currentBuild._shellsStorage[countType.type]);
            CalculateShells();
        }

        public void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}