using Infrastructure.Services.Factories.UiFactory;
using UI;
using UnityEngine;

namespace Infrastructure.UIStateMachine.States
{
    public class UIStateLevelComplete : UiStateMachineState
    {
        public override void Enter()
        {
            if (UiWindow == null)
            {
                GameObject uiWindowObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>()
                    .GetLevelCompleteWindow(GetUiRoot());

                UiWindow = uiWindowObject.GetComponent<UIWindow>();
            }

            base.Enter();
        }
    }
}
