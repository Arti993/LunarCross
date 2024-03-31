using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SatelliteBehaviour : EntityBehaviour
{
    [SerializeField] private float _levitationHeight = 1f;
    [SerializeField] private float _levitationHalfCycleTime = 2f;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        AllStates = new List<EntityBaseState>()
        {
            new LevitationState(this, this, _rigidbody, _levitationHeight, _levitationHalfCycleTime),
            new ObstacleKnockedState(this, _rigidbody)
        };

        CurrentState = AllStates.First();
        CurrentState.Start();
    }
}
