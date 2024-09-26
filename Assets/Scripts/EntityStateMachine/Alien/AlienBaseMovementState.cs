public class AlienBaseMovementState : BaseMovementState
{
    private NPCMovement _npcMovement;

    public AlienBaseMovementState(IEntityStateSwitcher stateSwitcher, NPCMovement npcMovement) : base(stateSwitcher)
    {
        _npcMovement = npcMovement;
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
        NoReact();
    }

    public override void ReactOnEntryVehicleTossZone()
    {
        StateSwitcher.SwitchState<KnockedState>();
    }
}
