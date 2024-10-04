using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _motorForce;
    [SerializeField] private Wheel[] _wheels;
    [SerializeField] private float _steerAngle;
    [SerializeField] private Transform _centerOfMass;
    [SerializeField] private Transform point;
   

  
    private NavMeshAgent _agent;
    private Vector3[] _pathCorners;


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
        _agent = GetComponent<NavMeshAgent>();
        _agent.updatePosition = false;
        _agent.updateRotation = false;
    }



    private void Update()
    {
        PathUpdate();
        WheelUpdate();
        Steering();
       
    }

    private void WheelUpdate()
    {

        foreach (var wheel in _wheels)
        {
            wheel.colider.motorTorque = _motorForce;
            wheel.UpdateMesh();
        }
    }

    private void PathUpdate()
    {
        _agent.nextPosition = transform.position;
        _agent.SetDestination(point.position);
        _pathCorners = _agent.path.corners;
    }

    private void Steering()
    {
        if (_pathCorners.Length > 0)
        {
            Vector3 directionToTarget = _pathCorners[1] - transform.position;
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
    }

   

   
}
