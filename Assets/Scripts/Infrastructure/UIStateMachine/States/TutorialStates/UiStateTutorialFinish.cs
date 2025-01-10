using Data;
using Infrastructure.Services.Factories.UiFactory;

namespace Infrastructure.UIStateMachine.States.TutorialStates
{
    public class UiStateTutorialFinish : UiStateMachineTutorialState
    {
        public UiStateTutorialFinish(IUiWindowFactory uiWindowFactory) : base(uiWindowFactory)
        {
            PrefabPath = PrefabsPaths.TutorialFinishWindow;
        }
    }
}
