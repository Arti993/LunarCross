using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautBaseMovementState : BaseMovementState
{
    private NPCMovement _npcMovement;
    private IEntityStateSwitcher _stateSwitcher;

    public AstronautBaseMovementState(IEntityStateSwitcher stateSwitcher, NPCMovement npcMovement) : base(stateSwitcher)
    {
        _npcMovement = npcMovement;
        _stateSwitcher = stateSwitcher;
    }

    public override void Start()
    {
        Move();
    }

    public override void Stop()
    {
        _npcMovement.Disable();
    }

    public override void Move()
    {
        _npcMovement.enabled = true;
    }

    public override void ReactOnEntryVehicleCatchZone()
    {
        _stateSwitcher.SwitchState<OutsideVehicleAttachState>();
    }

    public override void ReactOnEntryVehicleTossZone()
    {
        _stateSwitcher.SwitchState<KnockedByVehicleState>();
    }
}