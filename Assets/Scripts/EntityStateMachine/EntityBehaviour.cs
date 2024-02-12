using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour, IEntityStateSwitcher
{
    protected IReadOnlyList<EntityBaseState> AllStates;
    public EntityBaseState CurrentState { get; protected set; }

    public void Move()
    {
        CurrentState.Move();
    }

    public void ReactOnEntryVehicleCatchZone()
    {
        CurrentState.ReactOnEntryVehicleCatchZone();
    }

    public void ReactOnEntryVehicleTossZone() 
    {
        CurrentState.ReactOnEntryVehicleTossZone();
    }

    public void SwitchState<T>() where T : EntityBaseState
    {
        var state = AllStates.FirstOrDefault(state => state is T);

        CurrentState.Stop();

        state.Start();

        CurrentState = state;
    }
}
