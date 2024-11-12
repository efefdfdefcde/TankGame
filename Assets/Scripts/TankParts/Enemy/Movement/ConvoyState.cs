using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.TankParts.Enemy
{
    public class ConvoyState : MovementState
    {
        private float _changePointDistancion;
        private List<Transform> _pathConvoyCorners;
        private int _currentCornerIndex;
        private Transform _currentConvoyCorner;
        private NavMeshAgent _agent;
        private Vector3[] _pathCorners;
        private float _reteatSpeed;

        public void Construct(EnemyData data,Transform transform)
        {
            _wheels = data.GetWheels();
            this.transform = transform;
            _changePointDistancion = data.GetChangePositionDistancion();
            _motorForce = data.GetForce();
            _maxSpeed = data.GetConvoySpeed();
            _reteatSpeed = data.GetRetreatSpeed();
            _pathConvoyCorners = data._convoyPath;
            _agent = data._agent;
            _rb = data._rb;
            _currentConvoyCorner = _pathConvoyCorners[0];
            _currentCornerIndex = 0;
            _agent.updatePosition = false;
            _agent.updateRotation = false;
        }

        public override void Update()
        {
            base.Update();
            ConvoyPathUpdate();
        }

        protected override void PathUpdate()
        {
            _agent.nextPosition = transform.position;
            _agent.SetDestination(_currentConvoyCorner.position);
            _pathCorners = _agent.path.corners;
            try { _currentCorner = _pathCorners[1]; }
#pragma warning disable 0168
            catch (Exception e) { }
#pragma warning restore 0168
        }

        private void ConvoyPathUpdate()
        {
            if ((_currentConvoyCorner.position - transform.position).magnitude < _changePointDistancion)
            {
                try
                {
                    _currentCornerIndex++;
                    _currentConvoyCorner = _pathConvoyCorners[_currentCornerIndex];
                }
                catch (IndexOutOfRangeException ex)
                {
                    Debug.Log(ex.Message + "End of Path");
                }
            }
        }

        public void Retreat()
        {
            _maxSpeed = _reteatSpeed;
        }

    }
}