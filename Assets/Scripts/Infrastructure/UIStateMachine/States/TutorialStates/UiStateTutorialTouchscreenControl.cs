using Data;
using Infrastructure.Services.Factories.UiFactory;

namespace Infrastructure.UIStateMachine.States.TutorialStates
{
    public class UiStateTutorialTouchscreenControl : UiStateMachineTutorialState
    {
        public UiStateTutorialTouchscreenControl(IUiWindowFactory uiWindowFactory) : base(uiWindowFactory)
        {
            PrefabPath = PrefabsPaths.TutorialTouchscreenControl;
        }
    }
}