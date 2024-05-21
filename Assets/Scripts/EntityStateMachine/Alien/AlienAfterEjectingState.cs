using UnityEngine;

public class AlienAfterEjectingState : AfterEjectingState
{
    private IEntityStateSwitcher _stateSwitcher;
    private Rigidbody _rigidbody;
    private Transform _entityTransform;
    private float _speed = 0.5f;

    public AlienAfterEjectingState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody) : base(stateSwitcher)
    {
        _stateSwitcher = stateSwitcher;
        _rigidbody = rigidbody;
        _entityTransform = rigidbody.transform;
    }

    public override void Start()
    {
        Move();
    }

    public override void Move()
    {
        _entityTransform.rotation = Quaternion.LookRotation(-_entityTransform.forward);
        _rigidbody.velocity = _entityTransform.forward * _speed;
    }

    public override void ReactOnEntryVehicleCatchZone()
    {
        NoReact();
    }

    public override void ReactOnEntryVehicleTossZone()
    {
        _stateSwitcher.SwitchState<KnockedState>();
    }


    public override void Stop()
    {
        
    }
}
