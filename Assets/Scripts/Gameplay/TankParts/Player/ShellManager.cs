using Assets.Scripts.Architecture;
using Assets.Scripts.Shells;
using Assets.Scripts.Shop.ResearchTree;
using Assets.Scripts.UI.ShellSelector;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Assets.Scripts.TankParts.Player
{
    public class ShellManager : MonoBehaviour
    {
        public Subject<Unit> _shellManagerInit = new();
        public Subject<Unit> _shellEndEvent = new Subject<Unit>();
        public event Action _unselectAction;
        public event Action<GameObject,int> _setShellsAction;

        [SerializeField] private DataManagerGameplay _dataManager;
        [SerializeField] private GameObject _shellOrganiser;
        [SerializeField] private ShellConstructor _shellFormPrefab;
        [SerializeField] private PlayerCannon _playerCannon;

        private List<ShellData> _dataList = new();
        private Dictionary<ShellConroller, (ShellView, ShellData)> _shellsCatalog = new();
        private (ShellView shellView, ShellData shellData) _currentShell;
        private ShellConroller _startShell;

        private CompositeDisposable _disposables = new();

        [Inject]
        private void Construct()
        {
            _dataManager._shellManagerInit.Subscribe(data =>
            {
                foreach (var shellData in data._shellInfo)
                {
                    if (shellData.Value._isAllowed && shellData.Value._count > 0) _dataList.Add(shellData.Value);
                }
            }).AddTo(_disposables);
        }

        private void Start()
        {
            _shellManagerInit.OnNext(Unit.Default);
            _shellManagerInit.OnCompleted();
            ShellSpawn();
            _playerCannon._shellSpend += ShellSpend;
            SelectShell(_startShell);
        }

        private void ShellSpawn()
        {
            foreach (var shellData in _dataList)
            {
                var shellForm = Instantiate(_shellFormPrefab);
                shellForm.transform.SetParent(_shellOrganiser.transform);
                shellForm.ConstructView(shellData);
                var formViewController = shellForm.GetViewController();
                if (!_startShell) _startShell = formViewController.Item2;
                formViewController.Item1.UpdateCount(shellData._count);
                _shellsCatalog.Add(formViewController.Item2, (formViewController.Item1, shellData));
                formViewController.Item2._selectShellAction += SelectShell;
            }
        }

        private void SelectShell(ShellConroller controller)
        {
            if (_currentShell.shellView != null)
            {
                _currentShell.shellView.Unselect();
            }
            _currentShell = _shellsCatalog[controller];
            _currentShell.shellView.Select();
            ShellData shellData = _currentShell.Item2;
            GameObject shell = shellData._shellPrefab;
            var shellConstructor = shell.GetComponent<PlayerShell>();
            shellConstructor.SetCharacteristic(shellData._damage, shellData._shellPenetration, shellData._fuseSensivity); //Set Characteristic to prefab
            _setShellsAction?.Invoke(shell, _currentShell.shellData._count);
        }

        private void ShellSpend()
        {
            _currentShell.Item2._count--;
            if(_currentShell.Item2._count == 0)_shellEndEvent.OnNext(Unit.Default);
            _currentShell.shellView.UpdateCount(_currentShell.Item2._count);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

    }
}