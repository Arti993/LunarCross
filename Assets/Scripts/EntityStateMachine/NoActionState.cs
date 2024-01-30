using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoActionState : EntityBaseState
{
    public NoActionState(IEntityStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Move()
    {

    }

    public override void ReactOnEntryVehicleCatchZone()
    {
        NoReact();
    }

    public override void ReactOnEntryVehicleTossZone()
    {
        NoReact();
    }

    public override void Start()
    {

    }

    public override void Stop()
    {

    }
}
