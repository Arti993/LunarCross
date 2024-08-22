using System;
using System.Collections;
using UnityEngine;

public class TutorialWindow : GameplayUIWindow
{
    private const float DelayBeforeShowWindow = 2.3f;

    protected virtual void Awake()
    {
        Vector3 startPosition = PanelRect.localPosition;

        startPosition.y = PanelBottomPosY;

        PanelRect.localPosition = startPosition;
        
        StartCoroutine(ShowWindowAfterDelay());
    }

    public void Continue()
    {
        ResumeGame();
    }

    protected virtual void ResumeGame()
    {
        PanelOutro();
        
        Time.timeScale = 1f;
        
        GameObject uiRoot = DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();

        DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetPauseButton(uiRoot);
    }
    
    protected virtual void PauseGame()
    {
        DestroyPauseButton();

        PanelIntro();

        Time.timeScale = 0f;
    }

    protected CatchZoneViewer GetCatchZoneViewer()
    {
        GameObject vehicle = DIServicesContainer.Instance.GetService<IGameplayFactory>().GetPlayerInstance();
        
        if(vehicle.TryGetComponent(out CatchZoneViewer catchZoneViewer) == false)
            throw new InvalidOperationException();

        return catchZoneViewer;
    }

    private IEnumerator ShowWindowAfterDelay()
    {
        yield return new WaitForSeconds(DelayBeforeShowWindow);

        PauseGame();
    }
}