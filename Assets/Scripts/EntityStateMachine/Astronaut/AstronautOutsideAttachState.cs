using System.Collections;
using UnityEngine;

public class AstronautOutsideAttachState : OutsideVehicleAttachState
{
    private const string LevitatingTrigger = "isLevitating";

    private Rigidbody _rigidbody;
    private Animator _animator;
    private IEntityStateSwitcher _stateSwitcher;
    private IPlaceableToVehicle _placementPattern;

    private EntityBehaviour _entityBehaviour;
    private Coroutine _changeAttachStateCoroutine;
    private float _changeAttachStateTime = 7;

    public AstronautOutsideAttachState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Animator animator, IPlaceableToVehicle placementPattern) : base(stateSwitcher)
    {
        _rigidbody = rigidbody;
        _animator = animator;
        _stateSwitcher = stateSwitcher;
        _placementPattern = placementPattern;
    }

    public override void Start()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.TryGetComponent(out EntityBehaviour behaviour);
        _entityBehaviour = behaviour;

        Move();
    }

    public override void Move()
    {
        _placementPattern.PlaceToVehicle();

        _animator.SetBool(LevitatingTrigger, true);

        if (_entityBehaviour != null)
            _changeAttachStateCoroutine = _entityBehaviour.StartCoroutine(WaitToInsideAttach());

    }

    public override void Stop()
    {
        if (_changeAttachStateCoroutine != null)
        {
            _entityBehaviour.StopCoroutine(_changeAttachStateCoroutine);

            _changeAttachStateCoroutine = null;
        }

    }

    public override void ReactOnEntryVehicleCatchZone()
    {
        NoReact();
    }

    public override void ReactOnEntryVehicleTossZone()
    {
        NoReact();
    }

    private IEnumerator WaitToInsideAttach()
    {
        yield return new WaitForSeconds(_changeAttachStateTime);

        if (_entityBehaviour.GetComponentInParent<BindPoint>().IsFree == false)
        {
            if (_changeAttachStateCoroutine != null)
            {
                _stateSwitcher.SwitchState<InsideVehicleAttachState>();
                _changeAttachStateCoroutine = null;
            }
        }
    }
}
