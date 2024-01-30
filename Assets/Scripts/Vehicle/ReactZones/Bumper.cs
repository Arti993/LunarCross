using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EntityBehaviour entityBehaviour))
        {
            entityBehaviour.ReactOnEntryVehicleTossZone();
        }
    }
}
