using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace Assets.Scripts.TankParts.Player
{
    public class PersuitManager : MonoBehaviour
    {
        [SerializeField] private List<Transform> _persuitPoints;

        public static PersuitManager _instance;

        [Inject]
        private void Construct()
        {
            _instance = this;
        }

        private void Awake()
        {
            EventBus.Instance._returnPersuitPoint += ReturnPersuitPoint;   
        }

        public Transform GetPersuitPoint()
        {
            if( _persuitPoints.Count > 0)
            {
                var point = _persuitPoints[0];
                _persuitPoints.Remove(point);
                return point;
            }
            else  return null;
        }

        private void ReturnPersuitPoint(Transform persuitPoint)
        {
            _persuitPoints.Add(persuitPoint);
        }

        private void OnDestroy()
        {
            EventBus.Instance._returnPersuitPoint -= ReturnPersuitPoint;
        }
    }
}