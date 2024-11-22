using UnityEngine;

namespace LevelGeneration.Entities.EntityStateMachine.Alien
{
    public class AlienAfterEjectingState : AfterEjectingState
    {
        private const float Speed = 0.5f;

        private IEntityStateSwitcher _stateSwitcher;
        private Rigidbody _rigidbody;
        private Transform _entityTransform;

        public AlienAfterEjectingState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody) : base(stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
            _rigidbody = rigidbody;
            _entityTransform = rigidbody.transform;
        }

        public override void Start()
        {
            Move();
        }

        public override void Move()
        {
            _entityTransform.rotation = Quaternion.LookRotation(-_entityTransform.forward);
            _rigidbody.velocity = _entityTransform.forward * Speed;
        }

        public override void ReactOnEntryVehicleCatchZone()
        {
            NoReact();
        }

        public override void ReactOnEntryVehicleTossZone()
        {
            _stateSwitcher.SwitchState<KnockedState>();
        }

        public override void Stop()
        {
            NoReact();
        }
    }
}
