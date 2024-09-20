using UnityEngine;

public class GameCompleteWindow : UIWindow
{
    private void Awake()
    {
        PanelIntro();
    }

    public void OpenLeaderBoard()
    {
        GameObject uiRoot = GetComponentInParent<Canvas>().gameObject;

        GameObject leaderboardObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>()
            .GetWindow(PrefabsPaths.LeaderboardWindow, uiRoot);

        leaderboardObject.TryGetComponent(out YandexLeaderboard leaderboard);

        leaderboard.OpenWindow();
    }
}
