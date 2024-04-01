using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIWindow : MonoBehaviour
{
    private const int MainMenuSceneIndex = 0;
    
    public void GoToMainMenu()
    {
        PlayerPrefs.DeleteKey("SelectedLevelNumber");
        
        AllServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(MainMenuSceneIndex);
    }

    protected IEnumerator Destroy(float duration)
    {
        yield return new WaitForSeconds(duration);

        Destroy(gameObject);
    }
    
    protected void DestroyPauseButton()
    {
        GameObject uiRootObject = AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();

        PauseButton pauseButton = uiRootObject.GetComponentInChildren<PauseButton>();
        
        Destroy(pauseButton.gameObject);
    }
}
