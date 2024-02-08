using System;
using UnityEngine;

public abstract class BindPoint : MonoBehaviour
{
    protected Transform Transform;
    
    public EntityBehaviour BindedEntity { get; protected set; }
    public bool IsFree { get; protected set; }

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
        if (IsFree)
            throw new InvalidOperationException();

        IsFree = true;
    }
}
