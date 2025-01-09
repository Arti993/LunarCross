using Data;

namespace Infrastructure.UIStateMachine.States.TutorialStates
{
    public class UiStateTutorialAstronauts : UiStateMachineTutorialState
    {
        public UiStateTutorialAstronauts()
        {
            PrefabPath = PrefabsPaths.TutorialCollectingWindow;
        }
    }
}