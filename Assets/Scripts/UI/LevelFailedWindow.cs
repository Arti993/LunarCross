using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RectTransform))]
public class LevelFailedWindow : UIWindow
{
    [SerializeField] private float _stopTimeDelay = 0.15f;
    
    private bool _isGamePaused;
    
    private void Awake()
    {
        PauseGame();
        
        DestroyPauseButton();
    }
    
    public void RestartLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        AllServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(sceneIndex);
    }

    private void PauseGame()
    {
        if (_isGamePaused)
            return;

        _isGamePaused = true;
        
        StartCoroutine(StopTime());
    }
    
    private IEnumerator StopTime()
    {
        yield return new WaitForSeconds(_stopTimeDelay);

        Time.timeScale = 0f;
        
        PanelIntro();
    }
}
