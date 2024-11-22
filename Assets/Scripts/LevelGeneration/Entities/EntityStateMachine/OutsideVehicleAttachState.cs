namespace LevelGeneration.Entities.EntityStateMachine
{
    public abstract class OutsideVehicleAttachState : EntityBaseState
    {
        protected OutsideVehicleAttachState(IEntityStateSwitcher stateSwitcher) : base(stateSwitcher)
        {
        }
    }
}