using System;
using System.Collections;
using UnityEngine;

namespace LevelGeneration.Entities.EntityStateMachine
{
    public class EjectedFromVehicleState : EntityBaseState
    {
        private const string LevitatingTrigger = "isLevitating";
        private const string IdleTrigger = "isIdle";
        private const float TimeToEnableCollider = 1;

        protected readonly Rigidbody Rigidbody;
        protected readonly IPlaceableToVehicle PlacementPattern;
        private Animator Animator;
        private Collider Collider;
        private EntityBehaviour _entity;

        protected EjectedFromVehicleState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Animator animator,
            Collider collider, IPlaceableToVehicle placementPattern) : base(stateSwitcher)
        {
            Rigidbody = rigidbody;
            Animator = animator;
            Collider = collider;
            PlacementPattern = placementPattern;
        }

        public override void Start()
        {
            _ = Rigidbody.TryGetComponent(out EntityBehaviour entity);

            _entity = entity;

            _entity.transform.SetParent(null);

            if (_entity == null)
                throw new InvalidOperationException();

            Collider.enabled = false;

            _ = _entity.StartCoroutine(WaitToEnableCollider());

            if (Animator.GetBool(IdleTrigger))
                Animator.SetBool(IdleTrigger, false);

            if (Animator.GetBool(LevitatingTrigger) == false)
                Animator.SetBool(LevitatingTrigger, true);
        }

        public override void Stop()
        {
            Rigidbody.velocity = Vector3.zero;
        }

        private IEnumerator WaitToEnableCollider()
        {
            yield return new WaitForSeconds(TimeToEnableCollider);

            Collider.enabled = true;
        }
    }
}