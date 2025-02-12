using Assets.Scripts.TankParts.Enemy;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Convoy
{
    public class Transporter : ConvoyPart
    {
        [SerializeField] private float _scanRadius;
        [SerializeField] private LayerMask _playerMask;

        private ConvoyState _convoyState = new();
        private EnemyData _enemyData;

        private void OnEnable()
        {
            if (_enemyData == null)
            {
                _enemyData = GetComponent<EnemyData>();
            }       
            Construct();
        }

        private void Construct()
        {
            _enemyData._currentConvoy._convoyRetreatAction += Retreat;
            _convoyState.Construct(_enemyData, transform);
            _enemyData._currentConvoy.ConvoyPartSubscribe(this);
        }

        private void Update()
        {
            PlayerScan();
            _convoyState.Update();
        }

        private void PlayerScan()
        {
            var player = Physics.OverlapSphere(transform.position, _scanRadius, _playerMask);
            if (player.Length >= 1) _convoyRetreatAction?.Invoke();
        }

        public override void Retreat()
        {
            _convoyState.Retreat();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _enemyData._currentConvoy._convoyRetreatAction -= Retreat;
        }


    }
}