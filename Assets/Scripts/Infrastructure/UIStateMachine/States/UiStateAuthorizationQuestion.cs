using Data;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStateAuthorizationQuestion : UiStateMachineState
    {
        public UiStateAuthorizationQuestion()
        {
            PrefabPath = PrefabsPaths.AuthorizationQuestion;
        }
    }
}
