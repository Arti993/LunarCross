using UnityEngine;

public class UiStateLevelFailed : UiStateMachineState
{
    public override void Enter()
    {
        if (UiWindow == null)
        {
            GameObject uiWindowObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>()
                .GetWindow(PrefabsPaths.LevelFailedWindow,GetUiRoot());

            UiWindow = uiWindowObject.GetComponent<UIWindow>();
        }

        base.Enter();
    }

    public override void Exit()
    {
    }
}
