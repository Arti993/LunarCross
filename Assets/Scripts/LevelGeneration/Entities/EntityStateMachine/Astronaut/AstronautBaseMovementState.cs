namespace LevelGeneration.Entities.EntityStateMachine.Astronaut
{
    public class AstronautBaseMovementState : BaseMovementState
    {
        private NPCMovement _npcMovement;
        private IPlaceableToVehicle _placementPattern;

        public AstronautBaseMovementState(IEntityStateSwitcher stateSwitcher, NPCMovement npcMovement,
            IPlaceableToVehicle placementPattern) : base(stateSwitcher)
        {
            _npcMovement = npcMovement;
            _placementPattern = placementPattern;
        }

        public override void Start()
        {
            Move();
        }

        public override void Stop()
        {
            _npcMovement.Disable();
        }

        public override void Move()
        {
            _npcMovement.enabled = true;
        }

        public override void ReactOnEntryVehicleCatchZone()
        {
            _ = _placementPattern.TryPlaceToVehicle();
        }

        public override void ReactOnEntryVehicleTossZone()
        {
            StateSwitcher.SwitchState<KnockedState>();
        }
    }
}
