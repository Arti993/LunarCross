using UnityEngine;

public class AlienKnockedState : HumanoidKnockedState
{
    private readonly Collider _collider;
    
    public AlienKnockedState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Ragdoll ragdollbody, Collider collider) : 
        base(stateSwitcher, rigidbody, ragdollbody)
    {
        _collider = collider;
    }

    public override void Move()
    {
        base.Move();

        _collider.enabled = false;
    }
}
