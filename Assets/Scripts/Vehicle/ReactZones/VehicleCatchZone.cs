using LevelGeneration.Entities.EntityStateMachine;
using UnityEngine;

namespace Vehicle.ReactZones
{
    public abstract class VehicleCatchZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out EntityBehaviour entityBehaviour))
            {
                entityBehaviour.ReactOnEntryVehicleCatchZone();
            }
        }
    }
}
