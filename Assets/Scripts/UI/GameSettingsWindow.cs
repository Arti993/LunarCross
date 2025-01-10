using Data;
using Infrastructure.UIStateMachine.States;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameSettingsWindow : UiWindow
    {
        public void Exit()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentSceneIndex == (int) SceneIndex.MainMenu)
                UiStateMachine.SetState<UiStateMainMenu>();

            if (currentSceneIndex == (int) SceneIndex.Gameplay || currentSceneIndex == (int) SceneIndex.Tutorial)
                UiStateMachine.SetState<UiStatePauseMenu>();
        }
    }
}
