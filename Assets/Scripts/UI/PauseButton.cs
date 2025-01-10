using System;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using Reflex.Extensions;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PauseButton : CustomButton
    {
        private IUiStateMachine _uiStateMachine;
        
        public event Action Enabled;
        public event Action Disabled;

        protected override void Construct()
        {
            base.Construct();
            
            _uiStateMachine = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IUiStateMachine>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            Clicked += OnClicked;

            Enabled?.Invoke();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            Clicked -= OnClicked;

            Disabled?.Invoke();
        }

        private void OnClicked()
        {
            _uiStateMachine.SetState<UiStatePauseMenu>();
        }
    }
}
