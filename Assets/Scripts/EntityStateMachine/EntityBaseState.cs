using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBaseState
{
    protected readonly IEntityStateSwitcher StateSwitcher;

    protected EntityBaseState(IEntityStateSwitcher stateSwitcher)
    {
        StateSwitcher = stateSwitcher;
    }

    public abstract void Start();
    public abstract void Stop();
    public abstract void Move();
    public abstract void ReactOnEntryVehicleCatchZone();
    public abstract void ReactOnEntryVehicleTossZone();

    protected virtual void NoReact()
    {
    }
}
