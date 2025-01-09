using Data;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStateSettings : UiStateMachineState
    {
        public UiStateSettings()
        {
            PrefabPath = PrefabsPaths.SettingsWindow;
        }
    }
}
