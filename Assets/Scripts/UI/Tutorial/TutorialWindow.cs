using System;
using System.Collections;
using UnityEngine;

public class TutorialWindow : UIWindow
{
    protected virtual void Awake()
    {
        Vector3 startPosition = PanelRect.localPosition;

        startPosition.y = PanelBottomPosY;

        PanelRect.localPosition = startPosition;
    }

    public void Open()
    {
        Time.timeScale = 0f;
        
        PanelIntro();
    }
    
    public void Close()
    {
        PanelOutro();
        
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStatePauseButton>();
    }
    
    protected CatchZoneViewer GetCatchZoneViewer()
    {
        GameObject vehicle = DIServicesContainer.Instance.GetService<IGameplayFactory>().GetPlayerInstance();
        
        if(vehicle.TryGetComponent(out CatchZoneViewer catchZoneViewer) == false)
            throw new InvalidOperationException();

        return catchZoneViewer;
    }

    
}