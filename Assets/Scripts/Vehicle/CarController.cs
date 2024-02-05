using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;


public class CarController : MonoBehaviour
{
    public List<WheelAxle> wheelAxleList;
    public VehicleSettings vehicleSettings;
    private Rigidbody _rigidbody;
    private float _startSpeed = 2;
    private float _maxSpeed = 3.5f;

    [SerializeField] private float _ackermanFactor;

    private void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        _rigidbody.mass = vehicleSettings.mass;
        _rigidbody.drag = vehicleSettings.drag;
        _rigidbody.centerOfMass = vehicleSettings.centerOfMass;
        _rigidbody.velocity = new Vector3(0, 0, _startSpeed);
    }

    public void ApplyWheelVisuals(WheelCollider wheelCollider, GameObject wheelMesh)
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);

        Quaternion realRotation = rotation * Quaternion.Inverse(wheelCollider.transform.parent.rotation) *
            this.transform.rotation;

        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = realRotation;
    }

    public void FixedUpdate()
    {
        if (_rigidbody.velocity.z > _maxSpeed)
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, _maxSpeed);

        float motorTorque = vehicleSettings.motorTorque;
        float steering = vehicleSettings.steeringAngle * Input.GetAxis("Horizontal");

        foreach (WheelAxle wheelAxle in wheelAxleList)
        {
            if (wheelAxle.steering)
            {
                float sign = -1f;
                float factor = 1f;

                if (Mathf.Approximately(sign, Mathf.Sign(wheelAxle.wheelColliderLeft.steerAngle)))
                    factor = _ackermanFactor;

                wheelAxle.wheelColliderLeft.steerAngle = steering * factor;

                sign = 1f;
                factor = 1f;

                if (Mathf.Approximately(sign, Mathf.Sign(wheelAxle.wheelColliderRight.steerAngle)))
                    factor = _ackermanFactor;

                wheelAxle.wheelColliderRight.steerAngle = steering * factor;
            }

            if (wheelAxle.motor)
            {
                wheelAxle.wheelColliderLeft.motorTorque = motorTorque;
                wheelAxle.wheelColliderRight.motorTorque = motorTorque;
            }

            ApplyWheelVisuals(wheelAxle.wheelColliderLeft, wheelAxle.wheelMeshLeft);
            ApplyWheelVisuals(wheelAxle.wheelColliderRight, wheelAxle.wheelMeshRight);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;

    //    for (int i = 0; i < wheelAxleList.Count; i++)
    //    {
    //        Quaternion rotation;

    //        if (i == 0)
    //        {
    //            rotation = Quaternion.AngleAxis(wheelAxleList[i].wheelColliderLeft.steerAngle, Vector3.up);

    //            Gizmos.DrawRay(wheelAxleList[i].wheelColliderLeft.transform.position,
    //                rotation * (wheelAxleList[i].wheelColliderLeft.transform.forward * 10f));

    //            Gizmos.DrawRay(wheelAxleList[i].wheelColliderLeft.transform.position,
    //                rotation * (wheelAxleList[i].wheelColliderLeft.transform.forward * -10f));

    //            rotation = Quaternion.AngleAxis(wheelAxleList[i].wheelColliderRight.steerAngle, Vector3.up);

    //            Gizmos.DrawRay(wheelAxleList[i].wheelColliderRight.transform.position,
    //                rotation * (wheelAxleList[i].wheelColliderRight.transform.forward * 10f));

    //            Gizmos.DrawRay(wheelAxleList[i].wheelColliderRight.transform.position,
    //                rotation * (wheelAxleList[i].wheelColliderRight.transform.forward * -10f));
    //        }

    //        if (i == 2)
    //        {
    //            Gizmos.DrawRay(wheelAxleList[i].wheelColliderLeft.transform.position,
    //               (wheelAxleList[i].wheelColliderLeft.transform.forward * 10f));

    //            Gizmos.DrawRay(wheelAxleList[i].wheelColliderLeft.transform.position,
    //                (wheelAxleList[i].wheelColliderLeft.transform.forward * -10f));

    //            Gizmos.DrawRay(wheelAxleList[i].wheelColliderRight.transform.position,
    //               (wheelAxleList[i].wheelColliderRight.transform.forward * 10f));

    //            Gizmos.DrawRay(wheelAxleList[i].wheelColliderRight.transform.position,
    //                (wheelAxleList[i].wheelColliderRight.transform.forward * -10f));
    //        }
    //    }
    //}
}