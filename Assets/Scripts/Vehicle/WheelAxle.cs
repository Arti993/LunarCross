using UnityEngine;

namespace Vehicle
{
    [System.Serializable]
    public class WheelAxle
    {
        public WheelCollider wheelColliderLeft;
        public WheelCollider wheelColliderRight;
        public GameObject wheelMeshLeft;
        public GameObject wheelMeshRight;
        public bool motor;
        public bool steering;
    }
}

