using System;
using Infrastructure;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using Reflex.Attributes;

namespace UI
{
    public class PauseButton : CustomButton
    {
        private IUiStateMachine _uiStateMachine;
        
        public event Action Enabled;
        public event Action Disabled;
        
        [Inject]
        private void Construct(IUiStateMachine uiStateMachine)
        {
            _uiStateMachine = uiStateMachine;
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
