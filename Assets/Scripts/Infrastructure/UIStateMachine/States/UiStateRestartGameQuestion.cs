using Data;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStateRestartGameQuestion : UiStateMachineState
    {
        public UiStateRestartGameQuestion()
        {
            PrefabPath = PrefabsPaths.RestartGameQuestion;
        }
    }
}
