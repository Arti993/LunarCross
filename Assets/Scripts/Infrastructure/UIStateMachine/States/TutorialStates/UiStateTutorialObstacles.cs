using Data;
using Infrastructure.Services.Factories.UiFactory;

namespace Infrastructure.UIStateMachine.States.TutorialStates
{
    public class UiStateTutorialObstacles : UiStateMachineTutorialState
    {
        public UiStateTutorialObstacles(IUiWindowFactory uiWindowFactory) : base(uiWindowFactory)
        {
            PrefabPath = PrefabsPaths.TutorialObstaclesWindow;
        }
    }
}
