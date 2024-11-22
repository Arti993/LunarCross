using Data;
using Infrastructure.Services.Factories.UiFactory;
using UnityEngine;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStatePauseButton : UiStateMachineState
    {
        private GameObject _pauseButton;

        public override void Enter()
        {
            if (_pauseButton == null)
                _pauseButton = DIServicesContainer.Instance.GetService<IUiWindowFactory>()
                    .GetWindow(PrefabsPaths.PauseButton, GetUiRoot());

            _pauseButton.SetActive(true);
        }

        public override void Exit()
        {
            _pauseButton.SetActive(false);
        }
    }
}