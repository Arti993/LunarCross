using YG;
using Ami.BroAudio;
using Data;
using Infrastructure.Services.AudioPlayback;
using Infrastructure.Services.Factories.UiFactory;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using Infrastructure.UIStateMachine.States.TutorialStates;
using Reflex.Attributes;
using UI;
using UnityEngine;

namespace Infrastructure
{
    public class MainBootstrap : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        private IUiStateMachine _uiStateMachine;
        private IUiWindowFactory _uiWindowFactory;
        private IAudioPlayback _audioPlayback;
        private static bool _isFirstAwake = true;
        
        [Inject]
        private void Construct(IUiStateMachine uiStateMachine, 
            IUiWindowFactory uiWindowFactory, IAudioPlayback audioPlayback)
        {
            _uiStateMachine = uiStateMachine;
            _uiWindowFactory = uiWindowFactory;
            _audioPlayback = audioPlayback;
        }

        private void Awake()
        {
            if (_isFirstAwake)
            {
#if UNITY_WEBGL && !UNITY_EDITOR
    YandexGame.GameReadyAPI();
    YandexGame.SetFullscreen(true);
#endif
                
                PrepareUiStateMachine();

                _isFirstAwake = false;
            }

            OpenMainMenu();
        }

        private void PrepareUiStateMachine()
        {
            AddState(new UiStateMainMenu());
            AddState(new UiStateLeaderboard());
            AddState(new UiStateSettings());
            AddState(new UiStateGameComplete());
            AddState(new UiStatePauseMenu());
            AddState(new UiStatePauseButton());
            AddState(new UIStateLevelComplete());
            AddState(new UiStateLevelFailed());
            AddState(new UiStateRestartGameQuestion());
            AddState(new UiStateAuthorizationQuestion());
            AddState(new UiStateTutorialAliens());
            AddState(new UiStateTutorialAstronauts());
            AddState(new UiStateTutorialTornado());
            AddState(new UiStateTutorialObstacles());
            AddState(new UiStateTutorialFinish());
            AddState(new UiStateTutorialTouchscreenControl());
            AddState(new UiStateTutorialKeyboardControl());
            AddState(new UiStateNoWindow());
        }

        private void AddState(UiStateMachineState state)
        {
            _uiStateMachine.AddState(state);
        }

        private void OpenMainMenu()
        {
            GameObject uiRootObject = _uiWindowFactory.GetUIRoot();

            uiRootObject.GetComponent<UIRoot>().SetCamera(_camera);

            _uiWindowFactory.ShowUIObject(PrefabsPaths.GameMainTitle, uiRootObject);

            _uiStateMachine.SetState<UiStateMainMenu>();

            StartMenuMusicTheme();
        }

        private void StartMenuMusicTheme()
        {
            SoundID menuMusicTheme = _audioPlayback.MusicContainer.MenuTheme;

            _audioPlayback.PlayMusic(menuMusicTheme);
        }
    }
}