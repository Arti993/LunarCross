using System;
using System.Collections;
using UnityEngine;

public class EjectedFromVehicleState : EntityBaseState
{
    private const string LevitatingTrigger = "isLevitating";
    private const string IdleTrigger = "isIdle";
    
    protected Rigidbody Rigidbody;
    protected Animator Animator;
    protected Collider Collider;
    private EntityBehaviour _entity;
    private float _timeToEnableCollider = 1;
    private readonly IPlaceableToVehicle _placementPattern;
    
    protected EjectedFromVehicleState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Animator animator,
        Collider collider, IPlaceableToVehicle placementPattern) : base(stateSwitcher)
    {
        Rigidbody = rigidbody;
        Animator = animator;
        Collider = collider;
        _placementPattern = placementPattern;
    }

    public override void Start()
    {
        Rigidbody.TryGetComponent(out EntityBehaviour entity);

        _entity = entity;

        _entity.transform.SetParent(null);

        if (_entity == null)
            throw new InvalidOperationException();

        Collider.enabled = false;

        _entity.StartCoroutine(WaitToEnableCollider());
        
        if (Animator.GetBool(IdleTrigger))
            Animator.SetBool(IdleTrigger, false);

        if (Animator.GetBool(LevitatingTrigger) == false)
            Animator.SetBool(LevitatingTrigger, true);

        Move();
    }

    public override void Stop()
    {
    }

    public override void Move()
    {
    }

    public override void ReactOnEntryVehicleCatchZone()
    {
        _placementPattern.TryPlaceToVehicle();
    }

    public override void ReactOnEntryVehicleTossZone()
    {
    }

    private IEnumerator WaitToEnableCollider()
    {
        yield return new WaitForSeconds(_timeToEnableCollider);  

        Collider.enabled = true;
    }
}
