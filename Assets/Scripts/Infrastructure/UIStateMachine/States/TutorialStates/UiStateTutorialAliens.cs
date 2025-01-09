using Data;

namespace Infrastructure.UIStateMachine.States.TutorialStates
{
    public class UiStateTutorialAliens : UiStateMachineTutorialState
    {
        public UiStateTutorialAliens()
        {
            PrefabPath = PrefabsPaths.TutorialAliensWindow;
        }
    }
}
