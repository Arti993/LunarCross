using UnityEngine;

public class UiStateLeaderboard : UiStateMachineState
{
    private LeaderboardWindow _leaderboardWindow;
    
    public override void Enter()
    {
        if (_leaderboardWindow == null)
        {
            GameObject leaderboardObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>()
                .GetWindow(PrefabsPaths.LeaderboardWindow,GetUiRoot());

            leaderboardObject.TryGetComponent(out LeaderboardWindow leaderboard);

            _leaderboardWindow = leaderboard;
        }

        _leaderboardWindow.OpenWindow();
    }

    public override void Exit()
    {
        _leaderboardWindow.PanelOutro();
    }
}
