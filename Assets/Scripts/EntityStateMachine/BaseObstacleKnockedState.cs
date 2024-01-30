using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacleKnockedState : KnockedByVehicleState
{
    public BaseObstacleKnockedState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody) : base(stateSwitcher, rigidbody)
    {
        MovementDirection = new Vector3(Random.Range(-0.25f, 0.25f), 1, 0f);
        MovementSpeed = 11;
        TimeToDestroy = 4;
    }
}
