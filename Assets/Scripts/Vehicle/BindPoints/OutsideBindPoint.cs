using UnityEngine;

public class OutsideBindPoint : BindPoint
{
    private const int RightAngleGradus = 90;
    
    protected float AngleShift = 15;

    public override void Fill(EntityBehaviour entity)
    {
        base.Fill(entity);

        var EntityTransform = BindedEntity.transform;
        EntityTransform.position = Transform.position;
        EntityTransform.rotation = Transform.rotation;

        Quaternion quaternion = Quaternion.AngleAxis(RightAngleGradus, Transform.right) * Transform.rotation;

        quaternion = Quaternion.AngleAxis(AngleShift, Transform.up) * quaternion;

        EntityTransform.rotation = quaternion * EntityTransform.rotation;

        EntityTransform.SetParent(Transform);
    }
}
