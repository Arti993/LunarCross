namespace LevelGeneration.Entities.EntityStateMachine
{
    public abstract class ReactableOnCatchState : EntityBaseState
    {
        protected ReactableOnCatchState(IEntityStateSwitcher stateSwitcher) : base(stateSwitcher)
        {
        }

        public abstract void ReactOnEntryVehicleCatchZone();
    }
}
