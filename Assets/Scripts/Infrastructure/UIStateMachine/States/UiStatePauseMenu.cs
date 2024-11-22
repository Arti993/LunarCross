using Data;
using Infrastructure.Services.Factories.UiFactory;
using UI;
using UnityEngine;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStatePauseMenu : UiStateMachineState
    {
        private PauseMenu _pauseMenu;

        public override void Enter()
        {
            if (_pauseMenu == null)
            {
                GameObject uiWindowObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>()
                    .GetWindow(PrefabsPaths.PauseMenu, GetUiRoot());

                UiWindow = uiWindowObject.GetComponentInChildren<UIWindow>();

                _pauseMenu = uiWindowObject.GetComponentInChildren<PauseMenu>();
            }

            _pauseMenu.PauseGame();
        }
    }
}
