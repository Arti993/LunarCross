using System.Collections;
using UnityEngine;

namespace LevelGeneration.Entities
{
    public class SatelliteMovement : Movement
    {
        private const float OffsetY = 0.45f;

        [SerializeField] private float _levitationHeight = 0.45f;
        [SerializeField] private float _levitationHalfCycleTime = 2f;

        private Vector3 _startPoint;
        private Vector3 _endPoint;
        private float _distanceCovered;
        private float _journeyLength;
        private float _journeyPathCovered;
        
        protected override IEnumerator StartLoopMovementAfterDelay()
        {
            _startPoint = new Vector3(Transform.position.x, Transform.position.y + OffsetY, Transform.position.z);
            _endPoint = new Vector3(_startPoint.x, _startPoint.y + _levitationHeight, _startPoint.z);
            
            _journeyLength = Vector3.Distance(_startPoint, _endPoint);
            
            Transform.position = _startPoint;

            yield return new WaitForSeconds(DelayBeforeMove);

            DetectMovementStart();

            while (IsLoopMoving)
            {
                _distanceCovered = (Time.time - StartTime) * _journeyLength / _levitationHalfCycleTime;
                _journeyPathCovered = _distanceCovered / _journeyLength;

                Transform.position = Vector3.Lerp(_startPoint, _endPoint, _journeyPathCovered);

                if (_journeyPathCovered >= 1)
                {
                    Vector3 temp = _startPoint;
                    _startPoint = _endPoint;
                    _endPoint = temp;
                    StartTime = Time.time;
                }

                yield return null;
            }
        }
    }
}