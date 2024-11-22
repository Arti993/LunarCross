using Data;
using Infrastructure.Services.Factories.UiFactory;
using UI;
using UnityEngine;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStateRestartGameQuestion : UiStateMachineState
    {
        public override void Enter()
        {
            if (UiWindow == null)
            {
                GameObject uiWindowObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>()
                    .GetWindow(PrefabsPaths.RestartGameQuestion, GetUiRoot());

                UiWindow = uiWindowObject.GetComponent<UIWindow>();
            }

            base.Enter();
        }
    }
}
