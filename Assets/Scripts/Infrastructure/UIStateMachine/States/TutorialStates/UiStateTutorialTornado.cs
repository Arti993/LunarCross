using Data;
using Infrastructure.Services.Factories.UiFactory;

namespace Infrastructure.UIStateMachine.States.TutorialStates
{
    public class UiStateTutorialTornado : UiStateMachineTutorialState
    {
        public UiStateTutorialTornado(IUiWindowFactory uiWindowFactory) : base(uiWindowFactory)
        {
            PrefabPath = PrefabsPaths.TutorialTornadoWindow;
        }
    }
}
