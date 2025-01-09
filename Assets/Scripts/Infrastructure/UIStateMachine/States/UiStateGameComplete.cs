using Data;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStateGameComplete : UiStateMachineState
    {
        public UiStateGameComplete()
        {
            PrefabPath = PrefabsPaths.GameCompleteWindow;
        }
    }
}
