using UnityEngine;

public class GameCompleteWindow : UIWindow
{
    private void Awake()
    {
        PanelIntro();
    }

    public void OpenLeaderBoard()
    {
        Canvas uiRoot = GetComponentInParent<Canvas>();
        
        GameObject leaderboardObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>()
            .GetLeaderboardWindow(uiRoot.gameObject);

        leaderboardObject.TryGetComponent(out YandexLeaderboard leaderboard);

        leaderboard.OpenWindow();
    }
}
