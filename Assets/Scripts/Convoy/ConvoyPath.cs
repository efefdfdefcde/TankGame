using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ConvoyPath : MonoBehaviour
    {
        [SerializeField] private List<Transform> _points;
        
        public List<Transform> GetPoints() =>  _points;


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < _points.Count; i++)
            {
                Gizmos.DrawSphere(_points[i].position, 2f);
                if (i > 0)
                {
                    Gizmos.DrawLine(_points[i - 1].position, _points[i].position);
                }
            }
        }
    }
}