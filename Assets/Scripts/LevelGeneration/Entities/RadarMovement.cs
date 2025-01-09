using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelGeneration.Entities
{
    public class RadarMovement : Movement
    {
        [SerializeField] private float _rotationAngle = 120;
        [SerializeField] private float _rotationHalfCycleTime = 2f;
        [SerializeField] private float _minDelayBeforeMove = 0f;
        [SerializeField] private float _maxDelayBeforeMove = 0.7f;

        private Transform _transform;
        private Quaternion _startRotation;
        private Quaternion _endRotation;
        private float _startTime;
        private float _delayBeforeMove;
        private float _angleCovered;
        private float _fullRotationAngle;
        private float _rotationPathCovered;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _transform.rotation = Quaternion.identity;

            _startRotation = transform.rotation;
            _endRotation = Quaternion.Euler(_startRotation.x, _startRotation.y + _rotationAngle, _startRotation.z);
            _fullRotationAngle = Quaternion.Angle(_startRotation, _endRotation);
            _delayBeforeMove = Random.Range(_minDelayBeforeMove, _maxDelayBeforeMove);

            Move();
        }

        protected override IEnumerator StartMovementAfterDelay()
        {
            yield return new WaitForSeconds(_delayBeforeMove);

            _startTime = Time.time;
            IsMoving = true;

            while (IsMoving)
            {
                _angleCovered = (Time.time - _startTime) * _fullRotationAngle / _rotationHalfCycleTime;
                _rotationPathCovered = _angleCovered / _fullRotationAngle;

                _transform.rotation = Quaternion.Lerp(_startRotation, _endRotation, _rotationPathCovered);

                if (_rotationPathCovered >= 1)
                {
                    Quaternion temp = _startRotation;
                    _startRotation = _endRotation;
                    _endRotation = temp;
                    _startTime = Time.time;
                }

                yield return null;
            }
        }
    }
}
