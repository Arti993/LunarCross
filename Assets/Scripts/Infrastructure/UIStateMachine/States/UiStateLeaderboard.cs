using UnityEngine;

public class UiStateLeaderboard : UiStateMachineState
{
    private YandexLeaderboard _leaderboard;
    
    public override void Enter()
    {
        if (_leaderboard == null)
        {
            GameObject leaderboardObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>()
                .GetWindow(PrefabsPaths.LeaderboardWindow,GetUiRoot());

            leaderboardObject.TryGetComponent(out YandexLeaderboard leaderboard);

            _leaderboard = leaderboard;
        }

        _leaderboard.OpenWindow();
    }

    public override void Exit()
    {
        _leaderboard.PanelOutro();
    }
}
