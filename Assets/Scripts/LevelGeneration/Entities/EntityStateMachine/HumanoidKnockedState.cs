using Ami.BroAudio;
using Ami.Extension;
using Infrastructure;
using Infrastructure.Services.AudioPlayback;
using Reflex.Attributes;
using UnityEngine;

namespace LevelGeneration.Entities.EntityStateMachine
{
    public class HumanoidKnockedState : KnockedState
    {
        private readonly Collider _collider;
        private readonly Ragdoll _ragdollBody;
        private Rigidbody _spineRigidbody;
        private IAudioPlayback _audioPlayback;

        public HumanoidKnockedState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Ragdoll ragdollBody,
            Collider collider) : base(stateSwitcher, rigidbody)
        {
            _ragdollBody = ragdollBody;
            _collider = collider;
        }
        
        [Inject]
        private void Construct(IAudioPlayback audioPlayback)
        {
            _audioPlayback = audioPlayback;
        }

        public override void Start()
        {
            if (Rigidbody.gameObject.TryGetComponent(out EntityToEjectDetector ejector))
                ejector.Disable();

            base.Start();
        }

        public override void Stop()
        {
            _ragdollBody.TurnOff();

            base.Stop();
        }
        
        protected override void Move()
        {
            _ragdollBody.TurnOn();

            _spineRigidbody = _ragdollBody.GetSpineRigidbody();

            _spineRigidbody.velocity = MovementDirection * MovementSpeed;
            _spineRigidbody.angularVelocity =
                new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1f), Random.Range(-1f, 1f)).normalized;

            SoundID knock = _audioPlayback.SoundsContainer.Knock;

            _audioPlayback.PlaySound(knock);
        }
    }
}
