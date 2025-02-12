using System;
using UnityEngine;

namespace Assets.Scripts
{
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
}