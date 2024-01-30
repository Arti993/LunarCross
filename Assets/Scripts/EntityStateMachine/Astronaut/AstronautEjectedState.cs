using System;
using System.Collections;
using UnityEngine;

public class AstronautEjectedState : EjectedFromVehicleState
{
    private const string LevitatingTrigger = "isLevitating";
    private const string IdleTrigger = "isIdle";

    private Rigidbody _rigidbody;
    private Animator _animator;
    private Collider _collider;  //включается через секунду после старта
    private IEntityStateSwitcher _stateSwitcher;
    private EntityBehaviour _entity;

    public AstronautEjectedState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Animator animator,
        Collider collider) : base(stateSwitcher)
    {
        _rigidbody = rigidbody;
        _animator = animator;
        _collider = collider;
        _stateSwitcher = stateSwitcher;
    }

    public override void Start()
    {
        _rigidbody.TryGetComponent(out EntityBehaviour entity);

        _entity = entity;

        _entity.transform.SetParent(null);

        if (_entity == null)
            throw new InvalidOperationException();

        _collider.enabled = false;

        _entity.StartCoroutine(WaitToEnableCollider());

        Move();
    }

    public override void Move()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _rigidbody.drag = 0.7f;
 

        Vector3 movementDirection = new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), 1, 0.7f);  // подправить

        _rigidbody.AddForce(movementDirection * 10f, ForceMode.Impulse);

        if (_animator.GetBool(IdleTrigger))
            _animator.SetBool(IdleTrigger, false);

        if (_animator.GetBool(LevitatingTrigger) == false)
            _animator.SetBool(LevitatingTrigger, true);
    }

    public override void ReactOnEntryVehicleCatchZone()
    {
        _stateSwitcher.SwitchState<OutsideVehicleAttachState>();
    }

    public override void ReactOnEntryVehicleTossZone()
    {
        _stateSwitcher.SwitchState<KnockedByVehicleState>();
    }


    public override void Stop()
    {
    }

    private IEnumerator WaitToEnableCollider()
    {
        yield return new WaitForSeconds(1);  // убрать магическое число

        _collider.enabled = true;
    }
}
