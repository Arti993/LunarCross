using System.Collections.Generic;
using UnityEngine;
using Vehicle;
using Vehicle.BindPoints;

namespace LevelGeneration.Entities.EntityStateMachine.Alien
{
    public class AlienBehaviour : NpcBehaviour
    {
        private void OnEnable()
        {
            if (AllStates != null)
                SwitchState<AlienBaseMovement>();

            if (TryGetComponent(out EntityToEjectDetector ejector))
                ejector.enabled = true;
        }

        private void Start()
        {
            if (AllStates == null)
            {
                AllStates = new List<EntityBaseState>()
                {
                    new AlienBaseMovement(this, NpcMovement),
                    new AlienAfterEjectingState(this, Rigidbody),
                    new HumanoidKnockedState(this, Rigidbody, RagdollFly, Collider),
                };
            }

            SwitchState<AlienBaseMovement>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _ = other.gameObject.TryGetComponent(out BindPoint bindpoint);
            _ = other.gameObject.TryGetComponent(out VehicleCatchBehaviour vehicle);

            if (vehicle != null || bindpoint != null && bindpoint.IsFree == false)
            {
                if (GetCurrentState() is HumanoidKnockedState == false)
                    SwitchState<AlienAfterEjectingState>();
            }
        }
    }
}