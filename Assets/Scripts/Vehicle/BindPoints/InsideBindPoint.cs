using LevelGeneration.Entities.EntityStateMachine;
using UnityEngine;

namespace Vehicle.BindPoints
{
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
}
