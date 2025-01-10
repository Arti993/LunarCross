using Data;
using Infrastructure.Services.Factories.UiFactory;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStateLevelFailed : UiStateMachineState
    {
        public UiStateLevelFailed(IUiWindowFactory uiWindowFactory) : base(uiWindowFactory)
        {
            PrefabPath = PrefabsPaths.LevelFailedWindow;
        }
        
        public override void Exit()
        {
        }
    }
}
