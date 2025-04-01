using Assets.Scripts.New.Arhitecture.SaveSistem;
using Assets.Scripts.New.Shop.UI.NationSelect;
using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.New.Shop.PartsSO.Shells
{
    public class ShellCatalog : MonoBehaviour
    {
        private Dictionary<NationName, Dictionary<ShellType, (Shell, bool)>> _awailableShells = new()
        {
            {NationName.USSR,new Dictionary<ShellType, (Shell, bool)>() },
            {NationName.Germany,new Dictionary<ShellType, (Shell, bool)>()},
            {NationName.USA,new Dictionary<ShellType, (Shell, bool)>()}
        };

        private readonly ReactiveProperty<Dictionary<ShellType, (Shell, bool)>> _nationShells = new();
        public ReadOnlyReactiveProperty<Dictionary<ShellType, (Shell, bool)>> NationShells => _nationShells;


        private Dictionary<NationName,Dictionary<ShellType, bool>> _saveDictionary;

        private CompositeDisposable _disposables = new();

        #region
        [SerializeField] private DictionaryStruct[] _shellStruct;

        [Serializable]
        public struct DictionaryStruct
        {
            public NationName _nation;
            public ShellType _type;
            public Shell _shell;
            public bool _isAwailable;
        }
        #endregion

        public void Init(ShopExitParams exitParams)
        {
            foreach(var str in _shellStruct)
            {
                _awailableShells[str._nation].Add(str._type,(str._shell,str._isAwailable));
            }
            if (exitParams._awailableShells != null)
            {
                _saveDictionary = exitParams._awailableShells;

                foreach (var dictionar in new Dictionary<NationName, Dictionary<ShellType, bool>>(_saveDictionary))
                {
                    var currentValue = _awailableShells[dictionar.Key];

                    foreach (var value in new Dictionary<ShellType, (Shell, bool)>(currentValue))
                    {
                        var shell = _awailableShells[dictionar.Key][value.Key];
                        _awailableShells[dictionar.Key][value.Key] = (shell.Item1, value.Value.Item2);
                    }
                }
            }
            else
            {
                _saveDictionary = new()
                    {
                        {NationName.USSR,new Dictionary<ShellType, bool>() },
                        {NationName.Germany,new Dictionary<ShellType, bool>()},
                        {NationName.USA,new Dictionary<ShellType, bool>()}
                    };
                foreach (var shell in _awailableShells)
                {
                    foreach (var entry in shell.Value)
                    {
                        _saveDictionary[shell.Key].Add(entry.Key, entry.Value.Item2);
                    }
                }
            }
            exitParams._awailableShells = _saveDictionary;
            New.Arhitecture.EventBus.Instance._shellAwailable.Subscribe(shell => UnlockShell(shell)).AddTo(_disposables);
            New.Arhitecture.EventBus.Instance._selectNation.Subscribe(nation => _nationShells.Value = _awailableShells[nation]);
        }

        private void UnlockShell((NationName nation,ShellType shell) unlockInfo)
        {
            var nation = _awailableShells[unlockInfo.nation];
            var currentValue = nation[unlockInfo.shell];
            _awailableShells[unlockInfo.nation][unlockInfo.shell] = (currentValue.Item1, true);
            _saveDictionary[unlockInfo.nation][unlockInfo.shell] = true;
        }
        
        public void OnDestroy() => _disposables.Dispose();
    }
}