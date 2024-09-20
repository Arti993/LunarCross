using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RectTransform))]
public class LevelFailedWindow : MenuEscapeWindow
{
    [SerializeField] private float _stopTimeDelay = 0.15f;
    
    private bool _isFirstFailReacted;
    
    private void Awake()
    {
        PauseGame();
    }
    
    public void RestartLevel()
    {
        DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateNoWindow>();
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(currentSceneIndex);
    }

    private void PauseGame()
    {
        if (_isFirstFailReacted)
            return;

        _isFirstFailReacted = true;
        
        StartCoroutine(StopTime());
    }
    
    private IEnumerator StopTime()
    {
        yield return new WaitForSeconds(_stopTimeDelay);

        Time.timeScale = 0f;
    }
}
