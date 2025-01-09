namespace LevelGeneration.Entities.EntityStateMachine
{
    public abstract class EntityBaseState
    {
        protected readonly IEntityStateSwitcher StateSwitcher;

        protected EntityBaseState(IEntityStateSwitcher stateSwitcher)
        {
            StateSwitcher = stateSwitcher;
        }

        public abstract void Start();
        public abstract void Stop();
    }
}
