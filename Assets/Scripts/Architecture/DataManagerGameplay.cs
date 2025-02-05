using Assets.Scripts.Architecture.SaveSistem;
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

        [SerializeField] private ShellManager _shellManager;

        private GameplayEnterParams _enterParams;
        private GameplayExitParams _exitParams;
        private ShopEnterParams _shopEnterParams;

        private VehicleData _currentVehicle;

        private CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(GameplayEnterParams enterParams = null, GameplayExitParams exitParams = null)
        {
            _enterParams = enterParams;
            _exitParams = exitParams;
            _currentVehicle = Resources.Load<VehicleData>($"ScriptableObjects/VehicleDatas/{_enterParams._wehicleWay}");

            _shellManager._shellManagerInit.Subscribe(_ => _shellManagerInit.OnNext(_currentVehicle)).AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}