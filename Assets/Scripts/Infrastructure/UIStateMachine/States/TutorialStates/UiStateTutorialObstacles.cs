using Data;

namespace Infrastructure.UIStateMachine.States.TutorialStates
{
    public class UiStateTutorialObstacles : UiStateMachineTutorialState
    {
        public UiStateTutorialObstacles()
        {
            PrefabPath = PrefabsPaths.TutorialObstaclesWindow;
        }
    }
}
