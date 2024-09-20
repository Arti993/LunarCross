using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PauseMenu : MenuEscapeWindow
{
    [SerializeField] private CanvasGroup _backgroundPanel;

    public void PauseGame()
    {
        BackGroundPanelIntro();
        
        PanelIntro();

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        BackGroundPanelOutro();
        
        DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStatePauseButton>();
    }

    public void RestartLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateNoWindow>();

        DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(sceneIndex);
    }

    public void ChangeGameSettings()
    {
        DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateSettings>();
    }

    private void BackGroundPanelIntro()
    {
        _backgroundPanel.DOFade(1, PanelAnimationDuration).SetUpdate(true);
    }

    private void BackGroundPanelOutro()
    {
        _backgroundPanel.DOFade(0, PanelAnimationDuration).SetUpdate(true);
    }
}