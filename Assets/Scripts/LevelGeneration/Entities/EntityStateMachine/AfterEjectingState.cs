namespace LevelGeneration.Entities.EntityStateMachine
{
    public abstract class AfterEjectingState : EntityBaseState
    {
        protected AfterEjectingState(IEntityStateSwitcher stateSwitcher) : base(stateSwitcher)
        {
        }
    }
}
