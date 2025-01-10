using Data;
using Infrastructure.Services.Factories.UiFactory;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStateRestartGameQuestion : UiStateMachineState
    {
        public UiStateRestartGameQuestion(IUiWindowFactory uiWindowFactory) : base(uiWindowFactory)
        {
            PrefabPath = PrefabsPaths.RestartGameQuestion;
        }
    }
}
