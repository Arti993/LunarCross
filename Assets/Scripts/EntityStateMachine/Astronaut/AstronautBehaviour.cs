using System.Collections.Generic;
using System.Linq;

public class AstronautBehaviour : NpcBehaviour, IPlaceableToVehicle
{
    private IPlaceableToVehicle _placementPattern;

    private void Start()
    {
        _placementPattern = new PlacementToVehiclePattern(this);

        AllStates = new List<EntityBaseState>()
        {
            new AstronautBaseMovementState(this, NpcMovement),
            new AstronautInsideAttachState(this, Animator, _placementPattern),
            new AstronautOutsideAttachState(this, Rigidbody, Animator, _placementPattern),
            new AstronautEjectedState(this, Rigidbody, Animator, Collider, _placementPattern),
            new AstronautKnockedState(this, Rigidbody, RagdollFly),
            new RisingByGravityRayState(this, Rigidbody, Animator, Collider, _placementPattern)
        };

        CurrentState = AllStates.First();
        CurrentState.Start();
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
