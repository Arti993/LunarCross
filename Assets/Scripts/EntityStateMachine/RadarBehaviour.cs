using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RadarBehaviour : EntityBehaviour
{
    private float _rotationAngle = 120;
    private float _rotationHalfCycleTime = 2;
    
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        AllStates = new List<EntityBaseState>()
        {
            new SimpleRotationState(this, this, _rigidbody, _rotationAngle, _rotationHalfCycleTime),
            new ObstacleKnockedState(this, _rigidbody)
        };

        CurrentState = AllStates.First();
        CurrentState.Start();
    }
}
