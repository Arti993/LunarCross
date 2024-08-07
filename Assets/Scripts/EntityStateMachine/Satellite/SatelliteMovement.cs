using System.Collections;
using UnityEngine;

public class SatelliteMovement : Movement
{
    [SerializeField] private float _levitationHeight = 1f;
    [SerializeField] private float _levitationHalfCycleTime = 2f;
    [SerializeField] private float _minDelayBeforeMove = 0f;
    [SerializeField] private float _maxDelayBeforeMove = 0.7f;


    private Vector3 _startPoint;
    private Vector3 _endPoint;
    private float _startTime;
    private float _delayBeforeMove;
    private float _distanceCovered;
    private float _journeyLength;
    private float _journeyPathCovered;

    private void OnEnable()
    {
        _startPoint = transform.position;
        _endPoint = new Vector3(_startPoint.x, _startPoint.y + _levitationHeight, _startPoint.z);
        _journeyLength = Vector3.Distance(_startPoint, _endPoint);
        _delayBeforeMove = Random.Range(_minDelayBeforeMove, _maxDelayBeforeMove);

        Move();
    }

    public override void Move()
    {
        StartCoroutine(StartMovementAfterDelay());
    }

    private IEnumerator StartMovementAfterDelay()
    {
        yield return new WaitForSeconds(_delayBeforeMove);

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

            transform.position = Vector3.Lerp(_startPoint, _endPoint, _journeyPathCovered);

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

    private void OnDisable()
    {
        if (MovingCoroutine != null)
            StopCoroutine(MovingCoroutine);

        IsMoving = false;
    }
}