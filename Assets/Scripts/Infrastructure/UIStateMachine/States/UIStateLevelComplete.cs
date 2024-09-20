using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateLevelComplete : UiStateMachineState
{
    public override void Enter()
    {
        if (UiWindow == null)
        {
            GameObject uiWindowObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetLevelCompleteWindow(GetUiRoot());

            UiWindow = uiWindowObject.GetComponent<UIWindow>();
        }

        base.Enter();
    }
}
