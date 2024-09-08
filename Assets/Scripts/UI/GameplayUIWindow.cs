using UnityEngine;

public class GameplayUIWindow : UIWindow
{
    public void GoToMainMenu()
    {
        DIServicesContainer.Instance.GetService<IGameProgress>().ClearSelectedLevel();
        
        DIServicesContainer.Instance.GetService<IScenesLoader>().LoadMainMenuScene();
    }

    protected void DestroyPauseButton()
    {
        GameObject uiRootObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();

        PauseButton pauseButton = uiRootObject.GetComponentInChildren<PauseButton>();

        if (pauseButton != null)
            Destroy(pauseButton.gameObject);
    }
}