using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private const string FirstTimeLaunched = "FirstTimeLaunched";
    private Canvas _uiRoot;

    private void Awake()
    {
        _uiRoot = GetComponentInParent<Canvas>();
    }

    public void OnPlayButtonClick()
    {
        DIServicesContainer.Instance.GetService<IScenesLoader>().LoadGameplayScene();
    }

    public void OnLevelsChooseButtonCLick()
    {
        DIServicesContainer.Instance.GetService<IScenesLoader>().LoadLevelChooseScene();
    }

    public void OnNewGameButtonClick()
    {
        if (PlayerPrefs.HasKey(FirstTimeLaunched))
            DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetRestartGameQuestionWindow(_uiRoot.gameObject);
        else
        {
            PlayerPrefs.SetInt(FirstTimeLaunched, 1);
            PlayerPrefs.Save();
            
            OnPlayButtonClick();
        }
    }

    public void OnTutorialButtonClick()
    {
        DIServicesContainer.Instance.GetService<IScenesLoader>().LoadTutorialScene();
    }

    public void OnLeaderBoardButtonClick()
    {
        GameObject leaderboardObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>()
            .GetLeaderboardWindow(_uiRoot.gameObject);

        leaderboardObject.TryGetComponent(out YandexLeaderboard leaderboard);

        leaderboard.OpenWindow();
    }

    public void OnSettingsButtonClick()
    {
        DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetSettingsWindow(_uiRoot.gameObject);
    }

    public void OnQuitGameButtonClick()
    {
        Application.Quit();
    }
}