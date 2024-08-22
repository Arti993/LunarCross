using UnityEngine;

public class GameplayUIWindow : UIWindow
{
    private const string SelectedLevelNumber = "SelectedLevelNumber";

    public void GoToMainMenu()
    {
        PlayerPrefs.DeleteKey(SelectedLevelNumber);
        
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