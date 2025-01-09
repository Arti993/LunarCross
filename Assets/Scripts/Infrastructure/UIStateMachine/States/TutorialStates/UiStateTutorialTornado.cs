using Data;

namespace Infrastructure.UIStateMachine.States.TutorialStates
{
    public class UiStateTutorialTornado : UiStateMachineTutorialState
    {
        public UiStateTutorialTornado()
        {
            PrefabPath = PrefabsPaths.TutorialTornadoWindow;
        }
    }
}
