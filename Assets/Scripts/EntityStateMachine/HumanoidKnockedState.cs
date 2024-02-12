using UnityEngine;

public class HumanoidKnockedState : KnockedByVehicleState
{
    private Ragdoll _ragdoll;
    private Rigidbody _spineRigidbody;
    private Rigidbody _mainRigidbody;


    public HumanoidKnockedState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Ragdoll ragdollFly) : base(stateSwitcher, rigidbody)
    {
        _mainRigidbody = rigidbody;
        _ragdoll = ragdollFly;
        MovementDirection = new Vector3(Random.Range(-0.25f, 0.25f), 1, 0f);
        MovementSpeed = 55f;
        TimeToDestroy = 4f;
    }
    public override void Move()
    {
        _ragdoll.TurnOn();

        _spineRigidbody = _ragdoll.GetSpineRigidbody();

        _spineRigidbody.velocity = MovementDirection * MovementSpeed;
        _spineRigidbody.angularVelocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1f), Random.Range(-1f, 1f)).normalized;

        Object.Destroy(_mainRigidbody.gameObject, TimeToDestroy);
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
