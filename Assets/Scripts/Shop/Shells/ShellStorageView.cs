using Assets.Scripts.Shop.ResearchTree;
using R3;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Shop.Shells
{
    public class ShellStorageView : MonoBehaviour
    {
        public Subject<Dictionary<ShellType, ShellPopup>> _listUpdate = new();

        [SerializeField] private TextMeshProUGUI _storageCapasity;
        [SerializeField] private ShellStorageModel _model;
        [SerializeField] private ShellPopup _popupPref;
        [SerializeField] private GameObject _popupParent;
        [SerializeField] private ShellStoragePresenter _presenter;
        [SerializeField] private TextMeshProUGUI _shellsCount;

        private Dictionary<ShellType,ShellPopup> _spawnedPopups = new();
        private VenicleData _data;

        private CompositeDisposable _disposable = new();


        [Inject]
        private void Construct()
        {
            _model._setDataEvent.Subscribe(data => { _data = data; SpawnPopups(); SetShellStorage(); }).AddTo(_disposable);
            EventBus.Instance._shellUpdate.Subscribe(_ => SpawnPopups()).AddTo(_disposable);
            _presenter._shellsCountEvent.Subscribe(count => CountUpdate(count)).AddTo(_disposable);
        }

        private void SpawnPopups()
        {

            if (_spawnedPopups.Count > 0) DestoyPopups();
            foreach(var shell in _data._shellInfo)
            {
                if (shell.Value._data._isAllowed)
                {
                    var popup = Instantiate(_popupPref, _popupParent.transform);
                    popup.Init(shell.Value._gameplayData._shellImage, shell.Value._data._name, shell.Value._data._damage,
                        shell.Value._data._shellPenetration, shell.Value._data._fuseSensivity,shell.Value._data._count,_data._shellStorageCapasity, shell.Key);
                    _spawnedPopups.Add(shell.Key,popup);
                }     
            }
            _listUpdate?.OnNext(_spawnedPopups);
        }

        private void SetShellStorage()
        {
            _storageCapasity.text = _data._shellStorageCapasity.ToString();
        }

        private void CountUpdate(int count)
        {
            _shellsCount.text = count.ToString();
        }

        private void DestoyPopups()
        {
            foreach(var popup in _spawnedPopups) Destroy(popup.Value.gameObject);
            _spawnedPopups.Clear();
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}