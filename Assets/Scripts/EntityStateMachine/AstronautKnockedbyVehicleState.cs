using UnityEngine;

public class AstronautKnockedbyVehicleState : HumanoidKnockedState
{
    public AstronautKnockedbyVehicleState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Ragdoll ragdollBody) : base(stateSwitcher, rigidbody, ragdollBody)
    {
    }
}
