using Assets.Scripts;
using Assets.Scripts.TankParts.Player;
using System;
using System.Collections;
using UnityEngine;

namespace Assets
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _steerAngle;
        [SerializeField] private int _breakForce;
        [SerializeField] private float _motorForce;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private Wheel[] _wheels;
        [SerializeField] private Transform _centerOfMass;
        [SerializeField] private FuelPool _fuelPool;

        private float _breakInput;
        private float _inputVertical;
        private float _inputHorizontal;
        private float _currentSpeed;
        private Rigidbody _rb;
        private bool _isFuelEmpty;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.transform.position = _centerOfMass.position;
            _fuelPool._fuelIsEmptyAction += FuelEmpty;
        }

        private void Update()
        {
            CheckInput();
            WheelUpdate();
            Break();
            Steering();
            SpeedControll();
        }

        protected void WheelUpdate()
        {
            if (!_isFuelEmpty)
            {
                foreach (var wheel in _wheels)
                {
                    wheel.colider.motorTorque = _motorForce * _inputVertical;
                }
            }
            foreach (var wheel in _wheels)
            {
                wheel.UpdateMesh();
            }
        }

        private void Steering()
        {
            foreach (var wheel in _wheels)
            {
                if (wheel.IsSteering)
                {
                    wheel.colider.steerAngle = _steerAngle * _inputHorizontal;
                }
            }
        }

        private void CheckInput()
        {
            _inputVertical = Input.GetAxis("Vertical");
            _inputHorizontal = Input.GetAxis("Horizontal");

            float movingDirection = Vector3.Dot(transform.forward, _rb.linearVelocity);
            _breakInput = (movingDirection < -0.5f && _inputVertical > 0) || (movingDirection > 0.5f && _inputHorizontal < 0) ? Math.Abs(_inputVertical) : 0;
        }

        private void Break()
        {
            foreach (var wheel in _wheels)
            {
                wheel.colider.brakeTorque = _breakInput * _breakForce * (wheel.IsSteering ? 0.7f : 0.3f);
            }
        }

        private void SpeedControll()
        {
            _currentSpeed = _rb.linearVelocity.magnitude * 3.6f;
            if (_currentSpeed > _maxSpeed)
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * (_maxSpeed / 3.6f);
            }

        }

        private void FuelEmpty() => _isFuelEmpty = true;

        private void OnDestroy()
        {
            _fuelPool._fuelIsEmptyAction -= FuelEmpty;
        }
    }
}