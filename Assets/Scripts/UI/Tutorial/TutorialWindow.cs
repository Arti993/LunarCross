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

    public virtual void Open()
    {
        Time.timeScale = 0f;
        
        PanelIntro();
    }
    
    public virtual void Close()
    {
        PanelOutro();
        
        Time.timeScale = 1f;

        DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStatePauseButton>();
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

        Open();
    }
}