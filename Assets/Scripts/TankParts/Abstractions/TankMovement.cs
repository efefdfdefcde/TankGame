using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class TankMovement : MonoBehaviour
{
    [SerializeField] protected float _motorForce;
    [SerializeField] protected float _maxSpeed;
    [SerializeField] protected Wheel[] _wheels;
    [SerializeField] private Transform _centerOfMass;
   
    protected float _currentSpeed;
    protected Rigidbody _rb;


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

    protected virtual void Update()
    {
        Steering();
        SpeedControll();
    }

    protected void WheelUpdate(float controll)
    {

        foreach (var wheel in _wheels)
        {
            wheel.colider.motorTorque = _motorForce * controll;
            wheel.UpdateMesh();
        }
    }

    protected virtual void Steering()
    {
       
    }

    private void SpeedControll()
    {
        _currentSpeed = _rb.linearVelocity.magnitude * 3.6f;
        if (_currentSpeed > _maxSpeed)
        {
            _rb.linearVelocity = _rb.linearVelocity.normalized * (_maxSpeed / 3.6f); 
        }

    }
}
