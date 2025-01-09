using Data;
using Infrastructure.Services.Factories.ParticleSystemFactory;
using Infrastructure.Services.ScreenFader;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using Reflex.Attributes;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Vehicle
{
    [RequireComponent(typeof(Rigidbody))]
    public class VehicleSpeedLimit : MonoBehaviour
    {
        private const float StartSpeed = 2;
        private const float MaxSpeed = 4f;
        private const float TrackingPeriod = 0.08f;
        private const float MinRequiredDistancePerTrackingPeriod = 0.01f;

        [SerializeField] private Transform _blowUpPoint;

        private Rigidbody _rigidbody;
        private Transform _transform;
        private float _timer;
        private float _distanceTraveled;
        private float _periodStartPositionZ;
        private bool _isMinSpeedViolation;
        private IUiStateMachine _uiStateMachine;
        private IParticleSystemFactory _particleSystemFactory;
        private IScreenFader _screenFader;
        
        [Inject]
        private void Construct(IUiStateMachine uiStateMachine, IParticleSystemFactory particleSystemFactory,
            IScreenFader screenFader)
        {
            _uiStateMachine = uiStateMachine;
            _particleSystemFactory = particleSystemFactory;
            _screenFader = screenFader;
        }

        private void Start()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = new Vector3(0, 0, StartSpeed);
            _periodStartPositionZ = _transform.position.z;
        }

        private void FixedUpdate()
        {
            LimitSpeed();

            TrackMinimalMovement();
        }

        private void LimitSpeed()
        {
            if (_rigidbody.velocity.magnitude > MaxSpeed)
                _rigidbody.velocity = _rigidbody.velocity.normalized * MaxSpeed;
        }

        private void TrackMinimalMovement()
        {
            _timer += Time.deltaTime;
            _distanceTraveled = _transform.position.z - _periodStartPositionZ;

            if (_timer > TrackingPeriod)
            {
                if (_distanceTraveled < MinRequiredDistancePerTrackingPeriod)
                {
                    HandleSpeedViolation();
                }

                _timer = 0;
                _periodStartPositionZ = _transform.position.z;
            }
        }

        private void HandleSpeedViolation()
        {
            if (_isMinSpeedViolation == false)
            {
                _particleSystemFactory.ShowExplosionEffect(_blowUpPoint.position);

                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

                if (currentSceneIndex == (int) SceneIndex.Tutorial)
                {
                    TimePauserWithDelay timePauserWithDelay = new TimePauserWithDelay();

                    _screenFader.FadeOutAndLoadScene((int) SceneIndex.Tutorial);

                    _ = StartCoroutine(timePauserWithDelay.Pause());
                }
                else
                {
                    _uiStateMachine.SetState<UiStateLevelFailed>();
                }

                _isMinSpeedViolation = true;
            }
        }
    }
}
