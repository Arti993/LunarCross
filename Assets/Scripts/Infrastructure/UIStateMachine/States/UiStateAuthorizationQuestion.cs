using Data;
using Infrastructure.Services.Factories.UiFactory;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStateAuthorizationQuestion : UiStateMachineState
    {
        public UiStateAuthorizationQuestion(IUiWindowFactory uiWindowFactory) : base(uiWindowFactory)
        {
            PrefabPath = PrefabsPaths.AuthorizationQuestion;
        }
    }
}
