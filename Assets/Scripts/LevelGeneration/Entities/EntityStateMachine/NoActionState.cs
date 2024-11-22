namespace LevelGeneration.Entities.EntityStateMachine
{
    public class NoActionState : EntityBaseState
    {
        public NoActionState(IEntityStateSwitcher stateSwitcher) : base(stateSwitcher)
        {
        }

        public override void Start()
        {
            NoReact();
        }

        public override void Stop()
        {
            NoReact();
        }

        public override void Move()
        {
            NoReact();
        }

        public override void ReactOnEntryVehicleCatchZone()
        {
            NoReact();
        }

        public override void ReactOnEntryVehicleTossZone()
        {
            NoReact();
        }
    }
}
