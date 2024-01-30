using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class LevitationState : EntityBaseState
{
    private Rigidbody _rigidbody;
    private NavMeshAgent _navMeshAgent; 
    private IEntityStateSwitcher _stateSwitcher;
    private Vector3 _startPosition;
    private Tween _levitationAnimation;

    private float _levitationHeight = 0.12f;
    private float _levitationSpeed = 2;

    public LevitationState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, NavMeshAgent navMeshAgent) : base(stateSwitcher)
    {
        _rigidbody = rigidbody;
        _navMeshAgent = navMeshAgent;
        _stateSwitcher = stateSwitcher;
    }

    public override void Start()
    {
        _rigidbody.isKinematic = true;
        _startPosition = _rigidbody.transform.position;

        Move();
    }

    public override void Move()
    {
        _levitationAnimation = _rigidbody.transform.DOMoveY(_startPosition.y + _levitationHeight, _levitationSpeed).SetLoops(-1, LoopType.Yoyo);
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
        _levitationAnimation.Kill();

        _navMeshAgent.enabled = false;
    }
}
