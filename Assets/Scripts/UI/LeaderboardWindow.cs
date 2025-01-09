using Data;
using Infrastructure;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using Reflex.Attributes;
using UnityEngine;
using YG;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LeaderboardWindow : UiWindow
    {
        [SerializeField] private LeaderboardYG _leaderboardYG;

        private IUiStateMachine _uiStateMachine;

        [Inject]
        private void Construct(IUiStateMachine uiStateMachine)
        {
            _uiStateMachine = uiStateMachine;
        }
        
        public void OpenWindow()
        {
            PanelIntro();
        }

        public void Exit()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentSceneIndex == (int) SceneIndex.MainMenu)
                _uiStateMachine.SetState<UiStateMainMenu>();

            if (currentSceneIndex == (int) SceneIndex.Final)
                _uiStateMachine.SetState<UiStateGameComplete>();
        }
    }
}