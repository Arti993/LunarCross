using UnityEngine;

public class UiStateGameComplete : UiStateMachineState
{
    public override void Enter()
    {
        if (UiWindow == null)
        {
            GameObject uiWindowObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>()
                .GetWindow(PrefabsPaths.GameCompleteWindow,GetUiRoot());

            UiWindow = uiWindowObject.GetComponent<UIWindow>();
        }

        base.Enter();
    }
}
