using System;
using System.Collections;
using UnityEngine;

public class EjectedFromVehicleState : EntityBaseState
{
    private const string LevitatingTrigger = "isLevitating";
    private const string IdleTrigger = "isIdle";
    private const float TimeToEnableCollider = 1;
    
    protected Rigidbody Rigidbody;
    protected Animator Animator;
    protected Collider Collider;
    protected IPlaceableToVehicle PlacementPattern;
    private EntityBehaviour _entity;
    
    protected EjectedFromVehicleState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Animator animator,
        Collider collider, IPlaceableToVehicle placementPattern) : base(stateSwitcher)
    {
        Rigidbody = rigidbody;
        Animator = animator;
        Collider = collider;
        PlacementPattern = placementPattern;
    }

    public override void Start()
    {
        _ = Rigidbody.TryGetComponent(out EntityBehaviour entity);

        _entity = entity;

        _entity.transform.SetParent(null);

        if (_entity == null)
            throw new InvalidOperationException();

        Collider.enabled = false;

        _ = _entity.StartCoroutine(WaitToEnableCollider());
        
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
        _ = PlacementPattern.TryPlaceToVehicle();
    }

    public override void ReactOnEntryVehicleTossZone()
    {
    }

    private IEnumerator WaitToEnableCollider()
    {
        yield return new WaitForSeconds(TimeToEnableCollider);  

        Collider.enabled = true;
    }
}
