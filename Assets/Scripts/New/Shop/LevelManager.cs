using Assets.Scripts.Architecture;
using Assets.Scripts.ShopUI;
using R3;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.New.Shop.UI
{
    public class LevelManager : MonoBehaviour
    {
        public Subject<Unit> _levelInitialize = new();
        public Subject<(int, int)> _levelChanged = new();

        [SerializeField] private List<int> _levelList = new();
        [SerializeField] private LevelManagerUI _ui;
        [SerializeField] private New.Arhitecture.DataManagerShop _dataManager;

        private int _expirience;
        private int _expirienceLevel;
        private int _currentLevel;

        private CompositeDisposable _disposable = new();

        [Inject]
        private void Construct()
        {
            New.Arhitecture.EventBus.Instance._getExpirience.Subscribe(expirience => GetExperience(expirience)).AddTo(_disposable);
            _dataManager._levelInitialize.Subscribe(level => Init(level)).AddTo(_disposable);
        }

        private void Start()
        {
            _levelInitialize.OnNext(Unit.Default);
            _levelInitialize.OnCompleted();
        }

        private void Init((int level, int experience) levelInfo)
        {
            _currentLevel = levelInfo.level;
            _expirience = levelInfo.experience;
            _expirienceLevel = _levelList[_currentLevel];
            _ui.UpdateLevel(_expirience, _expirienceLevel, _currentLevel);
        }

        private void GetExperience(int experience)
        {
            _expirience += experience;
            while(_expirience >= _expirienceLevel)
            {
                _expirience -= _expirienceLevel;
                _currentLevel++;
                _expirienceLevel = _levelList[_currentLevel];
            }
            _levelChanged.OnNext((_currentLevel, _expirience));
            _ui.UpdateLevel(_expirience,_expirienceLevel,_currentLevel);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

    }
}