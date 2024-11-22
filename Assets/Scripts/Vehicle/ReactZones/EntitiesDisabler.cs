using LevelGeneration;
using UnityEngine;

namespace Vehicle.ReactZones
{
    public class EntitiesDisabler : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Entity entity))
            {
                entity.Disable();
            }
        }
    }
}
