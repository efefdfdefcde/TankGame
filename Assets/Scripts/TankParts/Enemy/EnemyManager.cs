using Assets.Scripts.Convoy;
using Assets.Scripts.TankParts.Enemy.Movement;
using Assets.Scripts.TankParts.Player;
using System;
using UnityEngine;

namespace Assets.Scripts.TankParts.Enemy
{
    public class EnemyManager : ConvoyPart
    {

        [SerializeField] private float _scanRadius;
        [SerializeField] private LayerMask _playerMask;
        [SerializeField] private EnemyTurret _enemyTurret;      
        [SerializeField] private EnemyCannon _enemyCannon;
        [SerializeField] private StuckTrigger _trigger;

        private StateMashine _SM = new();
        private ConvoyState _convoyState = new();
        private PersuitState _persuitState = new();
        private Transform _persuitPoint;
        private EnemyData _enemyData;

        private void OnEnable()
        {
            if (_enemyData == null)
            {
                _enemyData = GetComponent<EnemyData>();
            }
            Construct();
            _SM.Initialize(_convoyState);
          
        }

        public void Construct()
        {
            _enemyData._currentConvoy.ConvoyPartSubscribe(this);
            _enemyData._currentConvoy._convoyRetreatAction += Retreat;
            _convoyState.Construct(_enemyData,transform);
            _persuitState.Construct(_enemyData, transform/*,_trigger*/);
            _persuitState._endPersuitAction += ConvoyState;
        }

        private void Update()
        {
            PlayerScan();   
            _SM.CurrentState.Update();
        }

        private void PlayerScan()
        {
            var player = Physics.OverlapSphere(transform.position, _scanRadius, _playerMask);
            if (player.Length > 0)
            {
                _convoyRetreatAction?.Invoke();
                _enemyTurret.TurretRotation(player[0]);
                _enemyCannon.EnemyScan(player[0]);//Owerride
                if (!_persuitPoint)
                {
                    PersuitState();
                }             
            }
            else
            {
                _enemyTurret.TurretReturn();
            }
        }

        private void PersuitState()
        {
            _persuitPoint = PersuitManager._instance.GetPersuitPoint();
            if (_persuitPoint != null)
            {
                _persuitState.GetPoint(_persuitPoint);
                _SM.ChangeState(_persuitState);
            }
        }

        private void ConvoyState()
        {
            try { EventBus.Instance._returnPersuitPoint(_persuitPoint); }
#pragma warning disable 0168
            catch { Exception exception;}
#pragma warning restore 0168
            _persuitPoint = null;
            _SM.ChangeState(_convoyState);
        }

        public override void Retreat()
        {
            _convoyState.Retreat();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _enemyData._currentConvoy._convoyRetreatAction -= Retreat;
            ConvoyState();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _scanRadius);
        }
    }
}