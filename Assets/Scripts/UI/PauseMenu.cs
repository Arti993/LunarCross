using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Infrastructure;
using Infrastructure.Services.ScreenFader;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using Reflex.Attributes;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : MenuEscapeWindow
    {
        [SerializeField] private Image _backgroundPanel;
        private IUiStateMachine _uiStateMachine;
        private IScreenFader _screenFader;

        [Inject]
        private void Construct(IUiStateMachine uiStateMachine, IScreenFader screenFader)
        {
            _uiStateMachine = uiStateMachine;
            _screenFader = screenFader;
        }
        
        public void PauseGame()
        {
            PanelIntro();

            Time.timeScale = 0f;

            BackGroundPanelIntro();
        }

        public void ResumeGame()
        {
            BackGroundPanelOutro();

            Time.timeScale = 1f;

            _uiStateMachine.SetState<UiStatePauseButton>();
        }

        public void RestartLevel()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            _screenFader.FadeOutAndLoadScene(sceneIndex);
        }

        public void ChangeGameSettings()
        {
            _uiStateMachine.SetState<UiStateSettings>();
        }

        private void BackGroundPanelIntro()
        {
            _ = _backgroundPanel.DOFade(0.5f, PanelAnimationDuration).SetUpdate(true);
        }

        private void BackGroundPanelOutro()
        {
            _ = _backgroundPanel.DOFade(0f, PanelAnimationDuration).SetUpdate(true);
        }
    }
}