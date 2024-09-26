using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautGroundHitState : HumanoidKnockedState
{
    public AstronautGroundHitState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Ragdoll ragdollBody, Collider collider) : base(stateSwitcher, rigidbody, ragdollBody, collider)
    {
        MovementSpeed = 10;
    }
}
