using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PauseMenu : UIWindow
{
    [SerializeField] private RectTransform _pausePanelRect;
    [SerializeField] private CanvasGroup _backgroundPanel;
    [SerializeField] private float _pausePanelTopPosY = 850;
    [SerializeField] private float _pausePanelBottomPosY = 0f;
    [SerializeField] private float _pausePanelAnimationDuration = 0.5f;

    private bool _isGamePaused = false;

    private void Awake()
    {
        PauseGame();
    }
    
    public void ResumeGame()
    {
        if (_isGamePaused == false)
            throw new InvalidOperationException();

        PausePanelOutro();

        StartCoroutine(Destroy(_pausePanelAnimationDuration));

        Time.timeScale = 1f;

        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetPauseButton();
    }

    public void RestartLevel()
    {
        _isGamePaused = false;
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeGameSettings()
    {
    }

    private void PausePanelIntro()
    {
        _backgroundPanel.DOFade(1, _pausePanelAnimationDuration).SetUpdate((true));
        _pausePanelRect.DOAnchorPosY(_pausePanelBottomPosY, _pausePanelAnimationDuration).SetUpdate(true);
    }

    private void PausePanelOutro()
    {
        _backgroundPanel.DOFade(0, _pausePanelAnimationDuration).SetUpdate((true));
        _pausePanelRect.DOAnchorPosY(_pausePanelTopPosY, _pausePanelAnimationDuration).SetUpdate(true);
    }

    private void PauseGame()
    {
        if (_isGamePaused)
            throw new InvalidOperationException();

        _isGamePaused = true;

        DestroyPauseButton();
        
        PausePanelIntro();

        Time.timeScale = 0f;
    }
}