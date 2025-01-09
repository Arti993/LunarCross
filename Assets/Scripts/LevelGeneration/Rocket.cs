using System;
using System.Collections;
using System.Collections.Generic;
using Ami.BroAudio;
using Infrastructure.Services.AudioPlayback;
using Infrastructure.Services.Factories.ParticleSystemFactory;
using Reflex.Attributes;
using UnityEngine;

namespace LevelGeneration
{
    [RequireComponent(typeof(Rigidbody))]
    public class Rocket : MonoBehaviour
    {
        private const float Speed = 5.3f;
        private const float Delay = 0.5f;

        [SerializeField] private GameObject _ladder;
        [SerializeField] private GameObject _smokeBeforeTakeOff;
        [SerializeField] private Transform _bottomLadderPoint;
        [SerializeField] private Transform _middleLadderPoint;
        [SerializeField] private Transform _topLadderPoint;
        [SerializeField] private List<RunnerToRocket> _runnersToRocket;

        private Rigidbody _rigidbody;
        private IAudioPlayback _audioPlayback;
        private IParticleSystemFactory _particleSystemFactory;

        public Transform BottomLadderPoint => _bottomLadderPoint;
        public Transform TopLadderPoint => _topLadderPoint;
        
        [Inject]
        private void Construct(IAudioPlayback audioPlayback,
            IParticleSystemFactory particleSystemFactory)
        {
            _audioPlayback = audioPlayback;
            _particleSystemFactory = particleSystemFactory;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            SoundID rocketEngine = _audioPlayback.SoundsContainer.RocketEngine;

            _audioPlayback.PlaySound(rocketEngine);
        }

        public void PlaceRunner(RunnerToRocket runner)
        {
            if (_runnersToRocket.Remove(runner) == false)
                throw new InvalidOperationException();

            Destroy(runner.gameObject);

            if (_runnersToRocket.Count == 0)
                _ = StartCoroutine(FlyAway());
        }

        private IEnumerator FlyAway()
        {
            yield return new WaitForSeconds(Delay);

            _particleSystemFactory.ShowGreenCollectEffect(_middleLadderPoint.position);

            Destroy(_ladder);

            yield return new WaitForSeconds(Delay);

            Destroy(_smokeBeforeTakeOff);

            _rigidbody.AddForce(0f, Speed, 0f, ForceMode.VelocityChange);

            SoundID rocketTurbine = _audioPlayback.SoundsContainer.RocketTurbine;

            _audioPlayback.PlaySound(rocketTurbine);
        }
    }
}
