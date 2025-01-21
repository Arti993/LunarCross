using UnityEngine;

namespace LevelGeneration.Entities
{
    public class TornadoMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.8f;
        [SerializeField] private float _distance = 2.5f;

        private Vector3 _startPoint;
        private Vector3 _endPoint;
        private float _startTime;
        private float _journeyLength;

        private void Start()
        {
            _startPoint = transform.position;
            _endPoint = _startPoint + Vector3.right * _distance;
            _startTime = Time.time;
            _journeyLength = Vector3.Distance(_startPoint, _endPoint);
        }

        private void Update()
        {
            var distanceCovered = (Time.time - _startTime) * _speed;
            var fracJourney = distanceCovered / _journeyLength;
            transform.position = Vector3.Lerp(_startPoint, _endPoint, Mathf.PingPong(fracJourney, 1));
        }
    }
}
