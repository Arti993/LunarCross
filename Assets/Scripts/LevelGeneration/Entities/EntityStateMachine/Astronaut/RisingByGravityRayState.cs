using UnityEngine;

namespace LevelGeneration.Entities.EntityStateMachine.Astronaut
{
    public class RisingByGravityRayState : EjectedFromVehicleState
    {
        public RisingByGravityRayState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Animator animator,
            Collider collider,
            IPlaceableToVehicle placementPattern) : base(stateSwitcher, rigidbody, animator, collider, placementPattern)
        {
        }

        public override void Start()
        {
            base.Start();
            
            Rigidbody.isKinematic = false;
            Rigidbody.useGravity = false;

            Rigidbody.AddForce(Vector3.up, ForceMode.VelocityChange);
        }
    }
}
