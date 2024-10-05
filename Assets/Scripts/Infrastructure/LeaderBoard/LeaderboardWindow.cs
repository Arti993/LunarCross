using UnityEngine;
using YG;
using UnityEngine.SceneManagement;

public class LeaderboardWindow : UIWindow
{
    [SerializeField] private LeaderboardYG _leaderboardYG;

    public void OpenWindow()
    {
        PanelIntro();
    }

    public void Exit()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == (int) SceneIndex.MainMenu)
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateMainMenu>();

        if (currentSceneIndex == (int) SceneIndex.Final)
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateGameComplete>();
    }
}