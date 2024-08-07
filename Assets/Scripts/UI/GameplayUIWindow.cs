using UnityEngine;

public class GameplayUIWindow : UIWindow
{
    private const string SelectedLevelNumber = "SelectedLevelNumber";
    private const int MainMenuSceneIndex = 1;
    
    public void GoToMainMenu()
    {
        PlayerPrefs.DeleteKey(SelectedLevelNumber);

        AllServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(MainMenuSceneIndex);
    }

    protected void DestroyPauseButton()
    {
        GameObject uiRootObject = AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();

        PauseButton pauseButton = uiRootObject.GetComponentInChildren<PauseButton>();

        if (pauseButton != null)
            Destroy(pauseButton.gameObject);
    }
}