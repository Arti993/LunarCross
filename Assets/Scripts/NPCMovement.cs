using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float _roamRadius = 10f;
    [SerializeField] private float _changeDestinationInterval = 5f;

    private NavMeshAgent _agent;
    private Vector3 _randomDestination;
    private float _timer;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
    }

    private void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
            {
                _timer = 0f; 
                SetRandomDestination();
            }
        }
        else
        {
            _timer += Time.deltaTime; 
            if (_timer >= _changeDestinationInterval)
            {
                _timer = 0f;
                SetRandomDestination(); 
            }
        }
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _roamRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, _roamRadius, 1);
        _randomDestination = hit.position;
        _agent.SetDestination(_randomDestination);
    }

    public void Disable()
    {
        _agent.enabled = false;
        enabled = false;
    }
}
