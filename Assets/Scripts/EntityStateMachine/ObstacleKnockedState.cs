using UnityEngine;

public class ObstacleKnockedState : KnockedState
{
    private const float MovementSpeedValue = 11;
    
    public ObstacleKnockedState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody) : base(stateSwitcher, rigidbody)
    {
        MovementSpeed = MovementSpeedValue;
    }
}
