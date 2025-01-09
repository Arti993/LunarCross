using System.Collections.Generic;
using System.Linq;

namespace LevelGeneration.Entities.EntityStateMachine
{
    public class EntityBehaviour : Entity, IEntityStateSwitcher
    {
        protected IReadOnlyList<EntityBaseState> AllStates;
        
        protected EntityBaseState CurrentState { get; private set; }

        public void ReactOnEntryVehicleCatchZone()
        {
            if(CurrentState is IReactableOnCatch reactableOnCatchState)
                reactableOnCatchState.ReactOnEntryVehicleCatchZone();
        }

        public void ReactOnEntryVehicleTossZone()
        {
            if(CurrentState is IReactableOnToss reactableOnTossState)
                reactableOnTossState.ReactOnEntryVehicleTossZone();
        }

        public void SwitchState<T>() where T : EntityBaseState
        {
            var state = AllStates.FirstOrDefault(state => state is T);

            if (state == CurrentState)
                return;

            CurrentState?.Stop();

            CurrentState = state;

            CurrentState.Start();
        }

        protected EntityBaseState GetCurrentState()
        {
            return CurrentState;
        }
    }
}
