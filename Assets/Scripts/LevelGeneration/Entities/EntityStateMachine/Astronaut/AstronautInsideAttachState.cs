using UnityEngine;

namespace LevelGeneration.Entities.EntityStateMachine.Astronaut
{
    public class AstronautInsideAttachState : InsideVehicleAttachState
    {
        private const string LevitatingTrigger = "isLevitating";
        private const string IdleTrigger = "isIdle";

        private readonly Animator _animator;

        public AstronautInsideAttachState(IEntityStateSwitcher stateSwitcher, Animator animator) : base(stateSwitcher)
        {
            _animator = animator;
        }

        public override void Start()
        {
            _animator.SetBool(LevitatingTrigger, false);

            Move();
        }

        public override void Move()
        {
            _animator.SetBool(IdleTrigger, true);
        }

        public override void ReactOnEntryVehicleCatchZone()
        {
            NoReact();
        }

        public override void ReactOnEntryVehicleTossZone()
        {
            NoReact();
        }

        public override void Stop()
        {

        }
    }
}
