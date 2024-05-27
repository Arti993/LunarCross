using System;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public List<WheelAxle> wheelAxleList; 
    public VehicleSettings vehicleSettings;
    [SerializeField] private float _ackermanFactor;

    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;
    private Vector2 _moveInput;
    private float _steering;
    private float _steeringSign;
    private float _steeringFactor;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void Start()
    {
       
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.mass = vehicleSettings.mass;
        _rigidbody.drag = vehicleSettings.drag;
        _rigidbody.centerOfMass = vehicleSettings.centerOfMass;
    }

    private void FixedUpdate()
    {
        ControlWheels();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void ControlWheels()
    {
        _moveInput = _playerInput.Player.Turn.ReadValue<Vector2>();

        _steering = vehicleSettings.steeringAngle * _moveInput.x;

        foreach (WheelAxle wheelAxle in wheelAxleList)
        {
            if (wheelAxle.steering)
            {
                _steeringSign = -1f;
                _steeringFactor = 1f;

                if (Mathf.Approximately(_steeringSign, Mathf.Sign(wheelAxle.wheelColliderLeft.steerAngle)))
                    _steeringFactor = _ackermanFactor;

                wheelAxle.wheelColliderLeft.steerAngle = _steering * _steeringFactor;

                _steeringSign = 1f;
                _steeringFactor = 1f;

                if (Mathf.Approximately(_steeringSign, Mathf.Sign(wheelAxle.wheelColliderRight.steerAngle)))
                    _steeringFactor = _ackermanFactor;

                wheelAxle.wheelColliderRight.steerAngle = _steering * _steeringFactor;
            }

            if (wheelAxle.motor)
            {
                wheelAxle.wheelColliderLeft.motorTorque = vehicleSettings.motorTorque;
                wheelAxle.wheelColliderRight.motorTorque = vehicleSettings.motorTorque;
            }

            ApplyWheelVisuals(wheelAxle.wheelColliderLeft, wheelAxle.wheelMeshLeft);
            ApplyWheelVisuals(wheelAxle.wheelColliderRight, wheelAxle.wheelMeshRight);
        }
    }

    private void ApplyWheelVisuals(WheelCollider wheelCollider, GameObject wheelMesh)
    {
        wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);

        Quaternion realRotation = rotation * Quaternion.Inverse(wheelCollider.transform.parent.rotation) *
                                  transform.rotation;

        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = realRotation;
    }
}