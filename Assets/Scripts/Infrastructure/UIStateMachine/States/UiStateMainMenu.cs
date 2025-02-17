using Data;
using Infrastructure.Services.Factories.UiFactory;
using UI;
using UnityEngine;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStateMainMenu : UiStateMachineState
    {
        private MainMenu _menu;
        
        public UiStateMainMenu(IUiWindowFactory uiWindowFactory) : base(uiWindowFactory)
        {
            PrefabPath = PrefabsPaths.MainMenuButtons;
        }

        public override void Enter()
        {
            if (_menu == null)
            {
                GameObject mainMenuObject = GetUiObject();

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