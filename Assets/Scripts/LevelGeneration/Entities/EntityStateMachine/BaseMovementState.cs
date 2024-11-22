namespace LevelGeneration.Entities.EntityStateMachine
{
    public abstract class BaseMovementState : EntityBaseState
    {
        protected BaseMovementState(IEntityStateSwitcher stateSwitcher) : base(stateSwitcher)
        {
        }
    }
}