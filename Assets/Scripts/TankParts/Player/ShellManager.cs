using Assets.Scripts.Architecture;
using Assets.Scripts.Shells;
using Assets.Scripts.UI.ShellSelector;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TankParts.Player
{
    public class ShellManager : MonoBehaviour
    {
        public readonly Subject<Unit> _shellEndEvent = new Subject<Unit>();
        public event Action _unselectAction;
        public event Action<GameObject,int> _setShellsAction;

        [SerializeField] private List<ShellDataWithCount> _dataList;//Override
        [SerializeField] private GameObject _shellOrganiser;
        [SerializeField] private ShellConstructor _shellFormPrefab;
        [SerializeField] private PlayerCannon _playerCannon;

        private Dictionary<ShellConroller, (ShellView, ShellData, int)> _shellsCatalog = new();
        private (ShellView, ShellData, int) _currentShell;
        private ShellConroller _startShell;

        private void Start()
        {

            foreach (var shellData in _dataList)
            {
                var shellForm = Instantiate(_shellFormPrefab);
                shellForm.transform.SetParent(_shellOrganiser.transform);
                shellForm.ConstructView(shellData._data, shellData._count); 
                var formViewController = shellForm.GetViewController();
                if (!_startShell) _startShell = formViewController.Item2;
                formViewController.Item1.UpdateCount(shellData._count);
                _shellsCatalog.Add(formViewController.Item2,(formViewController.Item1,shellData._data,shellData._count));
                formViewController.Item2._selectShellAction += SelectShell;
            }
            _playerCannon._shellSpend += ShellSpend;
            SelectShell(_startShell);
        }

        private void SelectShell(ShellConroller controller)
        {
            if (_currentShell.Item1 != null)
            {
                _currentShell.Item1.Unselect();
            }
            _currentShell = _shellsCatalog[controller];
            _currentShell.Item1.Select();
            ShellData shellData = _currentShell.Item2;
            GameObject shell = shellData._shellPrefab;
            var shellConstructor = shell.GetComponent<PlayerShell>();
            shellConstructor.SetCharacteristic(shellData._damage, shellData._shellPenetration, shellData._fuseSensitivity);
            _setShellsAction?.Invoke(shell, _currentShell.Item3);
        }

        private void ShellSpend()
        {
            _currentShell.Item3--;
            if(_currentShell.Item3 == 0)_shellEndEvent.OnNext(Unit.Default);
            _currentShell.Item1.UpdateCount(_currentShell.Item3);
        }

    }
}