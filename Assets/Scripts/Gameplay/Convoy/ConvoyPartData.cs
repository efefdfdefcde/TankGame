using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Convoy
{
    [Serializable]
    public class ConvoyPartData 
    {
        public Vector3 _position;
        public EnemiesList _typeOfEnemy;
        public float _motorForce;
        public float _maxSpeed;
        public float _convoySpeed;
        public float _speedOfRetreat;
        public float _reloadTime;
        public float _rotationSpeed;

        public ConvoyPartData(Vector3 position)
        {
            _position = position;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}