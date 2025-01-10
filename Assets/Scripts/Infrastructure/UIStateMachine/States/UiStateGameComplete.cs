using Data;
using Infrastructure.Services.Factories.UiFactory;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStateGameComplete : UiStateMachineState
    {
        public UiStateGameComplete(IUiWindowFactory uiWindowFactory) : base(uiWindowFactory)
        {
            PrefabPath = PrefabsPaths.GameCompleteWindow;
        }
    }
}
