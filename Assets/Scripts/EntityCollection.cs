using System.Collections.Generic;
using UnityEngine;

public class EntityCollection
{
    private List<EntityBehaviour> _entityList = new List<EntityBehaviour>();

    public void Add(EntityBehaviour entity)
    {
        _entityList.Add(entity);
    }

    public void Clear()
    {
        foreach (var entity in _entityList)
        {
            if (entity != null)
                Object.Destroy(entity.gameObject);
        }

        _entityList.Clear();
    }
}