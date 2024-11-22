using LevelGeneration.Entities.EntityStateMachine;
using UnityEngine;

namespace Vehicle.ReactZones
{
    public class Bumper : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out EntityBehaviour entityBehaviour))
            {
                entityBehaviour.ReactOnEntryVehicleTossZone();
            }
        }
    }
}
