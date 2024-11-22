namespace LevelGeneration.Entities.EntityStateMachine
{
    public abstract class InsideVehicleAttachState : EntityBaseState
    {
        protected InsideVehicleAttachState(IEntityStateSwitcher stateSwitcher) : base(stateSwitcher)
        {
        }
    }
}
