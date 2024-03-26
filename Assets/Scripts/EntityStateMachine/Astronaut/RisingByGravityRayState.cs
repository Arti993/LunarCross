using UnityEngine;

public class RisingByGravityRayState : EjectedFromVehicleState
{
    //сделать отдельный интерфейс под захват и бамбер
    
    
    public RisingByGravityRayState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Animator animator,
        Collider collider) : base(stateSwitcher)
     {
         Rigidbody = rigidbody;
         Animator = animator;
         Collider = collider;
     }

    public override void Move()
    {
        Rigidbody.isKinematic = false;
        Rigidbody.useGravity = false;
        
        Rigidbody.AddForce(Vector3.up, ForceMode.VelocityChange);
    }
}
