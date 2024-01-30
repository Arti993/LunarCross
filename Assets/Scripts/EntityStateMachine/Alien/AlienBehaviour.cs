using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlienBehaviour : NpcBehaviour, IEjectorFromVehicle
{
    private IPlaceableToVehicle _entityToEject;

    private void Start()
    {
        AllStates = new List<EntityBaseState>()
        {
            new AlienBaseMovementState(this, NpcMovement),
            new AlienAfterEjectingState(this, Rigidbody),
            new HumanoidKnockedState(this, Rigidbody, RagdollFly)
        };

        CurrentState = AllStates.First();
        CurrentState.Start();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out BindPoint bindpoint))
        {
            if(bindpoint != null && bindpoint.IsFree == false)
            {
                _entityToEject = bindpoint.GetComponentInChildren<IPlaceableToVehicle>();
                Collider.enabled = false;

                Eject();
            }
        }
    }

    public void Eject()
    {
        if (_entityToEject != null)
            _entityToEject.UnplaceFromVehicle();

        this.SwitchState<AlienAfterEjectingState>();
    }
}
