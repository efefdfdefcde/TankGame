using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TankMovement : MonoBehaviour
{
    [SerializeField] private float _motorForce;
    [SerializeField] private Wheel[] _wheels;
    [SerializeField] private float _steerAngle;
    [SerializeField] private Transform _centerOfMass;
    [SerializeField] private int _breakForce;

    private float _breakInput;
    private float _inputVertical;
    private float _inputHorizontal;
    private Rigidbody _rb;
    private float _speed;

    [Serializable]
    public struct Wheel
    {
        public Transform wheelMesh;
        public WheelCollider colider;
        public bool IsSteering;

        public void UpdateMesh()
        {
            colider.GetWorldPose(out Vector3 pos, out Quaternion rot);
            wheelMesh.rotation = rot;
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.transform.position = _centerOfMass.position;
    }

    private void Update()
    {
        WheelUpdate();
        Steering();
        Break();
        CheckInput();
    }

    private void WheelUpdate()
    {
        _speed = _rb.linearVelocity.magnitude;

        foreach(var wheel in _wheels)
        {
            wheel.colider.motorTorque = _motorForce * _inputVertical;
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
   
}
