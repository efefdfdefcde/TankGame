using Assets.Scripts.TankParts.Enemy.Movement;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.TankParts.Enemy
{
    public class PersuitState : MovementState
    {
        public event Action _endPersuitAction;

        private float _pursuitEndDistance;
        private Transform _player;
        private NavMeshAgent _agent;
        private Vector3[] _pathCorners;
        //private StuckTrigger _trigger;

        public void Construct(EnemyData data,Transform transform/*, StuckTrigger trigger*/)
        {
            _wheels = data.GetWheels();
            this.transform = transform; 
            _agent = data._agent;
            _motorForce = data.GetForce();
            _maxSpeed = data.GetSpeed();
            _rb = data._rb;
            _pursuitEndDistance = data.GetEndPersuitDistance();
            //_trigger = trigger;
            //_trigger._stuckAction += AntiStuck;
            _agent.updatePosition = false;
            _agent.updateRotation = false;
        }

        public void GetPoint(Transform player)
        {
            _player = player;
        }

        public override void Update()
        {
            base.Update();
            PersuitEndScan();
        }

        protected override void PathUpdate()
        {
            _agent.nextPosition = transform.position;
            _agent.SetDestination(_player.position);
            _pathCorners = _agent.path.corners;
            _currentCorner = _pathCorners[1];
        }

        private void PersuitEndScan()
        {
            if((_player.transform.position - transform.position).magnitude > _pursuitEndDistance)
            {
                _endPersuitAction?.Invoke();
            }
        }

        //private void AntiStuck(int controll)
        //{
        //    _motorForce *= controll;
        //}

        

    }
}