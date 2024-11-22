using UnityEngine;

namespace Vehicle
{
    public class VehicleRotationLimit : MonoBehaviour
    {
        [SerializeField] private float _maxRotationAbsYAngle = 70f;

        private Transform _transform;
        private Vector3 _currentRotation;
        private float _globalZRotation;
        private float _deltaRotationY;
        private float _targetRotationY;

        private void Start()
        {
            _transform = transform;

            _globalZRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up).eulerAngles.y;
        }

        private void Update()
        {
            _currentRotation = transform.rotation.eulerAngles;

            _globalZRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up).eulerAngles.y;

            _deltaRotationY = Mathf.DeltaAngle(_currentRotation.y, _globalZRotation);

            if (Mathf.Abs(_deltaRotationY) > _maxRotationAbsYAngle)
            {
                _targetRotationY = _globalZRotation - Mathf.Sign(_deltaRotationY) * _maxRotationAbsYAngle;

                _transform.rotation = Quaternion.Euler(_currentRotation.x, _targetRotationY, _currentRotation.z);
            }
        }
    }
}
