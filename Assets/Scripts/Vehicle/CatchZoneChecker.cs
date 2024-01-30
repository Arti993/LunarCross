using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchZoneChecker
{
    private EntityBehaviour _entity;
    float _searchRadius;

    public CatchZoneChecker(EntityBehaviour entity, float searchRadius)
    {
        _entity = entity;
        _searchRadius = searchRadius;
    }

    public bool CheckEnteringCatchZone(out VehicleCatchZone catchZone)
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
