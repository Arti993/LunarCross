using System.Collections;
using UnityEngine;

public class SimpleRotationState : EntityBaseState
{
    private Rigidbody _rigidbody;
    private IEntityStateSwitcher _stateSwitcher;
    private EntityBehaviour _entity;
    private Coroutine _mainCoroutine;
    private Coroutine _movingCoroutine;
    private float _rotationAngle;
    private float _rotationHalfCycleTime;

    public SimpleRotationState(IEntityStateSwitcher stateSwitcher, EntityBehaviour entity, Rigidbody rigidbody,
    float rotationAngle, float rotationHalfCycleTime) : base(stateSwitcher)
    {
        _rigidbody = rigidbody;
        _entity = entity;
        _stateSwitcher = stateSwitcher;
        _rotationAngle = rotationAngle;
        _rotationHalfCycleTime = rotationHalfCycleTime;
    }

    public override void Start()
    {
        _rigidbody.isKinematic = true;

        Move();
    }

    public override void Move()
    {    
        _mainCoroutine = _entity.StartCoroutine(Rotate());
    }

    public override void ReactOnEntryVehicleCatchZone()
    {
        _stateSwitcher.SwitchState<KnockedState>();
    }

    public override void ReactOnEntryVehicleTossZone()
    {
        _stateSwitcher.SwitchState<KnockedState>();
    }

    public override void Stop()
    {
        _entity.StopCoroutine(_movingCoroutine);
        _entity.StopCoroutine(_mainCoroutine);
    }
    
    private IEnumerator Rotate()
    {
        Quaternion rotation = _entity.transform.rotation;
        Quaternion endRotation;
        bool isFullRotate = false;

        float delayBeforeRotate = Random.Range(0, _rotationHalfCycleTime);
        
        yield return new WaitForSeconds(delayBeforeRotate);
        
        while(true)
        {
            if (isFullRotate)
            {
                endRotation = Quaternion.identity;
                isFullRotate = false;
            }
            else
            {
                endRotation = Quaternion.Euler(rotation.x, rotation.y + _rotationAngle, rotation.z);
                isFullRotate = true;
            }
            
            _movingCoroutine = _entity.StartCoroutine(Moving(endRotation));

            yield return _movingCoroutine;
        }
    }

    private IEnumerator Moving(Quaternion endRotation)
    {
        float startTime = Time.time;
        float time;
        Quaternion startRotation = _entity.transform.rotation;
        
        while (Time.time - startTime < _rotationHalfCycleTime)
        {
            time = (Time.time - startTime) / _rotationHalfCycleTime;
            _entity.transform.rotation = Quaternion.Lerp(startRotation, endRotation, time);
            yield return null;
        }
    }
}
