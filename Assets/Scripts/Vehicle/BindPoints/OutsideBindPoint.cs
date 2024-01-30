using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideBindPoint : BindPoint
{
    protected float AngleShift = 15;

    public override void Fill(EntityBehaviour entity)
    {
        base.Fill(entity);

        BindedEntity.transform.position = Transform.position;
        BindedEntity.transform.rotation = Transform.rotation;

        Quaternion quaternion = Quaternion.AngleAxis(90, Transform.right) * Transform.rotation;

        quaternion = Quaternion.AngleAxis(AngleShift, Transform.up) * quaternion;

        BindedEntity.transform.rotation = quaternion * BindedEntity.transform.rotation;

        BindedEntity.transform.SetParent(Transform);
    }
}
