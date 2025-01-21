using System.Collections;
using UnityEngine;

namespace LevelGeneration.Entities
{
    public class RadarMovement : Movement
    {
        [SerializeField] private float _rotationAngle = 120;
        [SerializeField] private float _rotationHalfCycleTime = 2f;
        
        private Quaternion _startRotation;
        private Quaternion _endRotation;
        private float _angleCovered;
        private float _fullRotationAngle;
        private float _rotationPathCovered;

        protected override IEnumerator StartLoopMovementAfterDelay()
        {
            Transform.rotation = Quaternion.identity;

            _startRotation = transform.rotation;
            _endRotation = Quaternion.Euler(_startRotation.x, _startRotation.y + _rotationAngle, _startRotation.z);
            _fullRotationAngle = Quaternion.Angle(_startRotation, _endRotation);
            
            yield return new WaitForSeconds(DelayBeforeMove);

            DetectMovementStart();

            while (IsLoopMoving)
            {
                _angleCovered = (Time.time - StartTime) * _fullRotationAngle / _rotationHalfCycleTime;
                _rotationPathCovered = _angleCovered / _fullRotationAngle;

                Transform.rotation = Quaternion.Lerp(_startRotation, _endRotation, _rotationPathCovered);

                if (_rotationPathCovered >= 1)
                {
                    Quaternion temp = _startRotation;
                    _startRotation = _endRotation;
                    _endRotation = temp;
                    StartTime = Time.time;
                }

                yield return null;
            }
        }
    }
}
