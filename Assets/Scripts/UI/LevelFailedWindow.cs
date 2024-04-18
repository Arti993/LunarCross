using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class LevelFailedWindow : UIWindow
{
    [SerializeField] private float _restartPanelTopPosY;
    [SerializeField] private float _panelAnimationDuration = 0.5f;
    [SerializeField] private float _stopTimeDelay = 0.15f;
    
    private RectTransform _restartPanelRect;
    private bool _isGamePaused;
    
    private void Awake()
    {
        _restartPanelRect = GetComponent<RectTransform>();
        PauseGame();
    }
    
    public void RestartLevel()
    {
        _isGamePaused = false;

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        AllServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(sceneIndex);
    }

    private void PanelIntro()
    {
        _restartPanelRect.DOAnchorPosY(_restartPanelTopPosY, _panelAnimationDuration).SetUpdate(true);
    }

    private void PauseGame()
    {
        if (_isGamePaused)
            throw new InvalidOperationException();

        _isGamePaused = true;

        DestroyPauseButton();
        
        StartCoroutine(StopTime());
    }
    
    private IEnumerator StopTime()
    {
        yield return new WaitForSeconds(_stopTimeDelay);

        Time.timeScale = 0f;
        
        PanelIntro();
    }
}
