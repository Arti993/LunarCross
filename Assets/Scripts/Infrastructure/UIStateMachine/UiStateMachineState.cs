using Infrastructure.Services.Factories.UiFactory;
using UI;
using UnityEngine;

namespace Infrastructure.UIStateMachine
{
    public abstract class UiStateMachineState
    {
        protected UIWindow UiWindow;

        public virtual void Enter()
        {
            UiWindow.PanelIntro();
        }

        public virtual void Exit()
        {
            UiWindow.PanelOutro();
        }

        protected GameObject GetUiRoot()
        {
            return DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();
        }
    }
}
