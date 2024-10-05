using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiStateAuthorizationQuestion : UiStateMachineState
{
    public override void Enter()
    {
        if (UiWindow == null)
        {
            GameObject uiWindowObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>()
                .GetWindow(PrefabsPaths.AuthorizationQuestion,GetUiRoot());

            UiWindow = uiWindowObject.GetComponent<UIWindow>();
        }

        base.Enter();
    }
}
