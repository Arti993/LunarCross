using Data;
using Infrastructure.UIStateMachine.States;
using YG;
using UnityEngine.SceneManagement;

namespace UI
{
    public class AuthorizationQuestion : UiWindow
    {
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
                UiStateMachine.SetState<UiStateMainMenu>();
                return;
            }

            if (currentSceneIndex == (int) SceneIndex.Final)
                UiStateMachine.SetState<UiStateGameComplete>();
        }

        private void OpenLeaderBoard()
        {
            UiStateMachine.SetState<UiStateLeaderboard>();
        }
    }
}