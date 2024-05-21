using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautGroundHitState : HumanoidKnockedState
{
    public AstronautGroundHitState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Ragdoll ragdollBody) : base(stateSwitcher, rigidbody, ragdollBody)
    {
        MovementSpeed = 10;
    }
}
