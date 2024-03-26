using UnityEngine;
using UnityEngine.SceneManagement;

public class UIWindow : MonoBehaviour
{
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    
    protected void DestroyPauseButton()
    {
        GameObject uiRootObject = AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();

        PauseButton pauseButton = uiRootObject.GetComponentInChildren<PauseButton>();
        
        Destroy(pauseButton.gameObject);
    }
}
