using Data;
using Infrastructure.Services.Factories.UiFactory;

namespace Infrastructure.UIStateMachine.States.TutorialStates
{
    public class UiStateTutorialKeyboardControl : UiStateMachineTutorialState
    {
        public UiStateTutorialKeyboardControl(IUiWindowFactory uiWindowFactory) : base(uiWindowFactory)
        {
            PrefabPath = PrefabsPaths.TutorialKeyboardControl;
        }
    }
}
