using Data;
using Infrastructure.Services.Factories.UiFactory;
using UI;
using UnityEngine;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStateMainMenu : UiStateMachineState
    {
        private MainMenu _menu;

        public override void Enter()
        {
            if (_menu == null)
            {
                GameObject mainMenuObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>()
                    .GetWindow(PrefabsPaths.MainMenuButtons, GetUiRoot());

                _menu = mainMenuObject.GetComponent<MainMenu>();
            }

            _menu.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            _menu.Disable();
        }
    }
}