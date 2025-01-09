using System;
using UnityEngine;

namespace LevelGeneration
{
    public class Entity : MonoBehaviour
    {
        public event Action<Entity> Disabled;

        public void Disable()
        {
            Disabled?.Invoke(this);
        }
    }
}
