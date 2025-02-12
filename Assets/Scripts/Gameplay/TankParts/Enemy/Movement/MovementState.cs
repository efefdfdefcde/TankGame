using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TankParts.Enemy
{
    public abstract class MovementState : State
    {
        protected float _motorForce;
        protected float _maxSpeed;
        protected Wheel[] _wheels;
        protected Transform transform;
        protected Vector3 _currentCorner;
        protected Transform _centerOfMass;
        protected float _currentSpeed;
        protected Rigidbody _rb;

        public override void Update()
        {
            WheelUpdate();
            PathUpdate();
            Steering();
            SpeedControll();
        }

        protected void WheelUpdate()
        {

            foreach (var wheel in _wheels)
            {
                wheel.colider.motorTorque = _motorForce;
                wheel.UpdateMesh();
            }
        }

        protected virtual void PathUpdate()
        {

        }

        protected virtual void Steering()
        {
            Vector3 directionToTarget = _currentCorner - transform.position;
            directionToTarget.y = 0;
            float targetAngle = Vector3.SignedAngle(transform.forward, directionToTarget, Vector3.up);

            foreach (var wheel in _wheels)
            {
                if (wheel.IsSteering)
                {
                    wheel.colider.steerAngle = Mathf.Clamp(targetAngle, -40f, 40f);
                }
            }
        }

        protected void SpeedControll()
        {
            _currentSpeed = _rb.linearVelocity.magnitude * 3.6f;
            if (_currentSpeed > _maxSpeed)
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * (_maxSpeed / 3.6f);
            }

        }
    }
}