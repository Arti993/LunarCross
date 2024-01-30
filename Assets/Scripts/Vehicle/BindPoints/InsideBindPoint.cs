using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideBindPoint : BindPoint
{
    public override void Fill(EntityBehaviour entity)
    {
        base.Fill(entity);

        BindedEntity.transform.position = Transform.position;
        BindedEntity.transform.rotation = Transform.rotation;
        BindedEntity.transform.SetParent(Transform);
    }
}
