using System.Collections;
using UnityEngine;
using Vehicle.BindPoints;

namespace LevelGeneration.Entities.EntityStateMachine.Astronaut
{
    public class OutsideVehicleAttachState : EntityBaseState
    {
        private const string LevitatingTrigger = "isLevitating";
        private const float ChangeAttachStateTime = 12;

        private readonly Rigidbody _rigidbody;
        private readonly Animator _animator;
        private readonly IPlaceableToVehicle _placementPattern;
        private EntityBehaviour _entityBehaviour;
        private Coroutine _changeAttachStateCoroutine;
        
        public OutsideVehicleAttachState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Animator animator,
            IPlaceableToVehicle placementPattern) : base(stateSwitcher)
        {
            _rigidbody = rigidbody;
            _animator = animator;
            _placementPattern = placementPattern;
        }

        public override void Start()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.TryGetComponent(out EntityBehaviour behaviour);
            _entityBehaviour = behaviour;

            _animator.SetBool(LevitatingTrigger, true);

            if (_entityBehaviour != null)
                _changeAttachStateCoroutine = _entityBehaviour.StartCoroutine(WaitToInsideAttach());

        }

        public override void Stop()
        {
            if (_changeAttachStateCoroutine != null)
            {
                _entityBehaviour.StopCoroutine(_changeAttachStateCoroutine);

                _changeAttachStateCoroutine = null;
            }
        }

        private IEnumerator WaitToInsideAttach()
        {
            yield return new WaitForSeconds(ChangeAttachStateTime);

            if (_entityBehaviour.GetComponentInParent<BindPoint>().IsFree == false)
            {
                if (_placementPattern.TryPlaceToVehicle())
                {
                    _changeAttachStateCoroutine = null;
                }
                else
                {
                    _changeAttachStateCoroutine = _entityBehaviour.StartCoroutine(WaitToInsideAttach());
                }
            }
        }
    }
}
