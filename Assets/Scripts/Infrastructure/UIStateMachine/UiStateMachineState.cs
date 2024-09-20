using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
