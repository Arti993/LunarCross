using Data;
using UnityEngine;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStatePauseButton : UiStateMachineState
    {
        private GameObject _pauseButton;

        public UiStatePauseButton()
        {
            PrefabPath = PrefabsPaths.PauseButton;
        }

        public override void Enter()
        {
            if (_pauseButton == null)
                _pauseButton = GetUiObject();

            _pauseButton.SetActive(true);
        }

        public override void Exit()
        {
            _pauseButton.SetActive(false);
        }
    }
}