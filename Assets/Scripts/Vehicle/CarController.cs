using UnityEngine;
using System.Collections.Generic;
using Agava.WebUtility;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    private const float InputChangeStepTime = 0.5f;

    [SerializeField] private List<WheelAxle> wheelAxleList;
    [SerializeField] private VehicleSettings vehicleSettings;
    [SerializeField] private float _ackermanFactor;

    private IControlInput _playerInput;
    private Rigidbody _rigidbody;
    private Vector2 _moveInput;
    private float _steering;
    private float _steeringSign;
    private float _steeringFactor;
    private float _sensitiveInput;

    private void OnEnable()
    {
        if (Device.IsMobile)
            _playerInput = DIServicesContainer.Instance.GetService<IGameplayFactory>().GetUiControlInput();
        else
            _playerInput = DIServicesContainer.Instance.GetService<IGameplayFactory>()
                .GetDesktopControlInput(transform);
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

    private void ControlWheels()
    {
        _moveInput = _playerInput.GetMoveInput();

        _sensitiveInput = Mathf.Lerp(_sensitiveInput, _moveInput.x, InputChangeStepTime);

        _steering = vehicleSettings.steeringAngle * _sensitiveInput;

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