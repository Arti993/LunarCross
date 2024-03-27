using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIWindow : MonoBehaviour
{
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        PlayerPrefs.DeleteKey("SelectedLevelNumber");
        
        SceneManager.LoadScene(0);
    }
    
    public IEnumerator Destroy(float duration)
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
