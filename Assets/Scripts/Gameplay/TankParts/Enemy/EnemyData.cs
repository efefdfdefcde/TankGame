using Assets.Scripts.Convoy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.TankParts.Enemy
{
    public class EnemyData : MonoBehaviour
    {
        [SerializeField] private EnemiesList _enemyType;
        [SerializeField] private float _motorForce;
        [SerializeField] private float _persuitSpeed;
        [SerializeField] private float _convoySpeed;
        [SerializeField] private float _retreatSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _reloadSpeed;
        [SerializeField] private Transform _centerOfMass;
        [SerializeField] private float _changePointDistancion;
        [SerializeField] private float _endPersuitDistance;
        [SerializeField] private Wheel[] _wheels;

        public Rigidbody _rb { get; private set; }
        public NavMeshAgent _agent { get; private set; }

        public List<Transform> _convoyPath;

        public Convoy.Convoy _currentConvoy { get; private set; }

        private void OnEnable()
        {
            _rb = GetComponent<Rigidbody>();
            _agent = GetComponent<NavMeshAgent>();
            _rb.transform.position = _centerOfMass.position;
        }

        public Wheel[] GetWheels() => _wheels;

        public float GetForce() => _motorForce;

        public float GetSpeed() => _persuitSpeed;

        public float GetConvoySpeed() => _convoySpeed;

        public float GetRetreatSpeed() => _retreatSpeed;

        public float GetRotationSpeed() => _rotationSpeed;

        public float GetReloadSpeed() => _reloadSpeed;

        public float GetChangePositionDistancion() => _changePointDistancion;

        public float GetEndPersuitDistance() => _endPersuitDistance;

        public EnemiesList GetEnemyType() => _enemyType;

        public void Construct(ConvoyPartData convoyPart,Convoy.Convoy convoy)
        {
            _motorForce = convoyPart._motorForce;
            _persuitSpeed = convoyPart._maxSpeed;
            _convoySpeed = convoyPart._convoySpeed;
            _retreatSpeed = convoyPart._speedOfRetreat;
            _rotationSpeed = convoyPart._rotationSpeed;
            _reloadSpeed = convoyPart._rotationSpeed;
            _convoyPath = convoy._path.GetPoints();
            _currentConvoy = convoy;
        }

       
    }
}