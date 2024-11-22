namespace LevelGeneration.Entities.EntityStateMachine
{
    public interface IEntityStateSwitcher
    {
        public void SwitchState<T>() where T : EntityBaseState;
    }
}
