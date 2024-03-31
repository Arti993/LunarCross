using UnityEngine;

public class KnockedByVehicleState : EntityBaseState
{
    protected Rigidbody Rigidbody;
    protected Vector3 MovementDirection;
    protected float MovementSpeed;

    public KnockedByVehicleState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody) : base(stateSwitcher)
    {
        Rigidbody = rigidbody;
        MovementDirection = new Vector3(Random.Range(-0.25f, 0.25f), 1, 0f);
        MovementSpeed = 55;
    }

    public override void Start()
    {
        if (Rigidbody.gameObject.isStatic)
            Rigidbody.gameObject.isStatic = false;

        Rigidbody.isKinematic = false;
        Rigidbody.velocity = Vector3.zero;

        Move();
    }

    public override void Move()
    {
        Rigidbody.velocity = MovementDirection * MovementSpeed;
        Rigidbody.angularVelocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1f), Random.Range(-1f, 1f)).normalized;
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
