using System.Collections;
using UnityEngine;

public class SatelliteMovement : Movement
{
    [SerializeField] private float _levitationHeight = 0.45f;
    [SerializeField] private float _levitationHalfCycleTime = 2f;
    [SerializeField] private float _minDelayBeforeMove = 0f;
    [SerializeField] private float _maxDelayBeforeMove = 0.7f;

    private Vector3 _startPoint;
    private Vector3 _endPoint;
    private float _offsetY = 0.45f;
    private float _startTime;
    private float _delayBeforeMove;
    private float _distanceCovered;
    private float _journeyLength;
    private float _journeyPathCovered;
    private bool _isFirstAwake = true;
    private Transform _transform;
    
    private void Start()
    {
        _transform = transform;
        
        Move();
    }

    private void OnEnable()
    {
        if (_isFirstAwake)
        {
            _isFirstAwake = false;
            return;
        }

        Move();
    }

    public override void Move()
    {
        StartCoroutine(StartMovementAfterDelay());
    }
    
    private IEnumerator StartMovementAfterDelay()
    {
        yield return new WaitForSeconds(_delayBeforeMove);

        _startPoint = new Vector3(_transform.position.x, _transform.position.y + _offsetY, _transform.position.z);
        _transform.position = _startPoint;
        _endPoint = new Vector3(_startPoint.x, _startPoint.y + _levitationHeight, _startPoint.z);
        _journeyLength = Vector3.Distance(_startPoint, _endPoint);
        _delayBeforeMove = Random.Range(_minDelayBeforeMove, _maxDelayBeforeMove);

        MovingCoroutine = StartCoroutine(LoopMovement());
    }

    private IEnumerator LoopMovement()
    {
        _startTime = Time.time;
        IsMoving = true;

        while (IsMoving)
        {
            _distanceCovered = (Time.time - _startTime) * _journeyLength / _levitationHalfCycleTime;
            _journeyPathCovered = _distanceCovered / _journeyLength;

            _transform.position = Vector3.Lerp(_startPoint, _endPoint, _journeyPathCovered);

            if (_journeyPathCovered >= 1)
            {
                Vector3 temp = _startPoint;
                _startPoint = _endPoint;
                _endPoint = temp;
                _startTime = Time.time;
            }

            yield return null;
        }
    }
}