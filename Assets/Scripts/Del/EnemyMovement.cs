using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : TankMovement
{
    [SerializeField] private float _changePointDistancion;
   
    public List<Transform> _pathCorners;
    private Transform _currentConrner;
    private int _currentConrnerIndex;   

    public void Construct(float motorForce,float maxSpeed, List<Transform> _corners)
    {
        _motorForce = motorForce;
        _maxSpeed = maxSpeed;
        _pathCorners = _corners;
        _currentConrnerIndex = 0;
    }



    protected  void Start()
    {
         
        _currentConrner = _pathCorners[0];
    }



    protected override void Update()
    {
        PathUpdate();
        WheelUpdate(1);
        base.Update();
    }

    private void PathUpdate()
    {
        if ((_currentConrner.position - transform.position).magnitude < _changePointDistancion)
        {          
            try 
            {
                _currentConrnerIndex++;
                _currentConrner = _pathCorners[_currentConrnerIndex];            
            }
            catch (IndexOutOfRangeException ex)
            {
                Debug.Log(ex.Message + "End of Path");
            }
        }
    }

    protected override void Steering()
    {
        Vector3 directionToTarget = _currentConrner.position - transform.position;
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
