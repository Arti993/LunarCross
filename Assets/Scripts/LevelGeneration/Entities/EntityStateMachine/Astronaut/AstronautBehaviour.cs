using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration.Entities.EntityStateMachine.Astronaut
{
    public class AstronautBehaviour : NpcBehaviour
    {
        private IPlaceableToVehicle _placementPattern;

        private void OnEnable()
        {
            if (AllStates != null)
                SwitchState<AstronautBaseMovementState>();
        }

        private void Start()
        {
            if (AllStates == null)
            {
                _placementPattern = new PlacementToVehiclePattern(this);

                AllStates = new List<EntityBaseState>()
                {
                    new AstronautBaseMovementState(this, NpcMovement, _placementPattern),
                    new AstronautInsideAttachState(this, Animator),
                    new AstronautOutsideAttachState(this, Rigidbody, Animator, _placementPattern),
                    new AstronautEjectedState(this, Rigidbody, Animator, Collider, _placementPattern),
                    new HumanoidKnockedState(this, Rigidbody, RagdollFly, Collider),
                    new AstronautGroundHitState(this, Rigidbody, RagdollFly, Collider),
                    new RisingByGravityRayState(this, Rigidbody, Animator, Collider, _placementPattern),
                    new NoActionState(this)
                };
            }

            SwitchState<AstronautBaseMovementState>();
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            if (CurrentState is AstronautEjectedState)
            {
                if (otherCollider.gameObject.TryGetComponent(out ChunkGround chunkGround))
                    SwitchState<AstronautGroundHitState>();
            }
        }
    }
}