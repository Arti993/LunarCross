using UnityEngine;

public class KnockedByVehicleState : EntityBaseState
{
    private readonly Rigidbody _rigidbody;

    protected Vector3 MovementDirection;
    protected float MovementSpeed;

    public float TimeToDestroy { get; protected set; }

    public KnockedByVehicleState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody) : base(stateSwitcher)
    {
        _rigidbody = rigidbody;
    }

    public override void Start()
    {
        if (_rigidbody.gameObject.isStatic)
            _rigidbody.gameObject.isStatic = false;

        _rigidbody.isKinematic = false;
        _rigidbody.velocity = Vector3.zero;

        Move();
    }

    public override void Move()
    {
        _rigidbody.velocity = MovementDirection * MovementSpeed;
        _rigidbody.angularVelocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1f), Random.Range(-1f, 1f)).normalized;

        Object.Destroy(_rigidbody.gameObject, TimeToDestroy);
    }

    public override void ReactOnEntryVehicleCatchZone()
    {
        NoReact();
    }

    public override void ReactOnEntryVehicleTossZone()
    {
        NoReact();
    }

    public override void Stop()
    {

    }
}
