using Data;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using Reflex.Attributes;
using YG;
using UnityEngine.SceneManagement;

namespace UI
{
    public class AuthorizationQuestion : UiWindow
    {
        private IUiStateMachine _uiStateMachine;
        
        [Inject]
        private void Construct(IUiStateMachine uiStateMachine)
        {
            _uiStateMachine = uiStateMachine;
        }
        
        public void OnYesButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (YandexGame.auth)
            OpenLeaderBoard();
        else
            YandexGame.AuthDialog();
#endif
        }

        public void OnNoButtonClick()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentSceneIndex == (int) SceneIndex.MainMenu)
            {
                _uiStateMachine.SetState<UiStateMainMenu>();
                return;
            }

            if (currentSceneIndex == (int) SceneIndex.Final)
                _uiStateMachine.SetState<UiStateGameComplete>();
        }

        private void OpenLeaderBoard()
        {
            _uiStateMachine.SetState<UiStateLeaderboard>();
        }
    }
}