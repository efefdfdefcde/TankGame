using Assets.Scripts.New.Arhitecture.SaveSistem;
using Assets.Scripts.New.Shop;
using Assets.Scripts.New.Shop.PartsSO.Shells;
using Assets.Scripts.New.Shop.UI;
using Assets.Scripts.New.Shop.UI.NationSelect;
using Assets.Scripts.New.Shop.Upgrades;
using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEngine.ParticleSystem;

namespace Assets.Scripts.New.Arhitecture
{
    public class DataManagerShop : MonoBehaviour
    {
        public Subject<(int,int,int,int)> _bankInitialize = new();
        public Subject<(int,NationStorage)> _experienceInit = new();
        public Subject<NationStorage> _changeNation = new();
        public Subject<(int, int)> _levelInitialize = new();
        public Subject<(ShopExitParams, GameplayEnterParams)> _save = new();

        [SerializeField] private ShopBank _bank;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private BattleButton _battleButton;
        [SerializeField] private ShellCatalog _shellCatalog;

        private Dictionary<NationName, NationStorage> _nations = new();
        private NationName _currentNation;

        private ShopExitParams _exitParams;
        private ShopEnterParams _enterParams;
        private GameplayEnterParams _gameplayEnterParams = new();
        private Loader _loader = new();
       
        private DiContainer _container;
        private CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(DiContainer container, ShopEnterParams enterParams = null, ShopExitParams exitParams = null)
        {
            _enterParams = enterParams;
            _exitParams = exitParams;
            _container = container;
            SaveInit();
            New.Arhitecture.EventBus.Instance._selectNation.Subscribe(name => ChangeNation(name)).AddTo(_disposables);
            _battleButton._saveData.Subscribe(_ => Save()).AddTo(_disposables);
            NationInit();
            Level();
            BankInit();
        }

        private void SaveInit()
        {
            _loader.Load(ref _exitParams);
            _container.Bind<Dictionary<string, UpgradeStatusDictonary>>().FromInstance(_exitParams._upgradeStatus).AsSingle();
            _container.Bind<Dictionary<(int, NationName), BuildSave>>().FromInstance(_exitParams._buildDictionary).AsSingle();
            _container.Bind<Dictionary<NationName,int>>().FromInstance(_exitParams._selectedCells).AsSingle();
            _shellCatalog.Init(_exitParams);
        }

        private void NationInit()
        {
            NationStorage[] nationStorages = Resources.LoadAll<NationStorage>("ScriptableObjects/Nations");
            foreach (var storage in nationStorages)
            {
                _nations.Add(storage._name, storage);
            }
        } 

        private void Level()
        {
            _levelManager._levelInitialize.Subscribe(_ => _levelInitialize.OnNext((_exitParams._level, _exitParams._levelExperience)));
            _levelManager._levelChanged.Subscribe(level => { _exitParams._level = level.Item1; _gameplayEnterParams._level = level.Item1; _exitParams._levelExperience = level.Item2; });
        }

        private void BankInit()
        {
            if(_enterParams != null)
            {
                _bank._bankInitialize.Subscribe(_ => _bankInitialize.OnNext((_exitParams._money,_exitParams._gold,_enterParams._money ,_enterParams._gold)));
                _bank._bankInitialize.Subscribe(_ => _experienceInit.OnNext((_enterParams._experience, _nations[_exitParams._currentNation])));
            }
            _bank._moneyChanged.Subscribe(money => _exitParams._money = money).AddTo(_disposables);
            _bank._goldChanged.Subscribe(gold => _exitParams._gold = gold).AddTo(_disposables);
        }

        private void Start()
        {
            New.Arhitecture.EventBus.Instance._selectNation.OnNext(_exitParams._currentNation);
        }

        private void ChangeNation(NationName nationName)
        {
            _currentNation = nationName;
            var nation = _nations[_currentNation];
            _exitParams._currentNation = nationName;
            _changeNation.OnNext(nation);
        }

        private void Save()
        {
            _save.OnNext((_exitParams, _gameplayEnterParams));
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
            _shellCatalog.OnDestroy();
        }
    }
}