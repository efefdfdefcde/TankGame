using Assets.Scripts.Architecture.SaveSistem;
using Assets.Scripts.Gameplay;
using Assets.Scripts.TankParts.Player;
using R3;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Architecture
{
    public class DataManagerGameplay : MonoBehaviour
    {
        public Subject<VehicleData> _shellManagerInit = new();
        public Subject<ShopEnterParams> _saveParams = new();

        [SerializeField] private ShellManager _shellManager;
        [SerializeField] private Gameplay.Bank _bank;
        [SerializeField] private ToShopButton _toShop;

        private GameplayEnterParams _enterParams;
        private GameplayExitParams _exitParams;
        private ShopEnterParams _shopEnterParams = new();

        private VehicleData _currentVehicle;

        private CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(GameplayEnterParams enterParams = null, GameplayExitParams exitParams = null)
        {
            _enterParams = enterParams;
            _exitParams = exitParams;
            _currentVehicle = Resources.Load<VehicleData>($"ScriptableObjects/VehicleDatas/{_enterParams._wehicleWay}");

            _shellManager._shellManagerInit.Subscribe(_ => _shellManagerInit.OnNext(_currentVehicle)).AddTo(_disposables);
            _bank._pointsUpdate.Subscribe(points => SetPoints(points)).AddTo(_disposables);
            _toShop._saveEvent.Subscribe(_ => _saveParams.OnNext(_shopEnterParams)).AddTo(_disposables);
        }

        private void SetPoints((int money, int researchP) points)
        {
            _shopEnterParams._money = points.money;
            _shopEnterParams._researchPoints = points.researchP;
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}