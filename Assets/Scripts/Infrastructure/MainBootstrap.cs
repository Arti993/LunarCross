using YG;
using Ami.BroAudio;
using Data;
using Infrastructure.Services.AudioPlayback;
using Infrastructure.Services.Factories.UiFactory;
using Infrastructure.Services.FocusTest;
using Infrastructure.Services.ScreenFader;
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
        private IScreenFader _screenFader;
        private IFocusTestStateChanger _focusTestStateChanger;
        private static bool _isFirstAwake = true;
        
        [Inject]
        private void Construct(IUiStateMachine uiStateMachine, 
            IUiWindowFactory uiWindowFactory, IAudioPlayback audioPlayback, IScreenFader screenFader, IFocusTestStateChanger focusTestStateChanger)
        {
            _uiStateMachine = uiStateMachine;
            _uiWindowFactory = uiWindowFactory;
            _audioPlayback = audioPlayback;
            _screenFader = screenFader;
            _focusTestStateChanger = focusTestStateChanger;
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
                
                _screenFader.FadeIn();
                
                _focusTestStateChanger.EnableFocusTest();

                _isFirstAwake = false;
            }

            OpenMainMenu();
        }

        private void PrepareUiStateMachine()
        {
            AddState(new UiStateMainMenu(_uiWindowFactory));
            AddState(new UiStateLeaderboard(_uiWindowFactory));
            AddState(new UiStateSettings(_uiWindowFactory));
            AddState(new UiStateGameComplete(_uiWindowFactory));
            AddState(new UiStatePauseMenu(_uiWindowFactory));
            AddState(new UiStatePauseButton(_uiWindowFactory));
            AddState(new UIStateLevelComplete(_uiWindowFactory));
            AddState(new UiStateLevelFailed(_uiWindowFactory));
            AddState(new UiStateRestartGameQuestion(_uiWindowFactory));
            AddState(new UiStateAuthorizationQuestion(_uiWindowFactory));
            AddState(new UiStateTutorialAliens(_uiWindowFactory));
            AddState(new UiStateTutorialAstronauts(_uiWindowFactory));
            AddState(new UiStateTutorialTornado(_uiWindowFactory));
            AddState(new UiStateTutorialObstacles(_uiWindowFactory));
            AddState(new UiStateTutorialFinish(_uiWindowFactory));
            AddState(new UiStateTutorialTouchscreenControl(_uiWindowFactory));
            AddState(new UiStateTutorialKeyboardControl(_uiWindowFactory));
            AddState(new UiStateNoWindow(_uiWindowFactory));
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