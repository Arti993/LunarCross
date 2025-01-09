using UnityEngine;

namespace LevelGeneration.Entities.EntityStateMachine.Astronaut
{
    public class InsideVehicleAttachState : EntityBaseState
    {
        private const string LevitatingTrigger = "isLevitating";
        private const string IdleTrigger = "isIdle";

        private readonly Animator _animator;

        public InsideVehicleAttachState(IEntityStateSwitcher stateSwitcher, Animator animator) : base(stateSwitcher)
        {
            _animator = animator;
        }

        public override void Start()
        {
            _animator.SetBool(LevitatingTrigger, false);

            _animator.SetBool(IdleTrigger, true);
        }

        public override void Stop()
        {
            _animator.SetBool(IdleTrigger, false);
        }
    }
}
