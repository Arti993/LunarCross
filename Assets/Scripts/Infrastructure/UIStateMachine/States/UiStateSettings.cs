using Data;
using Infrastructure.Services.Factories.UiFactory;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStateSettings : UiStateMachineState
    {
        public UiStateSettings(IUiWindowFactory uiWindowFactory) : base(uiWindowFactory)
        {
            PrefabPath = PrefabsPaths.SettingsWindow;
        }
    }
}
