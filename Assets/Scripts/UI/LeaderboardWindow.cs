using Data;
using Infrastructure.UIStateMachine.States;
using UnityEngine;
using YG;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LeaderboardWindow : UiWindow
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
                UiStateMachine.SetState<UiStateMainMenu>();

            if (currentSceneIndex == (int) SceneIndex.Final)
                UiStateMachine.SetState<UiStateGameComplete>();
        }
    }
}