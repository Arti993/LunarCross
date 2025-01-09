using Infrastructure.Services.Factories.UiFactory;
using Reflex.Attributes;
using UI;
using UnityEngine;

namespace Infrastructure.UIStateMachine
{
    public abstract class UiStateMachineState
    {
        protected string PrefabPath;
        protected UiWindow UiWindow;
        protected IUiWindowFactory UiWindowFactory;
        
        [Inject]
        private void Construct(IUiWindowFactory uiWindowFactory)
        {
            UiWindowFactory = uiWindowFactory;
        }
        
        public virtual void Enter()
        {
            if (UiWindow == null)
            {
                GameObject uiWindowObject = GetUiObject();

                UiWindow = uiWindowObject.GetComponent<UiWindow>();
            }
            
            UiWindow.PanelIntro();
        }

        public virtual void Exit()
        {
            UiWindow.PanelOutro();
        }

        protected virtual GameObject GetUiObject()
        {
            return UiWindowFactory.GetWindow(PrefabPath, GetUiRoot());
        }

        protected GameObject GetUiRoot()
        {
            return UiWindowFactory.GetUIRoot();
        }
    }
}
