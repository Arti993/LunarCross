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
            new AstronautEjectedState(this, Rigidbody, Animator, Collider),
            new HumanoidKnockedState(this, Rigidbody, RagdollFly),
            new RisingByRayState(this, Rigidbody, Animator, Collider),
        };

        CurrentState = AllStates.First();
        CurrentState.Start();
    }

    public void PlaceToVehicle()
    {
        _placementPattern.PlaceToVehicle();
    }

    public void UnplaceFromVehicle()
    {
        _placementPattern.UnplaceFromVehicle();
    }
}
