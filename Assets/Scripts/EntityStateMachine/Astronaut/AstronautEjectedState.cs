using System;
using System.Collections;
using UnityEngine;

public class AstronautEjectedState : EjectedFromVehicleState
{
    public AstronautEjectedState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Animator animator, Collider collider, 
        IPlaceableToVehicle placementPattern) : base(stateSwitcher, rigidbody, animator, collider, placementPattern)
    {
    }

    public override void Move()
    {
        //убрать магические числа
        
        Rigidbody.isKinematic = false;
        Rigidbody.useGravity = true;
        Rigidbody.drag = 0.7f;
        
        Vector3 movementDirection = new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), 1, 0.7f);  // подправить

        Rigidbody.AddForce(movementDirection * 10f, ForceMode.Impulse);
    }

    public override void ReactOnEntryVehicleCatchZone()
    {
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.isKinematic = true;
        Rigidbody.useGravity = false;
        PlacementPattern.TryPlaceToVehicle();
    }
}
