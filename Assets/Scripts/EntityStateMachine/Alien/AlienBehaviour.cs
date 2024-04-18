using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlienBehaviour : NpcBehaviour
{
    private void Start()
    {
        AllStates = new List<EntityBaseState>()
        {
            new AlienBaseMovementState(this, NpcMovement),
            new AlienAfterEjectingState(this, Rigidbody),
            new AlienKnockedState(this, Rigidbody, RagdollFly, Collider)
        };

        CurrentState = AllStates.First();
        CurrentState.Start();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out BindPoint bindpoint))
        {
            if(bindpoint.IsFree == false)
            {
                this.SwitchState<AlienAfterEjectingState>();
            }
        }

        if (other.gameObject.TryGetComponent(out VehicleCatchBehaviour vehicle))
        {
            this.SwitchState<AlienAfterEjectingState>();
        }
    }
}
