using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class RadarBehaviour : EntityBehaviour
{
    private Rigidbody _rigidbody;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        AllStates = new List<EntityBaseState>()
        {
            new SimpleRotationState(this, _rigidbody, _navMeshAgent),
            new BaseObstacleKnockedState(this, _rigidbody)
        };

        CurrentState = AllStates.First();
        CurrentState.Start();
    }
}
