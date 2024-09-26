using System.Collections.Generic;
using System.Linq;

public class EntityBehaviour : Entity, IEntityStateSwitcher
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
        
        if(state == CurrentState)
            return;

        CurrentState?.Stop();

        CurrentState = state;

        CurrentState.Start();
    }

    public EntityBaseState GetCurrentState()
    {
        return CurrentState;
    }

    public override void Disable()
    {
        SwitchState<NoActionState>();
        
        base.Disable();
    }
}
