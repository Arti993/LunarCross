using Infrastructure.Services.Factories.UiFactory;
using UnityEngine;

namespace Infrastructure.UIStateMachine.States
{
    public class UIStateLevelComplete : UiStateMachineState
    {
        protected override GameObject GetUiObject()
        {
            return UiWindowFactory.GetLevelCompleteWindow(GetUiRoot());
        }

        public UIStateLevelComplete(IUiWindowFactory uiWindowFactory) : base(uiWindowFactory)
        {
        }
    }
}
