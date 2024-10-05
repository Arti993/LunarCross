using System.Collections.Generic;
using UnityEngine;

public class EntityCollection
{
    private List<Entity> _entityList = new List<Entity>();

    public void Add(Entity entity)
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