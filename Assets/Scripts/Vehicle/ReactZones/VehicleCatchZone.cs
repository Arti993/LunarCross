using UnityEngine;

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
