using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TankParts.Player
{
    public class FuelPool : MonoBehaviour
    {
        public event Action<float,float> _fuelUpdateAction;
        public event Action _fuelIsEmptyAction; //Override

        [SerializeField] private float _maxFuel;
        [SerializeField] private float _fuelWasteValue;

        private float _currentFuel;
        private bool _isEmpty => _currentFuel <= 0;

        private void Start()
        {
            _currentFuel = _maxFuel;
        }

        private void Update()
        {
            FuelWaste();
        }

        private void FuelWaste()
        {
            if (!_isEmpty)
            {
                _currentFuel -= _fuelWasteValue * Time.deltaTime;
                _fuelUpdateAction?.Invoke(_maxFuel, _currentFuel);
            }
            else
            {
                _fuelIsEmptyAction?.Invoke();
            }
        }
        
    }
}