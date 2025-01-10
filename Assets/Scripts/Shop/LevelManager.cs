using Assets.Scripts.ShopUI;
using R3;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<int> _levelList = new();
        [SerializeField] private LevelManagerUI _ui;

        private int _expirience;
        private int _currentLevel;

        private CompositeDisposable _disposable = new();

        private void Awake()
        {
            EventBus.Instance._getExpirience.Subscribe(expirience => GetExperience(expirience)).AddTo(_disposable);
        }

        private void GetExperience(int experience)
        {
            _expirience += experience;
            var level = _levelList[_currentLevel];
            if(_expirience >= level)
            {
                _expirience -= level;
                _currentLevel++;
            }
            _ui.UpdateLevel(_expirience,level,_currentLevel);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

    }
}