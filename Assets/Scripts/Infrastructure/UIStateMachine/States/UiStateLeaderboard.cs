using Data;
using UI;
using UnityEngine;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStateLeaderboard : UiStateMachineState
    {
        private LeaderboardWindow _leaderboardWindow;

        public UiStateLeaderboard()
        {
            PrefabPath = PrefabsPaths.LeaderboardWindow;
        }

        public override void Enter()
        {
            if (_leaderboardWindow == null)
            {
                GameObject leaderboardObject = GetUiObject();

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
}
