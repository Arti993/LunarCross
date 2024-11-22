using System;
using Infrastructure;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;

namespace UI
{
    public class PauseButton : CustomButton
    {
        public event Action Enabled;
        public event Action Disabled;

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
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStatePauseMenu>();
        }
    }
}
