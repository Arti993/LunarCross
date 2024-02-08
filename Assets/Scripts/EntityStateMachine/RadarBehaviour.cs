using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class RadarBehaviour : EntityBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        AllStates = new List<EntityBaseState>()
        {
            new SimpleRotationState(this, _rigidbody),
            new BaseObstacleKnockedState(this, _rigidbody)
        };

        CurrentState = AllStates.First();
        CurrentState.Start();
    }
}
