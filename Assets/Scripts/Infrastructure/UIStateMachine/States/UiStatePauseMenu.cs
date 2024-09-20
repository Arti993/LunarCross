using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiStatePauseMenu : UiStateMachineState
{
    private PauseMenu _pauseMenu;
    
    public override void Enter()
    {
        if (UiWindow == null)
        {
            GameObject uiWindowObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>()
                .GetWindow(PrefabsPaths.PauseMenu,GetUiRoot());

            UiWindow = uiWindowObject.GetComponentInChildren<UIWindow>();
            
            _pauseMenu = uiWindowObject.GetComponentInChildren<PauseMenu>();
        }

        _pauseMenu.PauseGame();
    }
}
