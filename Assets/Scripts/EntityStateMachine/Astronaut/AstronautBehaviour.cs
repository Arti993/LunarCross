using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AstronautBehaviour : NpcBehaviour, IPlaceableToVehicle
{
    private IPlaceableToVehicle _placementPattern;

    private void Start()
    {
        _placementPattern = new PlacementToVehiclePattern(this);

        AllStates = new List<EntityBaseState>()
        {
            new AstronautBaseMovementState(this, NpcMovement, _placementPattern),
            new AstronautInsideAttachState(this, Animator, _placementPattern),
            new AstronautOutsideAttachState(this, Rigidbody, Animator, _placementPattern),
            new AstronautEjectedState(this, Rigidbody, Animator, Collider, _placementPattern),
            new AstronautKnockedbyVehicleState(this, Rigidbody, RagdollFly),
            new AstronautGroundHitState(this, Rigidbody, RagdollFly),
            new RisingByGravityRayState(this, Rigidbody, Animator, Collider, _placementPattern)
        };

        CurrentState = AllStates.First();
        CurrentState.Start();
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (CurrentState is AstronautEjectedState)
        {
            if(otherCollider.gameObject.TryGetComponent(out ChunkGround chunkGround))
                this.SwitchState<AstronautGroundHitState>();
        }
    }

    public bool TryPlaceToVehicle()
    {
        return _placementPattern.TryPlaceToVehicle();
    }

    public void UnplaceFromVehicle()
    {
        _placementPattern.UnplaceFromVehicle();
    }
}
