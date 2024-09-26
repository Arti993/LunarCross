using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public event Action<Entity> Disabled;

    public virtual void Disable()
    {
        Disabled?.Invoke(this);
    }
}
