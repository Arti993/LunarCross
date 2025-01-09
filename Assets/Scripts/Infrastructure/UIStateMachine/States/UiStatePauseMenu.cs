using Data;
using UI;
using UnityEngine;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStatePauseMenu : UiStateMachineState
    {
        private PauseMenu _pauseMenu;

        public UiStatePauseMenu()
        {
            PrefabPath = PrefabsPaths.PauseMenu;
        }

        public override void Enter()
        {
            if (_pauseMenu == null)
            {
                GameObject uiWindowObject = GetUiObject();

                UiWindow = uiWindowObject.GetComponentInChildren<UiWindow>();

                _pauseMenu = uiWindowObject.GetComponentInChildren<PauseMenu>();
            }

            _pauseMenu.PauseGame();
        }
    }
}
