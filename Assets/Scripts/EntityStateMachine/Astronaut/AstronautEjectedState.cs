using System;
using System.Collections;
using UnityEngine;

public class AstronautEjectedState : EjectedFromVehicleState
{
    public AstronautEjectedState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Animator animator,
        Collider collider) : base(stateSwitcher)
    {
        Rigidbody = rigidbody;
        Animator = animator;
        Collider = collider;
        StateSwitcher = stateSwitcher;
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
}
