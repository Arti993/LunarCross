using UnityEngine;
using Agava.WebUtility;
using Data;
using Infrastructure.Services.AudioPlayback;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using Reflex.Extensions;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.FocusTest
{
    public class FocusTest : MonoBehaviour
    {
        private bool _isUnfocusePaused;
        private bool _isFocused;
        private IAudioPlayback _audioPlayback;
        private IFocusTestStateChanger _focusTestStateChanger;
        private IUiStateMachine _uiStateMachine;

        private void Construct()
        {
            _audioPlayback = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IAudioPlayback>();
            _focusTestStateChanger = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IFocusTestStateChanger>();
            _uiStateMachine = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IUiStateMachine>();
        }
        
        private void Awake()
        {
            Construct();
            
            DontDestroyOnLoad(gameObject);
            
            _isFocused = true;
        }

        
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            Application.focusChanged += OnInBackGroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackGroundChangeWeb;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Application.focusChanged -= OnInBackGroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackGroundChangeWeb;
        }

        public bool IsFocused()
        {
            return _isFocused;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentSceneIndex == (int) SceneIndex.Gameplay || currentSceneIndex == (int) SceneIndex.Tutorial)
                _focusTestStateChanger.DisablePauseMenuOpening();

            if (_isFocused == false)
            {
                MuteAudio(true);
                PauseGame(true);
            }
        }

        private void OnInBackGroundChangeApp(bool inApp)
        {
            _isFocused = inApp;
            MuteAudio(inApp == false);
            PauseGame(inApp == false);
        }

        private void OnInBackGroundChangeWeb(bool isBackground)
        {
            _isFocused = !isBackground;
            MuteAudio(isBackground);
            PauseGame(isBackground);
        }

        private void MuteAudio(bool value)
        {
            if (value)
            {
                _audioPlayback.MuteAudio();
            }
            else
            {
                _audioPlayback.UnMuteAudio();
            }
        }

        private void PauseGame(bool value)
        {
            if (value && Time.timeScale != 0)
            {
                if (_focusTestStateChanger.IsNeedToOpenPauseMenu)
                {
                    _uiStateMachine.SetState<UiStatePauseMenu>();

                    return;
                }

                _isUnfocusePaused = true;
                Time.timeScale = 0f;
            }

            if (_isUnfocusePaused && value == false)
            {
                Time.timeScale = 1f;
                _isUnfocusePaused = false;
            }
        }
    }
}