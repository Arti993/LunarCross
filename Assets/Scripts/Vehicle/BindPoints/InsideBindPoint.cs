using UnityEngine;

public class InsideBindPoint : BindPoint
{
    public override void Fill(EntityBehaviour entity)
    {
        base.Fill(entity);

        Transform bindedEntityTransform = BindedEntity.transform;
        bindedEntityTransform.position = Transform.position;
        bindedEntityTransform.rotation = Transform.rotation;
        
        BindedEntity.transform.SetParent(Transform);
    }
}
