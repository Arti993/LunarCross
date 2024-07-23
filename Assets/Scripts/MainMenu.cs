using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private const int GameplaySceneIndex = 2;
    private const int LevelChooseSceneIndex = 3;
    private const int TutorialSceneIndex = 4;

    private Canvas _uiRoot;

    private void Awake()
    {
        _uiRoot = GetComponentInParent<Canvas>();
    }

    public void OnPlayButtonClick()
    {
        AllServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(GameplaySceneIndex);
    }

    public void OnLevelsChooseButtonCLick()
    {
        AllServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(LevelChooseSceneIndex);
    }

    public void OnRestartGameButtonClick()
    {
        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetRestartGameQuestionWindow(_uiRoot.gameObject);
    }

    public void OnTutorialButtonClick()
    {
        AllServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(TutorialSceneIndex);
    }

    public void OnLeaderBoardButtonClick()
    {
        GameObject leaderboardObject = AllServicesContainer.Instance.GetService<IUiWindowFactory>()
            .GetLeaderboardWindow(_uiRoot.gameObject);

        leaderboardObject.TryGetComponent(out YandexLeaderboard leaderboard);
        
        leaderboard.OpenWindow();
    }

    public void OnSettingsButtonClick()
    {
        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetLanguageChangerWindow(_uiRoot.gameObject);
    }
    
    public void OnQuitGameButtonClick()
    {
        Application.Quit();
    }
}
