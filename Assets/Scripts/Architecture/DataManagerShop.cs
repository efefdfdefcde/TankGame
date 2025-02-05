using Assets.Scripts.Architecture.SaveSistem;
using Assets.Scripts.Shop;
using Assets.Scripts.Shop.ResearchTree;
using Assets.Scripts.Shop.ResearchTree.NewUpgrade;
using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Architecture
{
    public class DataManagerShop : MonoBehaviour
    {
        public Subject<(int,int,int,int)> _bankInitialize = new();
        public Subject<Dictionary<string, UpgradeStatusDictonary>> _upgradeInitialize = new();
        public Subject<(int,int)> _levelInitialize = new();
        public Subject<(ShopExitParams,GameplayEnterParams)> _save = new();
        public Subject<string> _setVehicleWay = new();

        [SerializeField] private BattleButton _battleButton;
        [SerializeField] private Bank _bank;
        [SerializeField] private UpgradePopupSpawner _spawner;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private VehicleChanger _vehicleChanger;

        private ShopExitParams _exitParams;
        private ShopEnterParams _enterParams;
        private GameplayEnterParams _gameplayEnterParams = new();
        private VehicleLoader _vehicleLoader = new();

        private CompositeDisposable _disposable = new();

        [Inject]
        private void Construct(ShopExitParams exitParams = null, ShopEnterParams enterParams = null)
        {
            _enterParams = enterParams;
            _exitParams = exitParams;
            _vehicleLoader.Load(ref _exitParams);
            Init();
        }

       

        private void Init()
        {
            if (!string.IsNullOrEmpty(_exitParams._currentVehicleWay)) _vehicleChanger._initEvent.Subscribe(_ => _setVehicleWay.OnNext(_exitParams._currentVehicleWay)).AddTo(_disposable);
            _vehicleChanger._setVenicleEvent.Subscribe(vehicleData => SetVehicle(vehicleData)).AddTo(_disposable);
            Bank();
            _spawner._upgradeInitialize.Subscribe(_ => _upgradeInitialize.OnNext(_exitParams._upgradeStatus)).AddTo(_disposable);
            _battleButton._saveData.Subscribe(_ => Save()).AddTo(_disposable);
            Level();
        }

        private void Level()
        {
            _levelManager._levelInitialize.Subscribe(_ => _levelInitialize.OnNext((_exitParams._level, _exitParams._levelExperience)));
            _levelManager._levelChanged.Subscribe(level => { _exitParams._level = level.Item1; _gameplayEnterParams._level = level.Item1; _exitParams._levelExperience = level.Item2; });
        }

        private void Bank()
        {
            _bank._bankInitialize.Subscribe(_ => _bankInitialize.OnNext((_exitParams._money, _exitParams._gold, _enterParams._money, _enterParams._gold))).AddTo(_disposable);
            _bank._moneyChangedEvent.Subscribe(money => _exitParams._money = money).AddTo(_disposable);
            _bank._goldChangedEvent.Subscribe(gold => _exitParams._gold = gold).AddTo(_disposable);
        }

        private void SetVehicle(VehicleData data)
        {
            _exitParams._currentVehicleWay = data.name;
            _gameplayEnterParams._wehicleWay = data.name;
        }

        private void Save()
        {
            _save.OnNext((_exitParams,_gameplayEnterParams));
        }



        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}