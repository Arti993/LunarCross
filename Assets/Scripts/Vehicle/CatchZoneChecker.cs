using JetBrains.Annotations;
using LevelGeneration.Entities.EntityStateMachine;
using UnityEngine;
using Vehicle.ReactZones;

namespace Vehicle
{
    public class CatchZoneChecker
    {
        private readonly EntityBehaviour _entity;
        private readonly float _searchRadius;

        public CatchZoneChecker(EntityBehaviour entity, float searchRadius)
        {
            _entity = entity;
            _searchRadius = searchRadius;
        }

        public bool CheckEnteringCatchZone([CanBeNull] out VehicleCatchZone catchZone)
        {
            catchZone = null;

            Collider[] intersectingColliders = Physics.OverlapSphere(_entity.transform.position, _searchRadius);

            if (intersectingColliders.Length > 0)
            {
                for (int i = 0; i < intersectingColliders.Length; i++)
                {
                    if (intersectingColliders[i].gameObject.TryGetComponent(out VehicleCatchZone zone))
                    {
                        catchZone = zone;
                        break;
                    }
                }
            }

            return catchZone != null;
        }
    }
}
