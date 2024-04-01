using UnityEngine;

public class AstronautKnockedState : HumanoidKnockedState
{
    public AstronautKnockedState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Ragdoll ragdollBody) : base(stateSwitcher, rigidbody, ragdollBody)
    {
    }
}
