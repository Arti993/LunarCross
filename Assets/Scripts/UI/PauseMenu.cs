using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Infrastructure.UIStateMachine.States;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : MenuEscapeWindow
    {
        [SerializeField] private Image _backgroundPanel;

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

            UiStateMachine.SetState<UiStatePauseButton>();
        }

        public void RestartLevel()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            ScreenFader.FadeOutAndLoadScene(sceneIndex);
        }

        public void ChangeGameSettings()
        {
            UiStateMachine.SetState<UiStateSettings>();
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