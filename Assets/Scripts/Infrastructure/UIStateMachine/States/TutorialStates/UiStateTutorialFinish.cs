using Data;

namespace Infrastructure.UIStateMachine.States.TutorialStates
{
    public class UiStateTutorialFinish : UiStateMachineTutorialState
    {
        public UiStateTutorialFinish()
        {
            PrefabPath = PrefabsPaths.TutorialFinishWindow;
        }
    }
}
