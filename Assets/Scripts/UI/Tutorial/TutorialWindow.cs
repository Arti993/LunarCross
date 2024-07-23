using System;
using System.Collections;
using UnityEngine;

public class TutorialWindow : UIWindow
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
        
        GameObject uiRoot = AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();

        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetPauseButton(uiRoot);
    }
    
    protected virtual void PauseGame()
    {
        DestroyPauseButton();

        PanelIntro();

        Time.timeScale = 0f;
    }

    protected CatchZoneViewer GetCatchZoneViewer()
    {
        GameObject vehicle = AllServicesContainer.Instance.GetService<IGameplayFactory>().GetPlayerInstance();
        
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