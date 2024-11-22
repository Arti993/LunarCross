using Data;
using Infrastructure;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using YG;
using UnityEngine.SceneManagement;

namespace UI
{
    public class AuthorizationQuestion : UIWindow
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
                DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateMainMenu>();
                return;
            }

            if (currentSceneIndex == (int) SceneIndex.Final)
                DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateGameComplete>();
        }

        private void OpenLeaderBoard()
        {
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateLeaderboard>();
        }
    }
}