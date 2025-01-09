using Data;
using Infrastructure;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using Reflex.Attributes;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameSettingsWindow : UiWindow
    {
        private IUiStateMachine _uiStateMachine;

        [Inject]
        private void Construct(IUiStateMachine uiStateMachine)
        {
            _uiStateMachine = uiStateMachine;
        }
        
        public void Exit()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentSceneIndex == (int) SceneIndex.MainMenu)
                _uiStateMachine.SetState<UiStateMainMenu>();

            if (currentSceneIndex == (int) SceneIndex.Gameplay || currentSceneIndex == (int) SceneIndex.Tutorial)
                _uiStateMachine.SetState<UiStatePauseMenu>();
        }
    }
}
