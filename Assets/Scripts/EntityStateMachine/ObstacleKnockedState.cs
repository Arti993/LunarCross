using UnityEngine;

public class ObstacleKnockedState : KnockedByVehicleState
{
    private const float MovementSpeedValue = 11;
    
    public ObstacleKnockedState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody) : base(stateSwitcher, rigidbody)
    {
        MovementSpeed = MovementSpeedValue;
    }
}
