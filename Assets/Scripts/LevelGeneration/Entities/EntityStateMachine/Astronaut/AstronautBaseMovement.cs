namespace LevelGeneration.Entities.EntityStateMachine.Astronaut
{
    public class AstronautBaseMovement : EntityBaseState, IReactableOnCatch, IReactableOnToss
    {
        private NPCMovement _npcMovement;
        private IPlaceableToVehicle _placementPattern;

        public AstronautBaseMovement(IEntityStateSwitcher stateSwitcher, NPCMovement npcMovement,
            IPlaceableToVehicle placementPattern) : base(stateSwitcher)
        {
            _npcMovement = npcMovement;
            _placementPattern = placementPattern;
        }

        public override void Start()
        {
            _npcMovement.enabled = true;
        }

        public override void Stop()
        {
            _npcMovement.Disable();
        }

        public void ReactOnEntryVehicleCatchZone()
        {
            _ = _placementPattern.TryPlaceToVehicle();
        }

        public void ReactOnEntryVehicleTossZone()
        {
            StateSwitcher.SwitchState<KnockedState>();
        }
    }
}
