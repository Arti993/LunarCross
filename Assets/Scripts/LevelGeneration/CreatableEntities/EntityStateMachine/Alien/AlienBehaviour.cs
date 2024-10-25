using System.Collections.Generic;
using UnityEngine;

public class AlienBehaviour : NpcBehaviour
{
    private void OnEnable()
    {
        if(AllStates != null)
            SwitchState<AlienBaseMovementState>();

        if(TryGetComponent(out EntityToEjectDetector ejector))
            ejector.enabled = true;
    }
    
    private void Start()
    {
        if (AllStates == null)
        {
            AllStates = new List<EntityBaseState>()
            {
                new AlienBaseMovementState(this, NpcMovement),
                new AlienAfterEjectingState(this, Rigidbody),
                new HumanoidKnockedState(this, Rigidbody, RagdollFly, Collider),
                new NoActionState(this)
            };
        }

        SwitchState<AlienBaseMovementState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _ = other.gameObject.TryGetComponent(out BindPoint bindpoint);

        if (bindpoint != null && bindpoint.IsFree == false)
        {
            if(GetCurrentState() is HumanoidKnockedState == false)
                SwitchState<AlienAfterEjectingState>();
        }

        if (other.gameObject.TryGetComponent(out VehicleCatchBehaviour vehicle))
        {
            if(GetCurrentState() is HumanoidKnockedState == false)
                SwitchState<AlienAfterEjectingState>();
        }
    }
}