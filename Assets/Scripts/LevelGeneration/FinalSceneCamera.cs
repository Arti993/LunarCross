using Ami.BroAudio;
using Infrastructure.Services.AudioPlayback;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using Reflex.Attributes;
using UnityEngine;

namespace LevelGeneration
{
    public class FinalSceneCamera : MonoBehaviour
    {
        private const float MaxCameraOffset = 28;
        private const float MaxRocketOffset = 55;

        [SerializeField] private Transform _rocket;
        [SerializeField] private GameObject _designObjectsContainer;

        private Transform _transform;
        private float _startRocketPositionY;
        private float _startCameraPositionY;
        private IAudioPlayback _audioPlayback;
        private IUiStateMachine _uiStateMachine;

        [Inject]
        private void Construct(IAudioPlayback audioPlayback, IUiStateMachine uiStateMachine)
        {
            _audioPlayback = audioPlayback;
            _uiStateMachine = uiStateMachine;
        }

        private void Start()
        {
            _transform = transform;
            _startRocketPositionY = _rocket.position.y;
            _startCameraPositionY = _transform.position.y;

            SoundID finalMusicTheme = _audioPlayback.MusicContainer.FinalTheme;

            _audioPlayback.PlayMusic(finalMusicTheme);
        }

        private void FixedUpdate()
        {
            float rocketOffset = _rocket.position.y - _startRocketPositionY;

            if (rocketOffset < MaxCameraOffset)
            {
                Vector3 newCameraPosition = _transform.position;
                newCameraPosition.y = _startCameraPositionY + rocketOffset;
                _transform.position = newCameraPosition;
            }
            else if (rocketOffset > MaxRocketOffset)
            {
                StopRocketSounds();

                Destroy(_rocket.gameObject);

                Destroy(_designObjectsContainer);

                ShowCompleteGameWindow();

                Destroy(this);
            }
        }

        private void ShowCompleteGameWindow()
        {
            _uiStateMachine.SetState<UiStateGameComplete>();
        }

        private void StopRocketSounds()
        {
            SoundID rocketEngine = _audioPlayback.SoundsContainer.RocketEngine;

            _audioPlayback.SoundsContainer.Stop(rocketEngine);

            SoundID rocketTurbine = _audioPlayback.SoundsContainer.RocketTurbine;

            _audioPlayback.SoundsContainer.Stop(rocketTurbine);
        }
    }
}
