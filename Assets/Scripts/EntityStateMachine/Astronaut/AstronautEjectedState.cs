using UnityEngine;

public class AstronautEjectedState : EjectedFromVehicleState
{
    private const float MaxDirectionValueX = 0.1f;
    private const float MinDirectionValueX = -0.1f;
    private const float DirectionValueY = 1;
    private const float DirectionValueZ = 0.7f;
    private const float Multiplier = 10;
    private const float DragFactor = 0.7f; 
    
    public AstronautEjectedState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Animator animator, Collider collider, 
        IPlaceableToVehicle placementPattern) : base(stateSwitcher, rigidbody, animator, collider, placementPattern)
    {
    }

    public override void Move()
    {
        Rigidbody.isKinematic = false;
        Rigidbody.useGravity = true;
        Rigidbody.drag = DragFactor;
        
        Vector3 movementDirection = new Vector3(Random.Range(MinDirectionValueX, MaxDirectionValueX), DirectionValueY, DirectionValueZ);

        Rigidbody.AddForce(movementDirection * Multiplier, ForceMode.Impulse);
    }

    public override void ReactOnEntryVehicleCatchZone()
    {
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.isKinematic = true;
        Rigidbody.useGravity = false;
        PlacementPattern.TryPlaceToVehicle();
    }
}
