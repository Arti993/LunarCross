using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private const string NotFirstGameLaunch = "NotFirstGameLaunch";
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
        if (PlayerPrefs.HasKey(NotFirstGameLaunch))
            DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetRestartGameQuestionWindow(_uiRoot.gameObject);
        else
        {
            OnTutorialButtonClick();
            
            PlayerPrefs.SetInt(NotFirstGameLaunch, 1);
            PlayerPrefs.Save();
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

    public void TestGameComplete()
    {
        //Тестовый метод, потом удалить НО СНАЧАЛА СДЕЛАТЬ МУЗЫКУ И ШУМ РАКЕТЫ
        
        PlayerPrefs.DeleteKey(NotFirstGameLaunch);
        
        DIServicesContainer.Instance.GetService<IGameProgress>().ClearSaves();
        
        DIServicesContainer.Instance.GetService<IScenesLoader>().LoadFinalScene();
    }
}