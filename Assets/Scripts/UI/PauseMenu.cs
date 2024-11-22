using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Infrastructure;
using Infrastructure.Services.ScreenFader;
using Infrastructure.UIStateMachine;
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

            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStatePauseButton>();
        }

        public void RestartLevel()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(sceneIndex);
        }

        public void ChangeGameSettings()
        {
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateSettings>();
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