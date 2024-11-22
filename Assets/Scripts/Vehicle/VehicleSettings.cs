using UnityEngine;

namespace Vehicle
{
    [System.Serializable]
    public class VehicleSettings
    {
        public float mass = 1500;
        public float drag = 0.05f;
        public Vector3 centerOfMass = new Vector3(0, -1.0f, 0);
        public float motorTorque = 1200;
        public float steeringAngle = 50;
    }
}
