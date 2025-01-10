using Infrastructure.Services.Factories.UiFactory;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStateNoWindow : UiStateMachineState
    {
        public UiStateNoWindow(IUiWindowFactory uiWindowFactory) : base(uiWindowFactory)
        {
        }
        
        public override void Enter()
        {
        }

        public override void Exit()
        {
        }
    }
}
