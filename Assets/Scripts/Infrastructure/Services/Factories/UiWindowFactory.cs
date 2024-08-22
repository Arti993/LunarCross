using UnityEngine;

public class UiWindowFactory : IUiWindowFactory
{
    private readonly IAssets _provider;
    private GameObject _uiRootObject;

    public UiWindowFactory(IAssets provider)
    {
        _provider = provider;
    }

    public GameObject GetUIRoot()
    {
        return _uiRootObject ? _uiRootObject : (_uiRootObject = _provider.Instantiate("Prefabs/UI/UIRoot"));
    }

    public void DeleteUIRoot()
    {
        _uiRootObject = null;
    }

    public GameObject GetPauseButton(GameObject parent)
    {
        _uiRootObject.TryGetComponent(out UIRoot uiRoot);

        if (uiRoot.PauseButton == null)
        {
            GameObject pauseButtonObject = _provider.Instantiate("Prefabs/UI/PauseButton", parent.transform);

            pauseButtonObject.TryGetComponent(out PauseButton pauseButton);
            
            uiRoot.SetPauseButtonIfItNotExist(pauseButton);

            return pauseButtonObject;
        }
        else
        {
            return uiRoot.PauseButton.gameObject;
        }
    }

    public GameObject GetLevelCompleteWindow(GameObject parent)
    {
        return _provider.Instantiate("Prefabs/UI/LevelCompleteWindow", parent.transform);
    }
    
    public GameObject GetPauseMenuWindow(GameObject parent)
    {
        return _provider.Instantiate("Prefabs/UI/PauseMenu", parent.transform);
    }
    
    public GameObject GetLevelFailedWindow(GameObject parent)
    {
        return _provider.Instantiate("Prefabs/UI/LevelFailedWindow", parent.transform);
    }

    public GameObject GetLevelNumberTitle(GameObject parent)
    {
        return _provider.Instantiate("Prefabs/UI/LevelNumberTitle", parent.transform);
    }

    public GameObject GetRestartGameQuestionWindow(GameObject parent)
    {
        return _provider.Instantiate("Prefabs/UI/RestartGameQuestionPanel", parent.transform);
    }

    public GameObject GetLeaderBoardElement(GameObject parent)
    {
        return _provider.Instantiate("Prefabs/UI/LeaderboardElement", parent.transform);
    }

    public GameObject GetTutorialKeyboardControlWindow(GameObject parent)
    {
        return _provider.Instantiate("Prefabs/UI/Tutorial/TutorialKeyboardControlWindow", parent.transform);
    }

    public GameObject GetTutorialTouchscreenControlWindow(GameObject parent)
    {
        return _provider.Instantiate("Prefabs/UI/Tutorial/TutorialTouchscreenControlWindow", parent.transform);
    }

    public GameObject GetTutorialCollectingWindow(GameObject parent)
    {
        return _provider.Instantiate("Prefabs/UI/Tutorial/TutorialCollectingWindow", parent.transform);
    }

    public GameObject GetTutorialAliensWindow(GameObject parent)
    {
        return _provider.Instantiate("Prefabs/UI/Tutorial/TutorialAliensWindow", parent.transform);
    }

    public GameObject GetTutorialObstaclesWindow(GameObject parent)
    {
        return _provider.Instantiate("Prefabs/UI/Tutorial/TutorialObstaclesWindow", parent.transform);
    }

    public GameObject GetTutorialTornadoWindow(GameObject parent)
    {
        return _provider.Instantiate("Prefabs/UI/Tutorial/TutorialTornadoWindow", parent.transform);
    }

    public GameObject GetTutorialFinishWindow(GameObject parent)
    {
        return _provider.Instantiate("Prefabs/UI/Tutorial/TutorialFinishWindow", parent.transform);
    }

    public GameObject GetMainMenuButtonsWindow(GameObject parent)
    {
        return _provider.Instantiate("Prefabs/UI/MainMenuButtons", parent.transform);
    }

    public GameObject GetLeaderboardWindow(GameObject parent)
    {
        return _provider.Instantiate("Prefabs/UI/LeaderBoardWindow", parent.transform);
    }

    public GameObject GetSettingsWindow(GameObject parent)
    {
        return _provider.Instantiate("Prefabs/UI/SettingsWindow", parent.transform);
    }
}
