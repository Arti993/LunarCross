public class AlienBaseMovementState : BaseMovementState
{
    private NPCMovement _npcMovement;
    private IEntityStateSwitcher _stateSwitcher;

    public AlienBaseMovementState(IEntityStateSwitcher stateSwitcher, NPCMovement npcMovement) : base(stateSwitcher)
    {
        _npcMovement = npcMovement;
        _stateSwitcher = stateSwitcher;
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
        _stateSwitcher.SwitchState<KnockedState>();
    }
}
