using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleRotationState : EntityBaseState
{
    private Rigidbody _rigidbody;
    private NavMeshAgent _navMeshAgent;
    private IEntityStateSwitcher _stateSwitcher;
    private Tween _rotationAnimation;

    private float _rotationAngle = 120;
    private float _rotationSpeed = 1;

    public SimpleRotationState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, NavMeshAgent navMeshAgent) : base(stateSwitcher)
    {
        _rigidbody = rigidbody;
        _navMeshAgent = navMeshAgent;
        _stateSwitcher = stateSwitcher;
    }

    public override void Start()
    {
        _rigidbody.isKinematic = true;

        Move();
    }

    public override void Move()
    {
        _rotationAnimation = _rigidbody.transform.DORotate(new Vector3(0f, _rotationAngle, 0f), _rotationSpeed).SetLoops(-1, LoopType.Yoyo);
    }

    public override void ReactOnEntryVehicleCatchZone()
    {
        _stateSwitcher.SwitchState<KnockedByVehicleState>();
    }

    public override void ReactOnEntryVehicleTossZone()
    {
        _stateSwitcher.SwitchState<KnockedByVehicleState>();
    }

    public override void Stop()
    {
        _rotationAnimation.Kill();

        _navMeshAgent.enabled = false;
    }
}
