using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautInsideAttachState : InsideVehicleAttachState
{
    private const string LevitatingTrigger = "isLevitating";
    private const string IdleTrigger = "isIdle";

    private Animator _animator;
    private IPlaceableToVehicle _placementPattern;

    public AstronautInsideAttachState(IEntityStateSwitcher stateSwitcher, Animator animator, 
        IPlaceableToVehicle placementPattern) : base(stateSwitcher)
    {
        _animator = animator;
        _placementPattern = placementPattern;
    }

    public override void Start()
    {
        _animator.SetBool(LevitatingTrigger, false);

        Move();
    }

    public override void Move()
    {
        _placementPattern.PlaceToVehicle();

        _animator.SetBool(IdleTrigger, true);
    }

    public override void ReactOnEntryVehicleCatchZone()
    {
        NoReact();
    }

    public override void ReactOnEntryVehicleTossZone()
    {
        NoReact();
    }

    public override void Stop()
    {
        
    }
}
