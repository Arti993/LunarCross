using Data;
using Infrastructure.Services.Factories.UiFactory;

namespace Infrastructure.UIStateMachine.States.TutorialStates
{
    public class UiStateTutorialAstronauts : UiStateMachineTutorialState
    {
        public UiStateTutorialAstronauts(IUiWindowFactory uiWindowFactory) : base(uiWindowFactory)
        {
            PrefabPath = PrefabsPaths.TutorialCollectingWindow;
        }
    }
}