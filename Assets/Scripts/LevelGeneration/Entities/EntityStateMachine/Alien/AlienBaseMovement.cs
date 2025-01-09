namespace LevelGeneration.Entities.EntityStateMachine.Alien
{
    public class AlienBaseMovement : EntityBaseState, IReactableOnToss
    {
        private NPCMovement _npcMovement;

        public AlienBaseMovement(IEntityStateSwitcher stateSwitcher, NPCMovement npcMovement) : base(stateSwitcher)
        {
            _npcMovement = npcMovement;
        }

        public override void Start()
        {
            _npcMovement.enabled = true;
        }

        public override void Stop()
        {
            _npcMovement.Disable();
        }
        
        public void ReactOnEntryVehicleTossZone()
        {
            StateSwitcher.SwitchState<KnockedState>();
        }
    }
}
