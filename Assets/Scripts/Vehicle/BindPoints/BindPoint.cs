using System;
using LevelGeneration.Entities.EntityStateMachine;
using UnityEngine;

namespace Vehicle.BindPoints
{
    public abstract class BindPoint : MonoBehaviour
    {
        protected Transform Transform;

        public EntityBehaviour BindedEntity { get; private set; }
        public bool IsFree { get; private set; }

        private void Awake()
        {
            Transform = transform;
            IsFree = true;
        }

        public virtual void Fill(EntityBehaviour entity)
        {
            if (IsFree == false)
                throw new InvalidOperationException();

            BindedEntity = entity;
            IsFree = false;
        }

        public void Exempt()
        {
            IsFree = true;
        }
    }
}
